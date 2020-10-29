

using System.Linq;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.PageObjects.BotWritten.UserPageObject;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten
{
	[Binding]
	public sealed class UserListSteps  : BaseStepDefinition
	{
		private readonly AllUserPage _allUserPage;
		private readonly ContextConfiguration _contextConfiguration;

		public UserListSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_allUserPage = new AllUserPage(contextConfiguration);
		}

		[StepDefinition(@"I navigate to the all user page")]
		public void NavigateToTheAllUserPage()
		{
			_allUserPage.Navigate();
			WaitUtils.waitForPage(_driverWait);
		}

		[Then(@"I verify the contents of the All User page")]
		public void VerifyTheContentOfTheAllUserPage()
		{
			_driverWait.Until(_ =>_allUserPage.CreateNewButton.Displayed);
			_driverWait.Until(_ =>_allUserPage.ListHeader.Displayed);
			var columnHeadings = _allUserPage.ListHeaders.ToList();
			Assert.True(columnHeadings.TrueForAll(c => _allUserPage.ExpectedColumnHeadings.Contains(c.Text)));
			_allUserPage.CreateNewButton.Click();
			_driverWait.Until(_ =>_allUserPage.SelectUserTypeModal.Displayed);
			_driverWait.Until(_ =>_allUserPage.SelectUserTypeDropdown.Displayed);
		}
	}
}