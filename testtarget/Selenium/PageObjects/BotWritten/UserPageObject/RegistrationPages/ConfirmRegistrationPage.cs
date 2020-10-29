

using OpenQA.Selenium;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class ConfirmRegistrationPage : BasePage
	{
		public IWebElement ConfirmButton => FindElementExt("ConfirmButton");

		public ConfirmRegistrationPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("ConfirmButton", (selector: "confirm_registration", type: SelectorType.ID));
		}
	}
}
