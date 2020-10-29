
using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
 

namespace Lactalis
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

			if (args.Length > 0 && args[0] == "swagger")
			{
				Console.WriteLine(GenerateSwagger(args));
				return;
			}

			try
			{
				Log.Information("Starting web host");
				CreateWebHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly");
				throw;
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((builderContext, config) =>
				{
					var env = builderContext.HostingEnvironment;

					config.SetBasePath(env.ContentRootPath);
					config.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true);
					config.AddXmlFile($"appsettings.{env.EnvironmentName}.xml", optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables();
					config.AddEnvironmentVariables("Lactalis_");
					config.AddEnvironmentVariables($"Lactalis_{env.EnvironmentName}_");
					config.AddCommandLine(args);
				})
				.UseSerilog()
				.UseStartup<Startup>();

		private static string GenerateSwagger(string[] args)
		{
			var host = CreateWebHostBuilder(args.Skip(1).ToArray()).Build();
			var sw = (ISwaggerProvider)host.Services.GetService(typeof(ISwaggerProvider));
			var doc = sw.GetSwagger("json", null, "/");
			return JsonConvert.SerializeObject(
				doc,
				Formatting.Indented,
				new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
					ContractResolver = new DefaultContractResolver()
				}
			);
		}
	}
}
