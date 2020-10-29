

using APITests.Utils;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Sorting
{
	[Binding]
	public sealed class SortingSteps : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		 public SortingSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[When("I sort (.*) by (.*)")]
		public void SortBy(string entityName, string columnName)
		{
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			var columnHeader = page.ColumnHeader(columnName);
			var initialHeaderState = columnHeader.GetAttribute("class");
			columnHeader.Click();
			_driverWait.Until(_ => columnHeader.GetAttribute("class") != initialHeaderState);
		}

		[Then("I assert that (.*) in (.*) of type (.*) is properly sorted in (.*)")]
		public void AssertThatTypeProperlySortedIn(string columnName, string pageName, string attributeType, string sortOrder)
		{
			var page = new GenericEntityPage(pageName, _contextConfiguration);
			// Get original list
			var originalList = page.GetColumnContents(columnName);
			// Filter out non-alphanumeric contents except Date, Time, DateTime
			var sortingList = SortingUtils.FilterOutNonAlphanumeric(attributeType, originalList);
			switch (sortingList.Count )
			{
				case 0:
					Assert.Empty(sortingList); // When table content is all non-alphanumeric/non-date/times
					break;
				default:
					var sortedList = SortingUtils.AssertSorted(_contextConfiguration.CultureInfo, attributeType, sortingList, sortOrder);
					Assert.Equal(sortedList,sortingList, new EqualityListComparer<string>());
					break;
			}
		}
	}
}