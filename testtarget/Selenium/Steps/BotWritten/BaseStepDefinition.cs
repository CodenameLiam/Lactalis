

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace SeleniumTests.Steps.BotWritten
{
	[Binding]
	public class BaseStepDefinition
	{
		ed readonly IWebDriver _driver;
		ed readonly IWait<IWebDriver> _driverWait;
		ed readonly string _baseUrl;
		ed readonly ITestOutputHelper _testOutputHelper;
		ed readonly ContextConfiguration _contextConfiguration;

		public BaseStepDefinition(ContextConfiguration contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_baseUrl = contextConfiguration.BaseUrl;
			_testOutputHelper = contextConfiguration.TestOutputHelper;
			_contextConfiguration = contextConfiguration;
		}
	}
}