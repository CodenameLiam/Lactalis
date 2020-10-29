
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;
using Lactalis;
using Lactalis.Helpers;
using Lactalis.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServersideTests.Helpers
{
	public static class ServerBuilder
	{
		/// <summary>
		/// Creates a web host with an in memory database for testing
		/// </summary>
		/// <param name="builderOptions">Options for the host</param>
		/// <returns>The new web host</returns>
		public static IWebHost CreateServer(ServerBuilderOptions builderOptions = null)
		{
			builderOptions ??= new ServerBuilderOptions();

			var httpContext = new DefaultHttpContext { User = builderOptions.UserPrincipal };


			var host = WebHost.CreateDefaultBuilder()
				.ConfigureAppConfiguration((builderContext, config) =>
				{
					var env = builderContext.HostingEnvironment;
					config.AddXmlFile("appsettings.xml", optional: false);
					config.AddXmlFile($"appsettings.Test.xml", optional: true);
					config.AddEnvironmentVariables();
				})
				.UseStartup<Startup>()
				.UseEnvironment("Development")
				.ConfigureServices(sc =>
				{
					sc.AddDbContext<LactalisDBContext>(builderOptions.DatabaseOptions(builderOptions));
					sc.AddScoped<IHttpContextAccessor>(_ => new HttpContextAccessor
					{
						HttpContext = httpContext
					});
					builderOptions.ConfigureServices?.Invoke(sc, builderOptions);
				})
				.Build();

			if (builderOptions.InitialiseData)
			{
				var dataSeed = host.Services.GetRequiredService<DataSeedHelper>();
				dataSeed.Initialize();
			}


			return host;
		}

		/// <summary>
		/// Creates a claims principal for a user
		/// </summary>
		/// <param name="userId">The id of the user</param>
		/// <param name="userName">The username of the user</param>
		/// <param name="email">The email of the user</param>
		/// <param name="roles">The groups that the user is in</param>
		/// <returns>A claims principal to represent this information</returns>
		public static ClaimsPrincipal CreateUserPrincipal(Guid userId, string userName, string email, IEnumerable<string> roles)
		{
			var identity = new ClaimsIdentity(
				CookieAuthenticationDefaults.AuthenticationScheme,
				OpenIdConnectConstants.Claims.Name,
				OpenIdConnectConstants.Claims.Role);
			identity.AddClaim(new Claim("UserId", userId.ToString()));
			identity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Subject, userName));
			identity.AddClaim(new Claim(OpenIdConnectConstants.Claims.Name, email));
			identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));


			return new ClaimsPrincipal(identity);
		}
	}
}