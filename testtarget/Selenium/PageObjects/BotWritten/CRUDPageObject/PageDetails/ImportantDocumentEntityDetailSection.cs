
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
	public class ImportantDocumentEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By DocumentCategoryIdElementBy => By.XPath("//*[contains(@class, 'documentCategory')]//div[contains(@class, 'dropdown__container')]");
		private static By DocumentCategoryIdInputElementBy => By.XPath("//*[contains(@class, 'documentCategory')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly ImportantDocumentEntity _importantDocumentEntity;

		//Attribute Header Titles
		private IWebElement FileHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='File']"));
		private IWebElement NameHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Name']"));
		private IWebElement QldHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='QLD']"));
		private IWebElement NswHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='NSW']"));
		private IWebElement VicHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='VIC']"));
		private IWebElement TasHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='TAS']"));
		private IWebElement WaHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='WA']"));
		private IWebElement SaHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='SA']"));
		private IWebElement NtHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='NT']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public ImportantDocumentEntityDetailSection(ContextConfiguration contextConfiguration, ImportantDocumentEntity importantDocumentEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_importantDocumentEntity = importantDocumentEntity;

			InitializeSelectors();
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("FileElement", (selector: "//div[contains(@class, 'file')]//input", type: SelectorType.XPath));
			selectorDict.Add("NameElement", (selector: "//div[contains(@class, 'name')]//input", type: SelectorType.XPath));
			selectorDict.Add("QldElement", (selector: "//div[contains(@class, 'qld')]//input", type: SelectorType.XPath));
			selectorDict.Add("NswElement", (selector: "//div[contains(@class, 'nsw')]//input", type: SelectorType.XPath));
			selectorDict.Add("VicElement", (selector: "//div[contains(@class, 'vic')]//input", type: SelectorType.XPath));
			selectorDict.Add("TasElement", (selector: "//div[contains(@class, 'tas')]//input", type: SelectorType.XPath));
			selectorDict.Add("WaElement", (selector: "//div[contains(@class, 'wa')]//input", type: SelectorType.XPath));
			selectorDict.Add("SaElement", (selector: "//div[contains(@class, 'sa')]//input", type: SelectorType.XPath));
			selectorDict.Add("NtElement", (selector: "//div[contains(@class, 'nt')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("DocumentcategoryElement", (selector: ".input-group__dropdown.documentCategoryId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement DocumentCategoryElement => FindElementExt("DocumentCategoryElement");

		//Attribute web Elements
		private IWebElement FileElement => FindElementExt("FileElement");
		private IWebElement NameElement => FindElementExt("NameElement");
		private IWebElement QldElement => FindElementExt("QldElement");
		private IWebElement NswElement => FindElementExt("NswElement");
		private IWebElement VicElement => FindElementExt("VicElement");
		private IWebElement TasElement => FindElementExt("TasElement");
		private IWebElement WaElement => FindElementExt("WaElement");
		private IWebElement SaElement => FindElementExt("SaElement");
		private IWebElement NtElement => FindElementExt("NtElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"File" => FileHeaderTitle,
				"Name" => NameHeaderTitle,
				"QLD" => QldHeaderTitle,
				"NSW" => NswHeaderTitle,
				"VIC" => VicHeaderTitle,
				"TAS" => TasHeaderTitle,
				"WA" => WaHeaderTitle,
				"SA" => SaHeaderTitle,
				"NT" => NtHeaderTitle,
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
				case "QLD":
					return QldElement;
				case "NSW":
					return NswElement;
				case "VIC":
					return VicElement;
				case "TAS":
					return TasElement;
				case "WA":
					return WaElement;
				case "SA":
					return SaElement;
				case "NT":
					return NtElement;
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
				case "QLD":
					SetQld(bool.Parse(value));
					break;
				case "NSW":
					SetNsw(bool.Parse(value));
					break;
				case "VIC":
					SetVic(bool.Parse(value));
					break;
				case "TAS":
					SetTas(bool.Parse(value));
					break;
				case "WA":
					SetWa(bool.Parse(value));
					break;
				case "SA":
					SetSa(bool.Parse(value));
					break;
				case "NT":
					SetNt(bool.Parse(value));
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
				"QLD" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.qld > div > p"),
				"NSW" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.nsw > div > p"),
				"VIC" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.vic > div > p"),
				"TAS" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.tas > div > p"),
				"WA" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.wa > div > p"),
				"SA" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.sa > div > p"),
				"NT" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.nt > div > p"),
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
			SetFile(_importantDocumentEntity.FileId.ToString());
			SetName(_importantDocumentEntity.Name);
			SetQld(_importantDocumentEntity.Qld);
			SetNsw(_importantDocumentEntity.Nsw);
			SetVic(_importantDocumentEntity.Vic);
			SetTas(_importantDocumentEntity.Tas);
			SetWa(_importantDocumentEntity.Wa);
			SetSa(_importantDocumentEntity.Sa);
			SetNt(_importantDocumentEntity.Nt);

			SetDocumentCategoryId(_importantDocumentEntity.DocumentCategoryId?.ToString());
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "documentcategory":
					return new List<Guid>() {GetDocumentCategoryId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetDocumentCategoryId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, DocumentCategoryIdInputElementBy, ElementState.VISIBLE);
			var documentCategoryIdInputElement = _driver.FindElementExt(DocumentCategoryIdInputElementBy);

			if (id != null)
			{
				documentCategoryIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				documentCategoryIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetDocumentCategoryId()
		{
			WaitUtils.elementState(_driverWait, DocumentCategoryIdElementBy, ElementState.VISIBLE);
			var documentCategoryIdElement = _driver.FindElementExt(DocumentCategoryIdElementBy);
			return new Guid(documentCategoryIdElement.GetAttribute("data-id"));
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

		private void SetQld (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (QldElement.Selected != boolValue) {
					QldElement.Click();
				}
			}
		}

		private Boolean? GetQld =>
			QldElement.Selected;

		private void SetNsw (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (NswElement.Selected != boolValue) {
					NswElement.Click();
				}
			}
		}

		private Boolean? GetNsw =>
			NswElement.Selected;

		private void SetVic (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (VicElement.Selected != boolValue) {
					VicElement.Click();
				}
			}
		}

		private Boolean? GetVic =>
			VicElement.Selected;

		private void SetTas (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (TasElement.Selected != boolValue) {
					TasElement.Click();
				}
			}
		}

		private Boolean? GetTas =>
			TasElement.Selected;

		private void SetWa (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (WaElement.Selected != boolValue) {
					WaElement.Click();
				}
			}
		}

		private Boolean? GetWa =>
			WaElement.Selected;

		private void SetSa (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (SaElement.Selected != boolValue) {
					SaElement.Click();
				}
			}
		}

		private Boolean? GetSa =>
			SaElement.Selected;

		private void SetNt (Boolean? value)
		{
			if (value is bool boolValue)
			{
				if (NtElement.Selected != boolValue) {
					NtElement.Click();
				}
			}
		}

		private Boolean? GetNt =>
			NtElement.Selected;


	}
}