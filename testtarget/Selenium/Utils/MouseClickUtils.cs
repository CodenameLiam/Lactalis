

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Enums;

namespace SeleniumTests.Utils
{
	internal static class MouseClickUtils
	{
		/// <summary>
		/// Find and click on an element given an identifier name and a path
		/// </summary>
		/// <param name="driver">The webdriver used to find the element</param>
		/// <param name="identifier">The name of the identifier used to find the element</param>
		/// <param name="path">The path to the element be clicked on</param>
		public static void ClickOnElement(IWebDriver driver, IWebElement element)
		{
			ScrollingUtils.scrollToElement(driver, element);
			element.Click();
		}

		/// <summary>
		/// Find and click on an element given an identifier name and a path
		/// </summary>
		/// <param name="driver">The webdriver used to find the element</param>
		/// <param name="identifier">The name of the identifier used to find the element</param>
		/// <param name="path">The path to the element be clicked on</param>
		public static void ClickOnElement(IWebDriver driver, IWait<IWebDriver> wait, By elementBy)
		{
			WaitUtils.elementState(wait, elementBy, ElementState.EXISTS);
			ScrollingUtils.scrollToElement(driver, elementBy);
			var element = driver.FindElement(elementBy);
			element.Click();
		}
	}
}