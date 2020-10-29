
using System;
using System.IO;
using System.Linq;
using Lactalis.Models;
using APITests.Utils;
using Microsoft.EntityFrameworkCore;
using APITests.Settings;
using Microsoft.Extensions.Configuration;

namespace APITests.Setup
{
	public class StartupTestFixture
	{
		public string BaseUrl { get; }

		public string TestUsername { get; }

		public string TestPassword { get;}

		public string SuperUsername { get; }

		public string SuperPassword { get; }

		public DbContextOptions<LactalisDBContext> DbContextOptions {get;}

		public Guid SuperOwnerId { get; private set; }

		public IConfigurationRoot AppSettings { get; }

		public SiteSettings SiteSettings { get; }

		public IConfigurationRoot UserSettings { get; }


		public StartupTestFixture()
		{
			AppSettings = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddXmlFile("appsettings.Test.xml", optional: true, reloadOnChange: false)
				.AddEnvironmentVariables()
				.AddEnvironmentVariables("Lactalis_")
				.AddEnvironmentVariables($"Lactalis_Test_")
				.Build();

			//load in site configuration
			var siteConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("SiteConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			SiteSettings = new SiteSettings();
			siteConfiguration.GetSection("site").Bind(SiteSettings);

			//load in the user configurations
			UserSettings = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("UserConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var superUserSettings = new UserSettings();
			var testUserSettings = new UserSettings();
			UserSettings.GetSection("super").Bind(superUserSettings);
			UserSettings.GetSection("test").Bind(testUserSettings);

			var baseUrlFromEnvironment = Environment.GetEnvironmentVariable("BASE_URL");
			BaseUrl = baseUrlFromEnvironment ?? SiteSettings.BaseUrl;

			TestUsername = testUserSettings.Username;
			TestPassword = testUserSettings.Password;
			SuperUsername = superUserSettings.Username;
			SuperPassword = superUserSettings.Password;

			var dbConnectionString = AppSettings["ConnectionStrings:DbConnectionString"];
			DbContextOptions = new DbContextOptionsBuilder<LactalisDBContext>()
				.UseNpgsql(dbConnectionString)
				.Options;

			PingServer.TestConnection(BaseUrl);
			using (var context = new LactalisDBContext(DbContextOptions, null, null))
			{
				SuperOwnerId = context.Users.First(x => x.UserName == SuperUsername).Id;
			}
		}

	}
}
