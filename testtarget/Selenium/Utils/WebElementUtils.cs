

using System;
using OpenQA.Selenium;
using SeleniumTests.Enums;

namespace SeleniumTests.Utils
{
	public static class WebElementUtils
	{
		public static bool CheckWebElementExists(IWebElement webElement)
		{
			try
			{
				return webElement.Displayed;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		/// <summary>
		/// Check if WebElement is readonly
		/// </summary>
		/// <param name="webElement"></param>
		/// <returns></returns>
		public static bool IsReadonly(IWebElement webElement) => bool.Parse(webElement.GetAttribute("readonly"));

		/// <summary>
		/// Returns an OpenQA.Selenium.By that can be used to locate an IWebElement with an IWebDriver
		/// </summary>
		/// <param name="identifier">XPath, CssSelector, ClassName, Id</param>
		/// <param name="path">The path to the element</param>
		/// <returns></returns>
		public static By GetElementAsBy(SelectorPathType identifier, string path)
		{
			switch (identifier)
			{
				case SelectorPathType.XPATH:
					return By.XPath(path);
				case SelectorPathType.CSS:
				case SelectorPathType.CSS_SELECTOR:
					return By.CssSelector(path);
				case SelectorPathType.ClASSNAME:
					return By.ClassName(path);
				case SelectorPathType.ID:
					return By.Id(path);
				default:
					throw new Exception($"Cannot find Identifier named {identifier}, please check you are using a valid selector name");
			}
		}
	}
}