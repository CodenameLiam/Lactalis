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
using EntityObject.Enums;
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
	public class PromotedArticlesEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By NewsArticlessElementBy => By.XPath("//*[contains(@class, 'newsArticles')]//div[contains(@class, 'dropdown__container')]/a");
		private static By NewsArticlessInputElementBy => By.XPath("//*[contains(@class, 'newsArticles')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly PromotedArticlesEntity _promotedArticlesEntity;

		//Attribute Header Titles
		private IWebElement StateHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='State']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public PromotedArticlesEntityDetailSection(ContextConfiguration contextConfiguration, PromotedArticlesEntity promotedArticlesEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_promotedArticlesEntity = promotedArticlesEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("StateElement", (selector: "//div[contains(@class, 'state')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("NewsarticlesElement", (selector: ".input-group__dropdown.newsArticless > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement StateElement => FindElementExt("StateElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"State" => StateHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
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
			// % protected region % [Configure entity application here] off begin
			SetState(_promotedArticlesEntity.State);

			if (_promotedArticlesEntity.NewsArticlesIds != null)
			{
				SetNewsArticless(_promotedArticlesEntity.NewsArticlesIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "newsarticles":
					return GetNewsArticless();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetNewsArticless(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, NewsArticlessInputElementBy, ElementState.VISIBLE);
			var newsArticlessInputElement = _driver.FindElementExt(NewsArticlessInputElementBy);

			foreach(var id in ids)
			{
				newsArticlessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				newsArticlessInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private List<Guid> GetNewsArticless()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, NewsArticlessElementBy, ElementState.VISIBLE);
			var newsArticlessElement = _driver.FindElements(NewsArticlessElementBy);

			foreach(var element in newsArticlessElement)
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

		private void SetState (State value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "state", value.ToString(), _isFastText);
		}

		private State GetState =>
			(State)Enum.Parse(typeof(State), StateElement.Text);
			

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}