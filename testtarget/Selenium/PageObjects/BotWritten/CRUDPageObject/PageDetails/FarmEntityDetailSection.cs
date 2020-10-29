
using System;
using System.Linq;
using System.Collections.Generic;
using EntityObject.Enums;
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
	public class FarmEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By PickupssElementBy => By.XPath("//*[contains(@class, 'pickups')]//div[contains(@class, 'dropdown__container')]/a");
		private static By PickupssInputElementBy => By.XPath("//*[contains(@class, 'pickups')]/div/input");
		private static By FarmerssElementBy => By.XPath("//*[contains(@class, 'farmers')]//div[contains(@class, 'dropdown__container')]/a");
		private static By FarmerssInputElementBy => By.XPath("//*[contains(@class, 'farmers')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly FarmEntity _farmEntity;

		//Attribute Header Titles
		private IWebElement CodeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Code']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));
		private IWebElement StateHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='State']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public FarmEntityDetailSection(ContextConfiguration contextConfiguration, FarmEntity farmEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_farmEntity = farmEntity;

			InitializeSelectors();
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("CodeElement", (selector: "//div[contains(@class, 'code')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));
			selectorDict.Add("StateElement", (selector: "//div[contains(@class, 'state')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("PickupsElement", (selector: ".input-group__dropdown.pickupss > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("FarmersElement", (selector: ".input-group__dropdown.farmerss > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement CodeElement => FindElementExt("CodeElement");
		private IWebElement NameElement => FindElementExt("NameElement");
		private IWebElement StateElement => FindElementExt("StateElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Code" => CodeHeaderTitle,
				"Name" => NameHeaderTitle,
				"State" => StateHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Code":
					return CodeElement;
				case "Name":
					return NameElement;
				case "State":
					return StateElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Code":
					SetCode(value);
					break;
				case "Name":
					SetName(value);
					break;
				case "State":
					SetState((State)Enum.Parse(typeof(State), value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Code" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.code > div > p"),
				"Name" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.name > div > p"),
				"State" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.state > div > p"),
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
			SetCode(_farmEntity.Code);
			SetName(_farmEntity.Name);
			SetState(_farmEntity.State);

			if (_farmEntity.PickupsIds != null)
			{
				SetPickupss(_farmEntity.PickupsIds.Select(x => x.ToString()));
			}
			if (_farmEntity.FarmersIds != null)
			{
				SetFarmerss(_farmEntity.FarmersIds.Select(x => x.ToString()));
			}
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "pickups":
					return GetPickupss();
				case "farmers":
					return GetFarmerss();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetPickupss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, PickupssInputElementBy, ElementState.VISIBLE);
			var pickupssInputElement = _driver.FindElementExt(PickupssInputElementBy);

			foreach(var id in ids)
			{
				pickupssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				pickupssInputElement.SendKeys(Keys.Return);
			}
		}

		private void SetFarmerss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, FarmerssInputElementBy, ElementState.VISIBLE);
			var farmerssInputElement = _driver.FindElementExt(FarmerssInputElementBy);

			foreach(var id in ids)
			{
				farmerssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				farmerssInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetPickupss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, PickupssElementBy, ElementState.VISIBLE);
			var pickupssElement = _driver.FindElements(PickupssElementBy);

			foreach(var element in pickupssElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}
		private List<Guid> GetFarmerss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, FarmerssElementBy, ElementState.VISIBLE);
			var farmerssElement = _driver.FindElements(FarmerssElementBy);

			foreach(var element in farmerssElement)
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

		private void SetCode (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "code", value, _isFastText);
			CodeElement.SendKeys(Keys.Tab);
			CodeElement.SendKeys(Keys.Escape);
		}

		private String GetCode =>
			CodeElement.Text;

		private void SetName (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "name", value, _isFastText);
			NameElement.SendKeys(Keys.Tab);
			NameElement.SendKeys(Keys.Escape);
		}

		private String GetName =>
			NameElement.Text;

		private void SetState (State value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "state", value.ToString(), _isFastText);
		}

		private State GetState =>
			(State)Enum.Parse(typeof(State), StateElement.Text);
			

	}
}