

using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using Xunit;



namespace SeleniumTests.Steps.BotWritten.Logout
{
	[Binding]
	public class LogoutSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly LogoutPage _logoutPage;

		public LogoutSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_logoutPage = new LogoutPage(_contextConfiguration);
		}

		[Then(@"I am redirected to the login page")]
		public void ThenIamRedirectedToTheLoginPage()
		{
			Assert.Equal($"{_baseUrl}/login?redirect=/", _driver.Url);
		}

		[StepDefinition("I am logged out of the site")]
		public void Logout()
		{
			_logoutPage.Navigate();

		}		
	}
}