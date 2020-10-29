

using System;
using OpenQA.Selenium;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.PageObjects.BotWritten.UserPageObjects;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class VerifySteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private readonly GenericEntityPage _genericEntityPage;

		public VerifySteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
			_genericEntityPage = new GenericEntityPage(_contextConfiguration);
		}

		[StepDefinition("I assert that I am redirected from (.*) to login page")]
		public void AssertRedirectedFromToLoginPage (String entityName)
		{
			LoginPage page = new LoginPage (_contextConfiguration);
			var expectedPage = page.Url + "?redirect=/admin/" + entityName.ToLower();
			_driverWait.Until(x => expectedPage == x.Url);
		}

		[ObsoleteAttribute]
		[StepDefinition("I expect the element with (.*) of (.*) to contain the text '(.*)'")]
		public void ExpectElementByToContainText(SelectorPathType selector, string path, string expectedText)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var elementText = _driver.FindElement(elementBy).Text;
			Assert.Equal(expectedText, elementText);
		}

		[ObsoleteAttribute]
		[StepDefinition("I expect the element with (.*) of (.*) to contain the date (.*)")]
		public void ExpectAnElementToBePresentBy(SelectorPathType selector, string path, string expectedDate)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var elementText = _driver.FindElement(elementBy).Text;
			Assert.Equal(expectedDate, elementText);
		}

		[ObsoleteAttribute]
		[StepDefinition("I expect the element with (.*) of (.*) to be visible")]
		public void ExpectAnElementToBePresentBy(SelectorPathType selector, string path)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			Assert.True(_driver.FindElement(elementBy).Displayed);
		}

		[StepDefinition("The string (.*) is in each row of the the collection content")]
		public void TheStringToSearchIsInEachOfTheCollectionContent(string stringToSearch)
		{
			bool isInEachRow = _genericEntityPage.TheCommonStringIsInEachOfTheRowContent(stringToSearch, _genericEntityPage.CollectionTable);
			Assert.True(isInEachRow);
		}
	}
}