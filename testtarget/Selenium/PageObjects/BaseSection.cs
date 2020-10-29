

using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects
{
	///<summary>
	///The base section page, has functionalit requried by all other sections
	///</summary>
	public class BaseSection
	{
		ed IWebDriver driver;
		ed bool isFastText;

		/*
		 * The web element selector type enum,
		 * used for selecting and interacting with web elements
		 */
		ed enum SelectorType
		{
			CSS,
			XPath,
			ID
		}

		ed IDictionary<string, (string selector, SelectorType type)> selectorDict = new Dictionary<string, (string, SelectorType)>();

		// check that the entity appears on the page
		public bool ElementExists(string element)
		{
			try
			{
				FindElementExt(element);
				return true;
			}
			catch
			{
				return false;
			}
		}

		ed IWebElement FindElementExt(IWebElement baseElement, string elementName)
		{
			var selector = selectorDict[elementName];
			var elementSelector = (selector.type == SelectorType.CSS) ?
				By.CssSelector(selector.selector) : By.XPath(selector.selector);
			return baseElement.FindElement(elementSelector);
		}

		ed IWebElement FindElementExt(string elementName)
		{
			var selector = selectorDict[elementName];
			By elementSelector = null;

			switch (selector.type)
			{
				case SelectorType.CSS:
					elementSelector = By.CssSelector(selector.selector);
					break;
				case SelectorType.ID:
					elementSelector = By.Id(selector.selector);
					break;
				case SelectorType.XPath:
					elementSelector = By.XPath(selector.selector);
					break;
			}
			return driver.FindElementExt(elementSelector);
		}

		public By GetWebElementBy(string elementName)
		{
			var selector = selectorDict[elementName];

			return selector.type switch
			{
				SelectorType.CSS => By.CssSelector(selector.selector),
				SelectorType.XPath => By.XPath(selector.selector),
				_ => null,
			};
		}

		public BaseSection(ContextConfiguration contextConfiguration)
		{
			driver = contextConfiguration.WebDriver;
			isFastText = contextConfiguration.SeleniumSettings.FastText;
		}
	}
}
