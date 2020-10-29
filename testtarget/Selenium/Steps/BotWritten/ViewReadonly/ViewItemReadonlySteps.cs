

using System;
using System.Linq;
using SeleniumTests.Enums;
using SeleniumTests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.ViewReadonly
{
	[Binding]
	public class ViewItemReadonlySteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public ViewItemReadonlySteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[When(@"I click View on the first row and navigate to the View (.*)")]
		public void WhenIClickViewOnTheFirstRowAndNavigateToTheViewEntityPage(string entityName)
		{
			var genericEntityPage = new GenericEntityPage(entityName, _contextConfiguration);
			genericEntityPage.ViewTopEntity();
		}

		[Then(@"I assert that the entity input fields  are readonly on the (.*) page")]
		public void ThenIAssertThatTheEntityInputFieldsAreReadonlyOnThePage(string entityName)
		{
			_driverWait.Until(d =>
				d.Url.ToLower().StartsWith(_baseUrl + $"/admin/{entityName.ToLower()}/view/"));
			var entityFactory = new EntityDetailFactory(_contextConfiguration);
			// Get a detail section Object
			var detailSection = entityFactory.CreateDetailSection(entityName);
			var readonlyInputFields = detailSection.GetReadonlyInputFieldAttributes();
			foreach (var readonlyInputField in readonlyInputFields)
			{
				var isReadOnly = string.IsNullOrEmpty(readonlyInputField.GetAttribute("readonly"))
						|| string.IsNullOrEmpty(readonlyInputField.GetAttribute("aria-disabled"));

				Assert.True(isReadOnly);
			}
		}
	}
}

