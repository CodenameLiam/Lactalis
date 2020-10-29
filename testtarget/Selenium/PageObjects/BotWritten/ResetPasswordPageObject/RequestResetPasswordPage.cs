

using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.ResetPasswordPageObject
{
	public class RequestResetPasswordPage : BasePage
	{
		public override string Url => baseUrl + "/reset-password-request";
		public IWebElement EmailAddressInput => FindElementExt("EmailAddressInput");
		public IWebElement ResetPasswordButton => FindElementExt("ResetPasswordButton");
		public IWebElement CancelButton => FindElementExt("CancelButton");

		public RequestResetPasswordPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("EmailAddressInput", (selector: "username-field", type: SelectorType.ID));
			selectorDict.Add("ResetPasswordButton", (selector: "reset_password", type: SelectorType.ID));
			selectorDict.Add("CancelButton", (selector: ".cancel-reset-pwd", type: SelectorType.CSS));
		}

		public void SetEmailAndSubmit (string email)
		{
			EmailAddressInput.SendKeys(email);
			ResetPasswordButton.Click();
		}
	}
}