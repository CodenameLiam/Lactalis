

using OpenQA.Selenium;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class LoginPage : BasePage
	{
		public override string Url => baseUrl + "/login";
		public IWebElement EmailInput => FindElementExt("EmailInput");
		public IWebElement PasswordInput => FindElementExt("PasswordInput");
		public IWebElement LoginButton => FindElementExt("LoginButton");
		public IWebElement RegisterButton => FindElementExt("RegisterButton");
		public IWebElement PasswordResetLink => FindElementExt("PasswordResetLink");


		public LoginPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("EmailInput", (selector: "login_username-field", type: SelectorType.ID));
			selectorDict.Add("PasswordInput", (selector: "login_password-field", type: SelectorType.ID));
			selectorDict.Add("LoginButton", (selector: "login_submit", type: SelectorType.ID));
			selectorDict.Add("RegisterButton", (selector: "login_register", type: SelectorType.ID));
			selectorDict.Add("PasswordResetLink", (selector: ".link-forgotten-password", type: SelectorType.CSS));
		}

		///<summary>
		/// Attempts to login with the given credentials
		///</summary>
		///<param name="email">The email to use as a string</param>
		///<param name="password">The password to use as a string </param>
		public void Login(string email, string password)
		{
			EmailInput.Clear();
			PasswordInput.Clear();
			EmailInput.SendKeys(email);
			PasswordInput.SendKeys(password);
			LoginButton.Click();
		}

	}
}
