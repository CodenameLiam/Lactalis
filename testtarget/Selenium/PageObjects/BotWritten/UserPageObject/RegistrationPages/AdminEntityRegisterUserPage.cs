

using System;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;



namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegisterAdminEntityUserPage : RegisterUserBasePage
	{
		public override string Url => baseUrl + "/login";

		public RegisterAdminEntityUserPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}

		public override void Register (UserBaseEntity entity)
		{
			FillRegistrationDetails(entity.EmailAddress, entity.Password);
			RegisterButton.Click();
		}
	}
}
