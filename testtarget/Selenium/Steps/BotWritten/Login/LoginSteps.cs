

using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using Xunit;



namespace SeleniumTests.Steps.BotWritten.Login
{
	[Binding]
	public class LoginSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly LoginPage _loginPage;

		public LoginSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_loginPage = new LoginPage(_contextConfiguration);
		}

		[Given("I login to the site as a user")]
		public void LoginAsUser()
		{
			var userName = _contextConfiguration.SuperUserConfiguration.Username;
			var password = _contextConfiguration.SuperUserConfiguration.Password;
			GivenIAttemptToLogin(userName, password, "success");
		}


		[Given(@"I login to the site with username (.*) and password (.*) then I expect login (.*)")]
		public void GivenIAttemptToLogin(string user, string pass, string success)
		{
			_loginPage.Navigate();
			_loginPage.Login(user, pass);
			try
			{
				_driverWait.Until(wd => wd.Url == _baseUrl + "/");
				Assert.Equal("success", success);
			}
			catch (OpenQA.Selenium.UnhandledAlertException)
			{
				Assert.Equal("failure", success);
			}
			catch (OpenQA.Selenium.WebDriverTimeoutException)
			{
				Assert.Equal(_driver.Url, _baseUrl + "/login");
			}
		}
	}
}