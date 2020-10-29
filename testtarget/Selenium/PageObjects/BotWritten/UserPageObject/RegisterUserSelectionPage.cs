

using System;
using OpenQA.Selenium;
using SeleniumTests.Enums;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegisterUserSelectionPage : BasePage
	{
		public IWebElement UserTypeDropdown => FindElementExt("UserTypeDropdown");
		public IWebElement ConfirmButton => FindElementExt("ConfirmButton");
		public IWebElement CancelButton => FindElementExt("CancelButton");

		public RegisterUserSelectionPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("UserTypeDropdown", (selector: "//div[contains(@class, 'input-group__dropdown')]//input", type: SelectorType.XPath));
			selectorDict.Add("ConfirmButton", (selector: "confirm_type", type: SelectorType.ID));
			selectorDict.Add("CancelButton", (selector: "cancel_register", type: SelectorType.ID));
		}

		public RegisterUserBasePage Select (UserType userType)
		{
			UserTypeDropdown.Click();

			switch (userType)
			{
				default:
					throw new Exception($"Invalid user type {userType}");
			}
		}
	}
}
