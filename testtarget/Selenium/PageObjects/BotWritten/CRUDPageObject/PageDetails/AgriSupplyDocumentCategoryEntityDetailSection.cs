/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
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
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class AgriSupplyDocumentCategoryEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By AgriSupplyDocumentssElementBy => By.XPath("//*[contains(@class, 'agriSupplyDocuments')]//div[contains(@class, 'dropdown__container')]/a");
		private static By AgriSupplyDocumentssInputElementBy => By.XPath("//*[contains(@class, 'agriSupplyDocuments')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly AgriSupplyDocumentCategoryEntity _agriSupplyDocumentCategoryEntity;

		//Attribute Header Titles
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public AgriSupplyDocumentCategoryEntityDetailSection(ContextConfiguration contextConfiguration, AgriSupplyDocumentCategoryEntity agriSupplyDocumentCategoryEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_agriSupplyDocumentCategoryEntity = agriSupplyDocumentCategoryEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin

			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("AgrisupplydocumentsElement", (selector: ".input-group__dropdown.agriSupplyDocumentss > .dropdown.dropdown__container", type: SelectorType.CSS));

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
			// % protected region % [Configure entity application here] off begin
			SetName(_agriSupplyDocumentCategoryEntity.Name);

			if (_agriSupplyDocumentCategoryEntity.AgriSupplyDocumentsIds != null)
			{
				SetAgriSupplyDocumentss(_agriSupplyDocumentCategoryEntity.AgriSupplyDocumentsIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "agrisupplydocuments":
					return GetAgriSupplyDocumentss();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetAgriSupplyDocumentss(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, AgriSupplyDocumentssInputElementBy, ElementState.VISIBLE);
			var agriSupplyDocumentssInputElement = _driver.FindElementExt(AgriSupplyDocumentssInputElementBy);

			foreach(var id in ids)
			{
				agriSupplyDocumentssInputElement.SendKeys(id);
				WaitForDropdownOptions();
				agriSupplyDocumentssInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetAgriSupplyDocumentss()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, AgriSupplyDocumentssElementBy, ElementState.VISIBLE);
			var agriSupplyDocumentssElement = _driver.FindElements(AgriSupplyDocumentssElementBy);

			foreach(var element in agriSupplyDocumentssElement)
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


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}