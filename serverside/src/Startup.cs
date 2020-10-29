
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Dataion;

using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.EntityFramework;
using GraphQL.Utilities;
using Audit.Core;
using Audit.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;

using Lactalis.Configuration;
using Lactalis.Models;
using Lactalis.Services;
using Lactalis.Helpers;
using Lactalis.Utility;
using Lactalis.Graphql;
using Lactalis.Graphql.Types;
using Lactalis.Controllers;
using Lactalis.Enums;
using Lactalis.Services.Scheduling;
using Lactalis.Services.CertificateProvider;
using Lactalis.Services.Interfaces;
using Lactalis.Services.Files;
using Lactalis.Services.Files.Providers;
using Serilog;
using Serilog.Events;
 

namespace Lactalis
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			Configuration = configuration;
			CurrentEnvironment = env;
		}

		private IWebHostEnvironment CurrentEnvironment { get; set; }

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.Enrich.FromLogContext()
				.Enrich.WithProperty("Application", "Lactalis")
				.WriteTo.Console()
				.CreateLogger();

			AddMvc(services);

			ConfigureDatabaseConnection(services);

			ConfigureAuthServices(services);

			ConfigureScopedServices(services);

			ConfigureGraphql(services);

			AddSwaggerService(services);

			AddApplicationConfigurations(services);


			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ctx => new LactalisActionResult();
			});

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "Client";
			});

			// Add scheduled tasks & scheduler
			LoadScheduledTasks(services);

			// Autofac Dependency Injection
			var container = RegisterAutofacTypes(services);

			//Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(container);
		}

		private void AddMvc(IServiceCollection services)
		{
			services.AddMvc(options =>
				{
					options.Filters.Add(new XsrfActionFilterAttribute());
					options.Filters.Add(new AntiforgeryFilterAttribute());
				})
				.AddControllersAsServices()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				})
				.AddMvcOptions(options =>
				{
					// Add extra output formatters after JSON to ensure JSON is the default
					options.OutputFormatters.Add(new CsvOutputFormatter());
				});
		}

		/// <summary>
		/// Set up the database connection
		/// </summary>
		/// <param name="services"></param>
		private void ConfigureDatabaseConnection(IServiceCollection services)
		{
			var dbConnectionString = Configuration.GetConnectionString("DbConnectionString");
			services.AddDbContext<LactalisDBContext>(options =>
			{
				options.UseNpgsql(dbConnectionString);
				options.UseOpenIddict<Guid>();
			});
		}

		private void AddSwaggerService(IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("json", new OpenApiInfo {Title = "Lactalis", Version = "v1"});
				options.ResolveConflictingActions(a => a.First());

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}

		private void ConfigureAuthServices(IServiceCollection services)
		{
			services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

			services.AddDataion()
				.PersistKeysToDbContext<LactalisDBContext>();

			// Register Identity Services
			services.AddIdentity<User, Group>(options =>
				{
					options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
					options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
					options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;

					options.User.AllowedUserNameCharacters += @"\*";

					if (CurrentEnvironment.IsDevelopment())
					{
						options.Password.RequiredLength = 6;
						options.Password.RequiredUniqueChars = 0;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Password.RequireDigit = false;
					}
					else
					{
						options.Password.RequiredLength = 12;
						options.Password.RequiredUniqueChars = 0;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireLowercase = false;
						options.Password.RequireUppercase = false;
						options.Password.RequireDigit = false;
					}

				})
				.AddEntityFrameworkStores<LactalisDBContext>()
				.AddDefaultTokenProviders();

			ConfigureAuthorizationLibrary(services);

			var certSetting = Configuration.GetSection("CertificateSetting").Get<CertificateSetting>();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			services.AddAuthentication("Identity.Application")
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.LoginPath = "/api/authorization/login";
					options.LogoutPath = "/api/authorization/logout";
					options.SlidingExpiration = true;
					options.ExpireTimeSpan = TimeSpan.FromDays(7);
					options.Events.OnRedirectToLogin = redirectOptions =>
					{
						redirectOptions.Response.StatusCode = StatusCodes.Status401Unauthorized;
						return Task.CompletedTask;
					};
				})
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
					options.Authority = certSetting.JwtBearerAuthority;
					options.Audience = certSetting.JwtBearerAudience;
					options.RequireHttpsMetadata = false;
					options.IncludeErrorDetails = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						NameClaimType = OpenIdConnectConstants.Claims.Name,
						RoleClaimType = OpenIdConnectConstants.Claims.Role
					};
				})
				;


			services.AddAuthorization(options =>
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(
						JwtBearerDefaults.AuthenticationScheme,
						CookieAuthenticationDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();

				options.AddPolicy(
					"AllowVisitorPolicy",
					new AuthorizationPolicyBuilder(
							JwtBearerDefaults.AuthenticationScheme,
							CookieAuthenticationDefaults.AuthenticationScheme)
						.RequireAssertion(_ => true)
						.Build());
			});
		}

		private void ConfigureAuthorizationLibrary(IServiceCollection services)
		{
			var certSetting = Configuration.GetSection("CertificateSetting").Get<CertificateSetting>();

			services.AddOpenIddict()
				.AddCore(options =>
				{
					options.UseEntityFrameworkCore()
						.UseDbContext<LactalisDBContext>()
						.ReplaceDefaultEntities<Guid>();
				})
				.AddServer(options =>
				{
					options.UseMvc();
					options.EnableTokenEndpoint("/api/authorization/connect/token");

					X509Certificate2 cert = null;
					if (CurrentEnvironment.IsDevelopment())
					{
						cert = new InRootFolderCertificateProvider(certSetting).ReadX509SigningCert();
					}
					else
					{
						// not for production, currently using the same as development testing.
						// todo: Create another Certificate Provider Inheriting BaseCertificateProvider, and override ReadX509SigningCert
						// to read cerficicate from another more secure place, e.g cerficate store, aws server...
						cert = new InRootFolderCertificateProvider(certSetting).ReadX509SigningCert();
					}

					if (cert == null)
					{
						// not for production, use x509 certificate and .AddSigningCertificate()
						options.AddEphemeralSigningKey();
					}
					else
					{
						options.AddSigningCertificate(cert);
					}

					// use jwt
					options.UseJsonWebTokens();
					options.AllowPasswordFlow();
					options.AllowRefreshTokenFlow();
					options.AcceptAnonymousClients();
					options.DisableHttpsRequirement();
				});
		}

		private void ConfigureScopedServices(IServiceCollection services) {
			// Register service to seed test data
			services.TryAddScoped<DataSeedHelper>();

			// Register core scoped services
			services.TryAddScoped<IUserService, UserService>();
			services.TryAddScoped<IGraphQlService, GraphQlService>();
			services.TryAddScoped<ICrudService, CrudService>();
			services.TryAddScoped<ISecurityService, SecurityService>();
			services.TryAddScoped<IIdentityService, IdentityService>();
			services.TryAddScoped<IEmailService, EmailService>();
			services.TryAddScoped<IAuditService, AuditService>();
			services.TryAddScoped<IXsrfService, XsrfService>();

			// Register context filters
			services.TryAddScoped<AntiforgeryFilter>();
			services.TryAddScoped<XsrfActionFilter>();

			// Configure the file system provider to use
			var storageOptions = new StorageProviderConfiguration();
			Configuration.GetSection("StorageProvider").Bind(storageOptions);
			switch (storageOptions.Provider)
			{
				case StorageProviders.S3:
					services.TryAddScoped<IUploadStorageProvider, S3StorageProvider>();
					break;
				case StorageProviders.FILE_SYSTEM:
				default:
					services.TryAddScoped<IUploadStorageProvider, FileSystemStorageProvider>();
					break;
			}

		}

		private void ConfigureGraphql(IServiceCollection services)
		{
			// GraphQL types must be registered as singleton services. This is since building the underlying graph is
			// expensive and should only be done once.
			services.TryAddSingleton<TradingPostListingEntityType>();
			services.TryAddSingleton<TradingPostListingEntityInputType>();
			services.TryAddSingleton<TradingPostCategoryEntityType>();
			services.TryAddSingleton<TradingPostCategoryEntityInputType>();
			services.TryAddSingleton<AdminEntityType>();
			services.TryAddSingleton<AdminEntityInputType>();
			services.TryAddSingleton<AdminEntityCreateInputType>();
			services.TryAddSingleton<FarmEntityType>();
			services.TryAddSingleton<FarmEntityInputType>();
			services.TryAddSingleton<MilkTestEntityType>();
			services.TryAddSingleton<MilkTestEntityInputType>();
			services.TryAddSingleton<FarmerEntityType>();
			services.TryAddSingleton<FarmerEntityInputType>();
			services.TryAddSingleton<FarmerEntityCreateInputType>();
			services.TryAddSingleton<ImportantDocumentCategoryEntityType>();
			services.TryAddSingleton<ImportantDocumentCategoryEntityInputType>();
			services.TryAddSingleton<TechnicalDocumentCategoryEntityType>();
			services.TryAddSingleton<TechnicalDocumentCategoryEntityInputType>();
			services.TryAddSingleton<QualityDocumentCategoryEntityType>();
			services.TryAddSingleton<QualityDocumentCategoryEntityInputType>();
			services.TryAddSingleton<QualityDocumentEntityType>();
			services.TryAddSingleton<QualityDocumentEntityInputType>();
			services.TryAddSingleton<TechnicalDocumentEntityType>();
			services.TryAddSingleton<TechnicalDocumentEntityInputType>();
			services.TryAddSingleton<ImportantDocumentEntityType>();
			services.TryAddSingleton<ImportantDocumentEntityInputType>();
			services.TryAddSingleton<NewsArticleEntityType>();
			services.TryAddSingleton<NewsArticleEntityInputType>();
			services.TryAddSingleton<AgriSupplyDocumentCategoryEntityType>();
			services.TryAddSingleton<AgriSupplyDocumentCategoryEntityInputType>();
			services.TryAddSingleton<SustainabilityPostEntityType>();
			services.TryAddSingleton<SustainabilityPostEntityInputType>();
			services.TryAddSingleton<AgriSupplyDocumentEntityType>();
			services.TryAddSingleton<AgriSupplyDocumentEntityInputType>();
			services.TryAddSingleton<PromotedArticlesEntityType>();
			services.TryAddSingleton<PromotedArticlesEntityInputType>();
			services.TryAddSingleton<TradingPostListingsTradingPostCategoriesType>();
			services.TryAddSingleton<TradingPostListingsTradingPostCategoriesInputType>();
			services.TryAddSingleton<FarmersFarmsType>();
			services.TryAddSingleton<FarmersFarmsInputType>();

			// Register enum GraphQl types
			services.TryAddSingleton<EnumerationGraphType<PriceType>>();
			services.TryAddSingleton<EnumerationGraphType<State>>();

			// Connect the database type to the GraphQL type
			GraphTypeTypeRegistry.Register<TradingPostListingEntity, TradingPostListingEntityType>();
			GraphTypeTypeRegistry.Register<TradingPostCategoryEntity, TradingPostCategoryEntityType>();
			GraphTypeTypeRegistry.Register<AdminEntity, AdminEntityType>();
			GraphTypeTypeRegistry.Register<FarmEntity, FarmEntityType>();
			GraphTypeTypeRegistry.Register<MilkTestEntity, MilkTestEntityType>();
			GraphTypeTypeRegistry.Register<FarmerEntity, FarmerEntityType>();
			GraphTypeTypeRegistry.Register<ImportantDocumentCategoryEntity, ImportantDocumentCategoryEntityType>();
			GraphTypeTypeRegistry.Register<TechnicalDocumentCategoryEntity, TechnicalDocumentCategoryEntityType>();
			GraphTypeTypeRegistry.Register<QualityDocumentCategoryEntity, QualityDocumentCategoryEntityType>();
			GraphTypeTypeRegistry.Register<QualityDocumentEntity, QualityDocumentEntityType>();
			GraphTypeTypeRegistry.Register<TechnicalDocumentEntity, TechnicalDocumentEntityType>();
			GraphTypeTypeRegistry.Register<ImportantDocumentEntity, ImportantDocumentEntityType>();
			GraphTypeTypeRegistry.Register<NewsArticleEntity, NewsArticleEntityType>();
			GraphTypeTypeRegistry.Register<AgriSupplyDocumentCategoryEntity, AgriSupplyDocumentCategoryEntityType>();
			GraphTypeTypeRegistry.Register<SustainabilityPostEntity, SustainabilityPostEntityType>();
			GraphTypeTypeRegistry.Register<AgriSupplyDocumentEntity, AgriSupplyDocumentEntityType>();
			GraphTypeTypeRegistry.Register<PromotedArticlesEntity, PromotedArticlesEntityType>();
			GraphTypeTypeRegistry.Register<TradingPostListingsTradingPostCategories, TradingPostListingsTradingPostCategoriesType>();
			GraphTypeTypeRegistry.Register<FarmersFarms, FarmersFarmsType>();

			// Add GraphQL core services and executors
			services.TryAddSingleton<IDocumentExecuter, EfDocumentExecuter>();
			services.AddGraphQL();
			services.TryAddSingleton<IDependencyResolver>(
				provider => new FuncDependencyResolver(provider.GetRequiredService)
			);

			// Add the schema and query for graphql
			services.TryAddSingleton<ISchema, LactalisSchema>();
			services.TryAddSingleton<LactalisQuery>();
			services.TryAddSingleton<LactalisMutation>();

			services.TryAddSingleton<IdObjectType>();
			services.TryAddSingleton<NumberObjectType>();
			services.TryAddSingleton<OrderGraph>();
			services.TryAddSingleton<BooleanObjectType>();

			// Send our db context to graphql to use
			EfGraphQLConventions.RegisterInContainer<LactalisDBContext>(services);
			EfGraphQLConventions.RegisterConnectionTypesInContainer(services);
		}

		/// <summary>
		/// Read in configuration key value tuples from the appsettings.xxx files.
		/// </summary>
		/// <param name="services"></param>
		private void AddApplicationConfigurations(IServiceCollection services)
		{
			services.Configure<EmailAccount>(Configuration.GetSection("EmailAccount"));
			services.Configure<StorageProviderConfiguration>(Configuration.GetSection("StorageProvider"));
			services.Configure<FileSystemStorageProviderConfiguration>(Configuration.GetSection("FileSystemStorageProvider"));
			services.Configure<S3StorageProviderConfiguration>(Configuration.GetSection("S3StorageProvider"));
		}

		private IContainer RegisterAutofacTypes(IServiceCollection services)
		{
			var builder = new ContainerBuilder();

			builder.Populate(services);
			return builder.Build();
		}

		private void LoadScheduledTasks(IServiceCollection services)
		{

			services.AddScheduler((sender, args) =>
			{
				Console.Write(args.Exception.Message);
				args.SetObserved();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			DataSeedHelper dataSeed,
			ILogger<AuditLog> logger)
		{

			Audit.Core.Configuration.Setup()
				.UseDynamicProvider(configurator =>
				{
					configurator.OnInsert(audit => AuditUtilities.LogAuditEvent(audit, logger));
					configurator.OnReplace((obj, audit) => AuditUtilities.LogAuditEvent(audit, logger));
				});


			dataSeed.Initialize();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseExceptionHandler("/Error");
				app.UseHsts();

			}


			app.UseSerilogRequestLogging(options =>
			{
				options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} by user: {User} responded {StatusCode} in {Elapsed:0.0000} ms";
				options.EnrichDiagnosticContext = (context, httpContext) =>
				{
					context.Set("User", httpContext.User?.Identity.Name);
					context.Set("UserId", httpContext.User?.FindFirst("UserId")?.Value);
				};
			});

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMiddleware<AuditMiddleware>();

			// Add Swagger json and ui
			var swaggerUrl = "api/swagger/{documentName}/openapi.json";
			app.UseSwagger(options =>
			{
				options.RouteTemplate = swaggerUrl;
			});
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/api/swagger/json/openapi.json", "Lactalis");
				options.RoutePrefix = "api/swagger";
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
			});


			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "Client";

				if (env.IsDevelopment())
				{
					var clientServerSettings = Configuration.GetSection("ClientServerSettings");
					spa.Options.SourcePath = clientServerSettings["ClientSourcePath"];
					bool.TryParse(clientServerSettings["UseProxyServer"], out var useProxyServer);

					if (useProxyServer)
					{
						spa.UseProxyToSpaDevelopmentServer(clientServerSettings["ProxyServerAddress"]);
					}
					else
					{
						spa.UseReactDevelopmentServer("start");
					}
				}
			});
		}
	}
}
