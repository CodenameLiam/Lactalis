

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Utils
{
	public static class ExtendedWebDriver
	{
		private const int timeoutInSeconds = 5; //Change to confid timeout

		public static IWebElement FindElementExt(this IWebDriver driver, By by) // Change to use a new class which is an extended webDriver and add these there as the default method
		{
			if (timeoutInSeconds > 0)
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
				return wait.Until(drv => drv.FindElements(by).FirstOrDefault());
			}
		}

		public static IEnumerable<IWebElement> FindElementsExt(this IWebDriver driver, By by)
		{
			if (timeoutInSeconds > 0)
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
				wait.Until(drv => drv.FindElements(by).Any());
				return driver.FindElements(by);
			}
		}

		public static void GoToUrlExt(this IWebDriver driver, string url)
		{
			driver.Navigate().GoToUrl(url);
			new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drvr =>
				drvr.Url.Equals(url));
		}
	}
}