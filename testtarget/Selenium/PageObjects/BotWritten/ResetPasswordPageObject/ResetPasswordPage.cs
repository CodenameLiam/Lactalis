

using OpenQA.Selenium;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.ResetPasswordPageObject
{
	public class ResetPasswordPage : BasePage
	{
		public IWebElement NewPasswordInput => FindElementExt("NewPasswordInput");
		public IWebElement ConfirmPasswordInput => FindElementExt("ConfirmPasswordInput");
		public IWebElement ConfirmButton => FindElementExt("ConfirmButton");


		public ResetPasswordPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("NewPasswordInput", (selector: "new_password-field", type: SelectorType.ID));
			selectorDict.Add("ConfirmPasswordInput", (selector: "confirm_password-field", type: SelectorType.ID));
			selectorDict.Add("ConfirmButton", (selector: "confirm_reset_password", type: SelectorType.ID));
		}

		public void EnterNewPasswordAndSubmit(string password)
		{
			NewPasswordInput.SendKeys(password);
			ConfirmPasswordInput.SendKeys(password);
			ConfirmButton.Click();
		}
	
	}
}