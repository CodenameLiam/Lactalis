

using System;
using System.Linq;
using APITests.EntityObjects.Models;
using SeleniumTests.Enums;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Utils;
using APITests.Factories;
using APITests.Utils;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.UserRegistration
{
	[Binding]
	public class UserRegistrationFeatureSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly LoginPage _loginPage;
		private readonly RegisterUserSelectionPage _registerUserSelectionPage;
		private RegisterUserBasePage RegisterUserPage;
		private UserBaseEntity UserEntity;
		private Email RegistrationEmail;

		public UserRegistrationFeatureSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_loginPage = new LoginPage(contextConfiguration);
			_registerUserSelectionPage = new RegisterUserSelectionPage(contextConfiguration);
		}

		[Given(@"I navigate to the registration page")]
		public void GivenINavigateToTheRegistrationPage()
		{
			_loginPage.Navigate();
			WaitUtils.waitForPage(_driverWait);
			_loginPage.RegisterButton.Click();
		}

		[Given(@"I choose (.*) as my user type")]
		public void GivenIChooseUserAsMyUserType(UserType userType)
		{
			RegisterUserPage = _registerUserSelectionPage.Select(userType);
			WaitUtils.waitForPage(_driverWait);
		}

		[Given(@"I complete the (.*) user registration form")]
		public void GivenICompleteTheRegistrationForm(string userType)
		{
			UserEntity = new UserEntityFactory(userType).Construct();
			UserEntity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_ONLY);
			RegisterUserPage.Register(UserEntity);
		}

		[Then(@"I will see a registration success message")]
		public void ThenIRegistrationSuccessMessage()
		{
			var registrationSuccessPage = new RegistrationSuccessPage(_contextConfiguration);
			_driverWait.Until(x => registrationSuccessPage.registrationEmail.Displayed);
			Assert.True(registrationSuccessPage.successHeader.Displayed);
			Assert.Equal(UserEntity.EmailAddress, registrationSuccessPage.registrationEmail.Text);
		}

		[StepArgumentTransformation]
		public static UserType TransformStringToUserTypeEnum(string userType)
		{
			switch (userType)
			{
				case "Admin":
					return UserType.ADMIN_ENTITY;
				case "Farmer":
					return UserType.FARMER_ENTITY;
				default:
					throw new Exception($"{userType} enum is not handled");
			}
		}
	}
}