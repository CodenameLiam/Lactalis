

using System;
using System.Linq;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.AdminNav
{
	[Binding]
	public class AdminMenuNavigationSteps : BaseStepDefinition
	{
		private readonly AdminNavSection _adminNavSection;

		public AdminMenuNavigationSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_adminNavSection = new AdminNavSection(contextConfiguration);
		}

		[Then(@"The Admin Nav Menu is displayed")]
		public void ThenIAssertThatTheAdminNavMenuIsDisplayed()
		{
			Assert.True(_adminNavSection.AdminNavMenu.Displayed);
		}

		[When(@"I toggle the Admin Nav section")]
		public void WhenIToggleTheAdminNavSection()
		{
			_adminNavSection.AdminNavToggle.Click();
		}

		[StepDefinition(@"I click on (.*) Nav link on the Admin Nav section")]
		public void WhenIClickOnLinkOfTheAdminNavSection(AdminSubMenuType subMenuType)
		{
			var adminNavSection = new AdminNavSection(_contextConfiguration);
			switch (subMenuType)
			{
				case AdminSubMenuType.USERS:
					adminNavSection.AdminNavIconUsers.ClickWithWait(_driverWait);
					break;
				case AdminSubMenuType.ENTITIES:
					adminNavSection.AdminNavIconEntities.ClickWithWait(_driverWait);
					break;
				default:
					throw new ArgumentException("Invalid Submenu Type: " + subMenuType);
			}
		}

		[Then(@"I see the Admin Submenus like")]
		public void ThenIAssertThatISeeUsersTheSubmenusLike(Table table)
		{
			// extract data from table
			var adminNavDataFromTable = table.GetTableData().Select(t => t.ToLower()).ToList();
			Assert.Equal(adminNavDataFromTable, _adminNavSection.GetAdminNavSubmenuValues());
		}

		[Then(@"I assert that (.*) Nav links are displayed")]
		public void ThenIAssertThatTheSubmenuDisplaysUsersNavLinks(int submenuNavLinks)
		{
			// submenu is displayed
			Assert.True(_adminNavSection.AdminNavSubLink.Displayed);
			Assert.Equal(submenuNavLinks, _adminNavSection.TotalSubmenuLinks());
		}

		[When(@"I click the (.*) admin submenu")]
		public void WhenIClickTheExpeditionAdminSubmenu( string linkName)
		{
			_adminNavSection.GetAdminNavSubmenuLink(linkName.ToLower()).Click();
		}

		[When(@"I am logged out of the site via admin nav link")]
		public void WhenIAmLoggedOutOfTheSiteViaAdminNavLink()
		{
			_adminNavSection.AdminNavIconLogout.Click();
		}

		[When(@"I click the home link of the admin nav section")]
		public void WhenIClickTheHomeLinkOfTheAdminNavSection()
		{
			_adminNavSection.AdminNavIconHome.Click();
		}
	}
}

