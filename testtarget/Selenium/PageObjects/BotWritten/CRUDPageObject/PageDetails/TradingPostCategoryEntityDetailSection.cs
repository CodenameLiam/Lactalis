
using System;
using System.Linq;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class TradingPostCategoryEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By TradingPostListingssElementBy => By.XPath("//*[contains(@class, 'tradingPostListings')]//div[contains(@class, 'dropdown__container')]/a");
		private static By TradingPostListingssInputElementBy => By.XPath("//*[contains(@class, 'tradingPostListings')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly TradingPostCategoryEntity _tradingPostCategoryEntity;

		//Attribute Header Titles
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public TradingPostCategoryEntityDetailSection(ContextConfiguration contextConfiguration, TradingPostCategoryEntity tradingPostCategoryEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_tradingPostCategoryEntity = tradingPostCategoryEntity;

			InitializeSelectors();
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("TradingpostlistingsElement", (selector: ".input-group__dropdown.tradingPostListingss > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Name" => NameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Name":
					return NameElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Name":
					SetName(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.name > div > p"),
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = GetErrorAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public void Apply()
		{
			SetName(_tradingPostCategoryEntity.Name);

			if (_tradingPostCategoryEntity.TradingPostListingsIds != null)
			{
				SetTradingPostListingss(_tradingPostCategoryEntity.TradingPostListingsIds.Select(x => x.ToString()));
			}
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "tradingpostlistings":
					return GetTradingPostListingss();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetTradingPostListingss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, TradingPostListingssInputElementBy, ElementState.VISIBLE);
			var tradingPostListingssInputElement = _driver.FindElementExt(TradingPostListingssInputElementBy);

			foreach(var id in ids)
			{
				tradingPostListingssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				tradingPostListingssInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetTradingPostListingss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, TradingPostListingssElementBy, ElementState.VISIBLE);
			var tradingPostListingssElement = _driver.FindElements(TradingPostListingssElementBy);

			foreach(var element in tradingPostListingssElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
			NameElement.SendKeys(Keys.Escape);
		}

		private String GetName =>
			NameElement.Text;


	}
}