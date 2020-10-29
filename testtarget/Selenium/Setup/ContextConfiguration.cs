

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using APITests.Settings;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace SeleniumTests.Setup
{
	public class ContextConfiguration
	{
		private readonly SeleniumSettings _seleniumSettings;

		public ContextConfiguration(ITestOutputHelper testOutputHelper)
		{
			//load in site configuration
			var siteConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("SiteConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var siteSettings = new SiteSettings();
			siteConfiguration.GetSection("site").Bind(siteSettings);

			var baseUrlFromEnvironment = Environment.GetEnvironmentVariable("BASE_URL");
			BaseUrl = baseUrlFromEnvironment ?? siteSettings.BaseUrl;

			// as soon as the site url is given we will test its connection and immediately fail if necessary
			APITests.Utils.PingServer.TestConnection(BaseUrl);

			//load in the selenium configuration configuration
			var seleniumConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("SeleniumConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			_seleniumSettings = new SeleniumSettings();
			seleniumConfiguration.GetSection("selenium").Bind(_seleniumSettings);
			seleniumConfiguration.GetSection("screensize").Bind(_seleniumSettings);
			seleniumConfiguration.GetSection("screenshot").Bind(_seleniumSettings);
			seleniumConfiguration.GetSection("cultureinfo").Bind(_seleniumSettings);

			CultureInfo = new CultureInfo(_seleniumSettings.Locale);

			//load in base choice configuration
			var baseChoiceConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("BaseChoiceConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var baseChoiceSettings = new BaseChoiceSettings();
			baseChoiceConfiguration.GetSection("basechoice").Bind(baseChoiceSettings);

			//load in the user configurations
			var userConfiguration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddIniFile("UserConfig.ini", optional: true, reloadOnChange: false)
				.Build();

			var superUserSettings = new UserSettings();
			var testUserSettings = new UserSettings();
			userConfiguration.GetSection("super").Bind(superUserSettings);
			userConfiguration.GetSection("test").Bind(testUserSettings);

			TestUserConfiguration = testUserSettings;
			SuperUserConfiguration = superUserSettings;
			SeleniumSettings = _seleniumSettings;
			BaseChoiceSettings = baseChoiceSettings;


			WebDriver = InitialiseWebDriver();
			TestOutputHelper = testOutputHelper;
			WebDriverWait = InitializeWebDriverWait(WebDriver, _seleniumSettings.Timeout, _seleniumSettings.PollInterval);
		}

		public readonly ITestOutputHelper TestOutputHelper;
		public CultureInfo CultureInfo { get; set; }
		public string BaseUrl { get; set; }
		public UserSettings TestUserConfiguration { get; set; }
		public UserSettings SuperUserConfiguration { get; set; }
		public SeleniumSettings SeleniumSettings { get; set; }
		public BaseChoiceSettings BaseChoiceSettings { get; set; }
		public IWebDriver WebDriver { get; set; }
		public IWait<IWebDriver> WebDriverWait { get; set; }

		public void WriteTestOutput(string text) => TestOutputHelper.WriteLine(text);

		public IWebDriver InitialiseWebDriver()
		{
			// get the driver type from the configuration
			var driverType = Environment.GetEnvironmentVariable("LACTALIS_TEST_SELENIUM_WEB_DRIVER")
								?? _seleniumSettings.Webdriver.ToLower();

			switch (driverType)
			{
				case "chrome":
				case "chrome-edge":
					var chromeOptions = new ChromeOptions();

					if (_seleniumSettings.Headless)
					{
						chromeOptions.AddArguments("--silent-launch");
						chromeOptions.AddArguments("--no-startup-window");
						chromeOptions.AddArguments("--no-sandbox");
						chromeOptions.AddArguments("--headless");
						chromeOptions.AddArguments("--allow-insecure-localhost");
						chromeOptions.AddArguments("--disable-gpu");
						chromeOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
					}

					// the screensize is set custom if it is either headless or overwritten flag is active
					if (_seleniumSettings.OverwriteDefault || _seleniumSettings.Headless)
					{
						chromeOptions.AddArguments($"--window-size={_seleniumSettings.Width},{_seleniumSettings.Height}");
					}
					else
					{
						chromeOptions.AddArguments("--start-maximized");
					}


					/*
					* for different chromium browsers we will need to specify the binary
					* path to tell it which version to use
					*/
					string chromeDriverDirectory;
					if (driverType == "chrome")
					{
						chromeDriverDirectory = ".";
					}
					else if (driverType == "chrome-edge")
					{
						chromeDriverDirectory = "./EdgeChromiumDriver";
						var binaryPath = _seleniumSettings.EdgeChromiumPath;
						chromeOptions.BinaryLocation = binaryPath;
					}
					else
					{
						throw new Exception("Could not find chromium driver");
					}

					// chrome options are shared between chromium drivers
					WebDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(chromeDriverDirectory), chromeOptions, TimeSpan.FromMinutes(3));
					break;
				case "firefox":
					var firefoxOptions = new FirefoxOptions();

					/*
					* this is required to fix a but in dotnet core where there is an implicit timeout waiting
					* for ipv6 to resolve. See: https://github.com/SeleniumHQ/selenium/issues/6597
					*/
					var service = FirefoxDriverService.CreateDefaultService(".");
					service.Host = "::1";

					if (_seleniumSettings.Headless)
					{
						firefoxOptions.AddArguments("--silent-launch");
						firefoxOptions.AddArguments("--no-startup-window");
						firefoxOptions.AddArguments("--no-sandbox");
						firefoxOptions.AddArguments("--headless");
						firefoxOptions.AddArguments("--allow-insecure-localhost");
						firefoxOptions.AddArguments("--disable-gpu");
						firefoxOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
					}


					WebDriver = new FirefoxDriver(service, firefoxOptions);

					if (_seleniumSettings.OverwriteDefault|| _seleniumSettings.Headless)
					{
						WebDriver.Manage().Window.Size = new Size(_seleniumSettings.Width, _seleniumSettings.Height);
					}
					else
					{
						WebDriver.Manage().Window.Maximize();
					}
					break;
				case "ie":
					WebDriver = new InternetExplorerDriver();

					break;
				case "edge":
					WebDriver = new EdgeDriver();

					break;
				default:
					//default to using a chrome driver which is maximised
					var defaultOptions = new ChromeOptions();
					defaultOptions.AddArguments(new List<string>() {
						"--start-maximized"
						,});
					WebDriver = new ChromeDriver(".", defaultOptions);
					break;
			}


			return WebDriver;
		}

		public static IWait<IWebDriver> InitializeWebDriverWait(IWebDriver webDriver, int timeout, int pollInterval)
		{
			return  new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(timeout))
			{
				PollingInterval = TimeSpan.FromMilliseconds(pollInterval)
			};
		}
	}
}
