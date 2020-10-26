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
	public class NewsArticleEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By PromotedArticlesIdElementBy => By.XPath("//*[contains(@class, 'promotedArticles')]//div[contains(@class, 'dropdown__container')]");
		private static By PromotedArticlesIdInputElementBy => By.XPath("//*[contains(@class, 'promotedArticles')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly NewsArticleEntity _newsArticleEntity;

		//Attribute Header Titles
		private IWebElement HeadlineHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Headline']"));
		private IWebElement FeatureImageHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Feature Image']"));
		private IWebElement ContentHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Content']"));
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

		public NewsArticleEntityDetailSection(ContextConfiguration contextConfiguration, NewsArticleEntity newsArticleEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_newsArticleEntity = newsArticleEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("HeadlineElement", (selector: "//div[contains(@class, 'headline')]//input", type: SelectorType.XPath));
			selectorDict.Add("FeatureImageElement", (selector: "//div[contains(@class, 'featureImage')]//input", type: SelectorType.XPath));
			selectorDict.Add("ContentElement", (selector: "//div[contains(@class, 'content')]//input", type: SelectorType.XPath));
			selectorDict.Add("QldElement", (selector: "//div[contains(@class, 'qld')]//input", type: SelectorType.XPath));
			selectorDict.Add("NswElement", (selector: "//div[contains(@class, 'nsw')]//input", type: SelectorType.XPath));
			selectorDict.Add("VicElement", (selector: "//div[contains(@class, 'vic')]//input", type: SelectorType.XPath));
			selectorDict.Add("TasElement", (selector: "//div[contains(@class, 'tas')]//input", type: SelectorType.XPath));
			selectorDict.Add("WaElement", (selector: "//div[contains(@class, 'wa')]//input", type: SelectorType.XPath));
			selectorDict.Add("SaElement", (selector: "//div[contains(@class, 'sa')]//input", type: SelectorType.XPath));
			selectorDict.Add("NtElement", (selector: "//div[contains(@class, 'nt')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("PromotedarticlesElement", (selector: ".input-group__dropdown.promotedArticlesId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement PromotedArticlesElement => FindElementExt("PromotedArticlesElement");

		//Attribute web Elements
		private IWebElement HeadlineElement => FindElementExt("HeadlineElement");
		private IWebElement FeatureImageElement => FindElementExt("FeatureImageElement");
		private IWebElement ContentElement => FindElementExt("ContentElement");
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
				"Headline" => HeadlineHeaderTitle,
				"Feature Image" => FeatureImageHeaderTitle,
				"Content" => ContentHeaderTitle,
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
				case "Headline":
					return HeadlineElement;
				case "Feature Image":
					return FeatureImageElement;
				case "Content":
					return ContentElement;
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
				case "Headline":
					SetHeadline(value);
					break;
				case "Feature Image":
					SetFeatureImage(value);
					break;
				case "Content":
					SetContent(value);
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
				"Headline" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.headline > div > p"),
				"Feature Image" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.featureImage > div > p"),
				"Content" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.content > div > p"),
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
			// % protected region % [Configure entity application here] off begin
			SetHeadline(_newsArticleEntity.Headline);
			SetFeatureImage(_newsArticleEntity.FeatureImageId.ToString());
			SetContent(_newsArticleEntity.Content);
			SetQld(_newsArticleEntity.Qld);
			SetNsw(_newsArticleEntity.Nsw);
			SetVic(_newsArticleEntity.Vic);
			SetTas(_newsArticleEntity.Tas);
			SetWa(_newsArticleEntity.Wa);
			SetSa(_newsArticleEntity.Sa);
			SetNt(_newsArticleEntity.Nt);

			SetPromotedArticlesId(_newsArticleEntity.PromotedArticlesId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "promotedarticles":
					return new List<Guid>() {GetPromotedArticlesId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetPromotedArticlesId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, PromotedArticlesIdInputElementBy, ElementState.VISIBLE);
			var promotedArticlesIdInputElement = _driver.FindElementExt(PromotedArticlesIdInputElementBy);

			if (id != null)
			{
				promotedArticlesIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				promotedArticlesIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetPromotedArticlesId()
		{
			WaitUtils.elementState(_driverWait, PromotedArticlesIdElementBy, ElementState.VISIBLE);
			var promotedArticlesIdElement = _driver.FindElementExt(PromotedArticlesIdElementBy);
			return new Guid(promotedArticlesIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetHeadline (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "headline", value, _isFastText);
			HeadlineElement.SendKeys(Keys.Tab);
			HeadlineElement.SendKeys(Keys.Escape);
		}

		private String GetHeadline =>
			HeadlineElement.Text;

		private void SetFeatureImage (String value)
		{
			const string script = "document.querySelector('.featureImageId>div>input').removeAttribute('style')";
			var js = (IJavaScriptExecutor)driver;
			js.ExecuteScript(script);
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources/RedCircle.svg"));
			var fileUploadElement = driver.FindElementExt(By.CssSelector(".featureImageId>div>input"));
			fileUploadElement.SendKeys(path);
		}

		private String GetFeatureImage =>
			FeatureImageElement.Text;

		private void SetContent (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "content", value, _isFastText);
			ContentElement.SendKeys(Keys.Tab);
			ContentElement.SendKeys(Keys.Escape);
		}

		private String GetContent =>
			ContentElement.Text;

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


		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}