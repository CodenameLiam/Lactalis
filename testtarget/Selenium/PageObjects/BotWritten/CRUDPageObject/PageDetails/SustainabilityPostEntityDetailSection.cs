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
	public class SustainabilityPostEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements

		//FlatPickr Elements

		//Attribute Headers
		private readonly SustainabilityPostEntity _sustainabilityPostEntity;

		//Attribute Header Titles
		private IWebElement TitleHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Title']"));
		private IWebElement ImageHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Image']"));
		private IWebElement FileHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='File']"));
		private IWebElement ContentHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Content']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public SustainabilityPostEntityDetailSection(ContextConfiguration contextConfiguration, SustainabilityPostEntity sustainabilityPostEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_sustainabilityPostEntity = sustainabilityPostEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("TitleElement", (selector: "//div[contains(@class, 'title')]//input", type: SelectorType.XPath));
			selectorDict.Add("ImageElement", (selector: "//div[contains(@class, 'image')]//input", type: SelectorType.XPath));
			selectorDict.Add("FileElement", (selector: "//div[contains(@class, 'file')]//input", type: SelectorType.XPath));
			selectorDict.Add("ContentElement", (selector: "//div[contains(@class, 'content')]//input", type: SelectorType.XPath));

			// Reference web elements

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement TitleElement => FindElementExt("TitleElement");
		private IWebElement ImageElement => FindElementExt("ImageElement");
		private IWebElement FileElement => FindElementExt("FileElement");
		private IWebElement ContentElement => FindElementExt("ContentElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Title" => TitleHeaderTitle,
				"Image" => ImageHeaderTitle,
				"File" => FileHeaderTitle,
				"Content" => ContentHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Title":
					return TitleElement;
				case "Image":
					return ImageElement;
				case "File":
					return FileElement;
				case "Content":
					return ContentElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Title":
					SetTitle(value);
					break;
				case "Image":
					SetImage(value);
					break;
				case "File":
					SetFile(value);
					break;
				case "Content":
					SetContent(value);
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Title" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.title > div > p"),
				"Image" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.image > div > p"),
				"File" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.file > div > p"),
				"Content" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.content > div > p"),
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
			SetTitle(_sustainabilityPostEntity.Title);
			SetImage(_sustainabilityPostEntity.ImageId.ToString());
			SetFile(_sustainabilityPostEntity.FileId.ToString());
			SetContent(_sustainabilityPostEntity.Content);

			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations

		// get associations

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetTitle (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "title", value, _isFastText);
			TitleElement.SendKeys(Keys.Tab);
			TitleElement.SendKeys(Keys.Escape);
		}

		private String GetTitle =>
			TitleElement.Text;

		private void SetImage (String value)
		{
			const string script = "document.querySelector('.imageId>div>input').removeAttribute('style')";
			var js = (IJavaScriptExecutor)driver;
			js.ExecuteScript(script);
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources/RedCircle.svg"));
			var fileUploadElement = driver.FindElementExt(By.CssSelector(".imageId>div>input"));
			fileUploadElement.SendKeys(path);
		}

		private String GetImage =>
			ImageElement.Text;

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

		private void SetContent (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "content", value, _isFastText);
			ContentElement.SendKeys(Keys.Tab);
			ContentElement.SendKeys(Keys.Escape);
		}

		private String GetContent =>
			ContentElement.Text;


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}