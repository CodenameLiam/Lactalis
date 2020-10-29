

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Utils
{
	public static class WebElementExtensions
	{
		/// <summary>
		/// Attempts to click on a web element, if a 'NoSuchElementException' occurs,
		/// will wait and try again.
		/// </summary>
		/// <param name="webElement"></param>
		/// <param name="wait"></param>
		public static void ClickWithWait(this IWebElement webElement, IWait<IWebDriver> wait)
		{
			wait.Until(driver =>
			{
				try
				{
					webElement.Click();
					return true;
				}
				catch (Exception e)
				{
					if (e is NoSuchElementException || e is ElementNotInteractableException)
					{
						return false;
					}
					throw;
				}
			});
		}
		
		/// <summary>
		/// Attempts to send keys to a web elements, if a 'NoSuchElementException' occurs,
		/// will wait and try again.
		/// </summary>
		/// <param name="webElement"></param>
		/// <param name="wait"></param>
		/// <param name="keys"></param>
		public static void SendKeysWithWait(this IWebElement webElement, IWait<IWebDriver> wait, string keys)
		{
			wait.Until(driver =>
			{
				try
				{
					webElement.SendKeys(keys);
					return true;
				}
				catch (NoSuchElementException)
				{
					return false;
				}
			});
		}
	}
}