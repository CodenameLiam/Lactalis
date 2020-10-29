
using System;
using System.Threading;
using SeleniumTests.PageObjects.TopbarAdminPageObject;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using SeleniumTests.Enums;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.TopBar
{
	[Binding]
	public class TopBarAdminStepDefinitions  : BaseStepDefinition
	{
		private readonly TopBarMenuAdmin _elementsAdminTopBar;
		private readonly ContextConfiguration _contextConfiguration;

		public TopBarAdminStepDefinitions(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_elementsAdminTopBar = new TopBarMenuAdmin(contextConfiguration);
		}

		[StepDefinition(@"I click on the Topbar Link")]
		public void WhenIClickOnTheTopbarLink()
		{
			(new TopBarMenuAdmin(_contextConfiguration)).TopBarLink.Click();
		}

		[StepDefinition(@"I assert that the admin bar is on the (.*)")]
		public void ThenIAssertThatTheAdminBarIsOnTheAdmin(TopbarMenuType topBarMenuText)
		{
			Assert.Equal(topBarMenuText.ToString().ToLower(), _elementsAdminTopBar.TopBarLink.Text.ToLower());

			switch (topBarMenuText)
			{
				case TopbarMenuType.ADMIN:
					Assert.Equal($"{_baseUrl}/", _driver.Url);
					break;
				case TopbarMenuType.FRONTEND:
					Assert.Equal($"{_baseUrl}/admin", _driver.Url);
					break;
				default:
					throw new Exception($"Could not find {topBarMenuText} url");
			}
		}

		[StepArgumentTransformation]
		public static TopbarMenuType TransformStringToTopbarMenuTypeEnum(string topbarMenuType)
		{
			// case insensitive
			switch (topbarMenuType.ToLower())
			{
				case "admin":
					return TopbarMenuType.ADMIN;
				case "frontend":
					return TopbarMenuType.FRONTEND;
				default:
					throw new Exception($"{topbarMenuType}enum is not handled");
			}
		}
	}
}