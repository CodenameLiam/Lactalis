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
using System.IO;
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
	public class AgriSupplyDocumentEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By AgriSupplyDocumentCategoryIdElementBy => By.XPath("//*[contains(@class, 'agriSupplyDocumentCategory')]//div[contains(@class, 'dropdown__container')]");
		private static By AgriSupplyDocumentCategoryIdInputElementBy => By.XPath("//*[contains(@class, 'agriSupplyDocumentCategory')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly AgriSupplyDocumentEntity _agriSupplyDocumentEntity;

		//Attribute Header Titles
		private IWebElement FileHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='File']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public AgriSupplyDocumentEntityDetailSection(ContextConfiguration contextConfiguration, AgriSupplyDocumentEntity agriSupplyDocumentEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_agriSupplyDocumentEntity = agriSupplyDocumentEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FileElement", (selector: "//div[contains(@class, 'file')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("AgrisupplydocumentcategoryElement", (selector: ".input-group__dropdown.agriSupplyDocumentCategoryId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement AgriSupplyDocumentCategoryElement => FindElementExt("AgriSupplyDocumentCategoryElement");

		//Attribute web Elements
		private IWebElement FileElement => FindElementExt("FileElement");
		private IWebElement NameElement => FindElementExt("NameElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"File" => FileHeaderTitle,
				"Name" => NameHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "File":
					return FileElement;
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
				case "File":
					SetFile(value);
					break;
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
				"File" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.file > div > p"),
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
			SetFile(_agriSupplyDocumentEntity.FileId.ToString());
			SetName(_agriSupplyDocumentEntity.Name);

			SetAgriSupplyDocumentCategoryId(_agriSupplyDocumentEntity.AgriSupplyDocumentCategoryId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "agrisupplydocumentcategory":
					return new List<Guid>() {GetAgriSupplyDocumentCategoryId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetAgriSupplyDocumentCategoryId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, AgriSupplyDocumentCategoryIdInputElementBy, ElementState.VISIBLE);
			var agriSupplyDocumentCategoryIdInputElement = _driver.FindElementExt(AgriSupplyDocumentCategoryIdInputElementBy);

			if (id != null)
			{
				agriSupplyDocumentCategoryIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				agriSupplyDocumentCategoryIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetAgriSupplyDocumentCategoryId()
		{
			WaitUtils.elementState(_driverWait, AgriSupplyDocumentCategoryIdElementBy, ElementState.VISIBLE);
			var agriSupplyDocumentCategoryIdElement = _driver.FindElementExt(AgriSupplyDocumentCategoryIdElementBy);
			return new Guid(agriSupplyDocumentCategoryIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetFile (String value)
		{
			const string script = "document.querySelector('.fileId>div>input').removeAttribute('style')";
			var js = (IJavaScriptExecutor)driver;
			js.ExecuteScript(script);
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources/RedCircle.svg"));
			var fileUploadElement = driver.FindElementExt(By.CssSelector(".fileId>div>input"));
			fileUploadElement.SendKeys(path);
		}

		private String GetFile =>
			FileElement.Text;

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