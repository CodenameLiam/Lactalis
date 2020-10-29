

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;

namespace SeleniumTests.PageObjects
{
	internal class ToasterAlert
	{
		ed IWebDriver _driver;
		ed IWait<IWebDriver> _wait;

		public static By ToasterBy => By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']");
		public static By ToasterCloseButtonBy => By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']/button");

		public IWebElement Toaster => _driver.FindElementExt(By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']"));

		private IWebElement ToasterCloseButton => _driver.FindElement(By.XPath("//div[@class='Toastify__toast Toastify__toast--success alert alert__success']/button"));

		public IWebElement ToasterBody => _driver.FindElement(By.XPath("//div[@role='alert' and @class='Toastify__toast-body']"));

		public ToasterAlert(ContextConfiguration contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_wait = contextConfiguration.WebDriverWait;
		}

		public string GetToasterAlertMessage()
		{
			// Wait for the toaster body to display
			bool isDisplayedToasterBody = WaitUtils.elementState(_wait, By.XPath("//div[@role='alert' and @class='Toastify__toast-body']"), ElementState.VISIBLE);

			if (isDisplayedToasterBody)
			{
				return ToasterBody.Text;
			}
			return string.Empty;
		}

		public void CloseToasterAlert()
		{
			WaitUtils.elementState(_wait, ToasterCloseButtonBy, ElementState.VISIBLE);
			ToasterCloseButton.Click();
		}
	}
}

