

using OpenQA.Selenium;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegisterUserSuccessfulPage : BasePage
	{
		public IWebElement LoginLink => FindElementExt("LoginLink");

		public RegisterUserSuccessfulPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("LoginLink", (selector: "a.login-link", type: SelectorType.CSS));
		}
	}
}
