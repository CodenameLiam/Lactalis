

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumTests.Utils
{
	internal static class ScrollingUtils
	{
		/// <summary>
		/// Will find and scroll to an element given an OpenQA.Selenium.By identifier
		/// </summary>
		/// <param name="driver">The webdriver to use to control the page</param>
		/// <param name="elementBy">The OpenQA.Selenium.By element identifier</param>
		public static void scrollToElement(IWebDriver driver, IWebElement element)
		{
			var actions = new Actions(driver);
			actions.MoveToElement(element);
			actions.Perform();
		}

		/// <summary>
		/// Will find and scroll to an element given an OpenQA.Selenium.By identifier
		/// </summary>
		/// <param name="driver">The webdriver to use to control the page</param>
		/// <param name="elementBy">The OpenQA.Selenium.By element identifier</param>
		public static void scrollToElement(IWebDriver driver, By elementBy)
		{
			var element = driver.FindElement(elementBy);
			var actions = new Actions(driver);
			actions.MoveToElement(element);
			actions.Perform();
		}

		/// <summary>
		/// Will scroll the page up or down given an amount of pixels to move by
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="pixelDistance"></param>
		public static void scrollUpOrDown(IWebDriver driver, int pixelDistance)
		{
			var js = (IJavaScriptExecutor)driver;
			js.ExecuteScript($"window.scrollBy(0,{pixelDistance})");
		}
	}
}