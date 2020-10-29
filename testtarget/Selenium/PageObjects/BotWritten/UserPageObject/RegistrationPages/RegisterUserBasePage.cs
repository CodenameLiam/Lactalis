

using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public abstract class RegisterUserBasePage : BasePage
	{
		public IWebElement EmailInput => FindElementExt("EmailInput");
		public IWebElement PasswordInput => FindElementExt("PasswordInput");
		public IWebElement ConfirmPasswordInput => FindElementExt("ConfirmPasswordInput");
		public IWebElement RegisterButton => FindElementExt("RegisterButton");
		public IWebElement CancelButton => FindElementExt("CancelButton");

		public RegisterUserBasePage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("EmailInput", (selector: "div.email > input", type: SelectorType.CSS));
			selectorDict.Add("PasswordInput", (selector: "div.password > input", type: SelectorType.CSS));
			selectorDict.Add("ConfirmPasswordInput", (selector: "div._confirmPassword > input", type: SelectorType.CSS));
			selectorDict.Add("RegisterButton", (selector: "submit_register", type: SelectorType.ID));
			selectorDict.Add("CancelButton", (selector: "cancel_register", type: SelectorType.ID));
		}

		public abstract void Register(UserBaseEntity entity);

		internal void FillRegistrationDetails(string email, string password)
		{
			EmailInput.SendKeys(email);
			PasswordInput.SendKeys(password);
			ConfirmPasswordInput.SendKeys(password);
		}
	}
}
