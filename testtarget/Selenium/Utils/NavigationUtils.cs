

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Utils
{
	internal static class NavigationUtils
	{
		/// <summary>
		/// Navigates to a given url and wait for the page to load
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="wait"></param>
		/// <param name="url"></param>
		public static void GoToUrl(IWebDriver driver, IWait<IWebDriver> wait, string url)
		{
			driver.Navigate().GoToUrl(url);
			WaitUtils.waitForPage(wait);
		}

		/// <summary>
		/// Refreshes the current page and wait for the reload to finish
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="wait"></param>
		public static void RefreshPage(IWebDriver driver, IWait<IWebDriver> wait)
		{
			driver.Navigate().Refresh();
			WaitUtils.waitForPage(wait);
		}
	}
}