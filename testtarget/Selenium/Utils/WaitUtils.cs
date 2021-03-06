

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Enums;

namespace SeleniumTests.Utils
{
	internal static class WaitUtils
	{
		public static void waitForPage(IWait<IWebDriver> wait)
		{
			wait.Until(d => ((IJavaScriptExecutor) d).ExecuteScript("return document.readyState").Equals("complete"));
		}

		public static void waitForPage(IWait<IWebDriver> wait, string url)
		{
			wait.Until(d => d.Url == url);
			waitForPage(wait);
		}

		/// <summary>
		/// A wait util method to use when waiting for an element to be in a state
		/// </summary>
		/// <param name="wait">The preconfigured driver wait to use</param>
		/// <param name="elementPath">The OpenQA.Selenium.By used to locate the element</param>
		/// <param name="status">visible, not visible, exists, not exist</param>
		/// <returns> a boolean indicating if the expected status is true or false</returns>
		public static bool elementState(IWait<IWebDriver> wait, By elementPath, ElementState status) =>
			waitForElementRunner(wait, null, elementPath, status);

		/// <summary>
		/// A wait util method to use when waiting for an element to be in a state
		/// </summary>
		/// <param name="wait">The preconfigured driver wait to use</param>
		/// <param name="parentElement">the parent of the element to wait for</param>
		/// <param name="elementPath">The OpenQA.Selenium.By used to locate the element</param>
		/// <param name="status">visible, not visible, exist, not exist</param>
		/// <returns> a boolean indicating if the expected status is true or false</returns>
		public static bool elementState(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath, ElementState status) =>
			waitForElementRunner(wait, parentElement, elementPath, status);

		private static bool waitForElementRunner(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath,
			ElementState status)
		{
			try
			{
				waitForElement(wait, parentElement, elementPath, status);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		private static void waitForElement(IWait<IWebDriver> wait, IWebElement parentElement, By elementPath, ElementState status)
		{
			switch (status)
			{
				case ElementState.VISIBLE:
					wait.Until(driver =>
					{
						try
						{
							return parentElement != null
								? parentElement.FindElement(elementPath).Displayed
								: driver.FindElement(elementPath).Displayed;
						}
						catch (NoSuchElementException)
						{
							return false;
						}
					});
					break;
				case ElementState.NOT_VISIBLE:
					wait.Until(driver =>
					{
						try
						{
							return parentElement != null
								? !parentElement.FindElement(elementPath).Displayed
								: !driver.FindElement(elementPath).Displayed;
						}
						catch (NoSuchElementException)
						{
							return true;
						}
					});
					break;
				case ElementState.EXISTS:
					wait.Until(driver =>
					{
						try
						{
							var element = parentElement != null
								? parentElement.FindElement(elementPath)
								: driver.FindElement(elementPath);

							return true;
						}
						catch (NoSuchElementException)
						{
							return false;
						}
					});
					break;
				case ElementState.NOT_EXIST:
					wait.Until(driver =>
					{
						try
						{
							var element = parentElement != null
								? parentElement.FindElement(elementPath)
								: driver.FindElement(elementPath);

							return false;
						}
						catch (NoSuchElementException)
						{
							return true;
						}
					});
					break;
				default:
					throw new Exception($" '{status}' is not a valid status to wait for element");
			}
		}
	}
}