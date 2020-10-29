
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

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class QualityDocumentEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By QualityDocumentCategoryIdElementBy => By.XPath("//*[contains(@class, 'qualityDocumentCategory')]//div[contains(@class, 'dropdown__container')]");
		private static By QualityDocumentCategoryIdInputElementBy => By.XPath("//*[contains(@class, 'qualityDocumentCategory')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly QualityDocumentEntity _qualityDocumentEntity;

		//Attribute Header Titles
		private IWebElement FileHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='File']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public QualityDocumentEntityDetailSection(ContextConfiguration contextConfiguration, QualityDocumentEntity qualityDocumentEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_qualityDocumentEntity = qualityDocumentEntity;

			InitializeSelectors();
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FileElement", (selector: "//div[contains(@class, 'file')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("QualitydocumentcategoryElement", (selector: ".input-group__dropdown.qualityDocumentCategoryId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement QualityDocumentCategoryElement => FindElementExt("QualityDocumentCategoryElement");

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
			SetFile(_qualityDocumentEntity.FileId.ToString());
			SetName(_qualityDocumentEntity.Name);

			SetQualityDocumentCategoryId(_qualityDocumentEntity.QualityDocumentCategoryId?.ToString());
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "qualitydocumentcategory":
					return new List<Guid>() {GetQualityDocumentCategoryId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetQualityDocumentCategoryId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, QualityDocumentCategoryIdInputElementBy, ElementState.VISIBLE);
			var qualityDocumentCategoryIdInputElement = _driver.FindElementExt(QualityDocumentCategoryIdInputElementBy);

			if (id != null)
			{
				qualityDocumentCategoryIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				qualityDocumentCategoryIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetQualityDocumentCategoryId()
		{
			WaitUtils.elementState(_driverWait, QualityDocumentCategoryIdElementBy, ElementState.VISIBLE);
			var qualityDocumentCategoryIdElement = _driver.FindElementExt(QualityDocumentCategoryIdElementBy);
			return new Guid(qualityDocumentCategoryIdElement.GetAttribute("data-id"));
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


	}
}