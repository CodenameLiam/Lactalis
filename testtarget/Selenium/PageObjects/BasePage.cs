

using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Setup;
using Xunit;

namespace SeleniumTests.PageObjects
{
	public interface IBasePage
	{
		string Url { get; set; }

		BasePage AssertOnPage();
		bool ElementExists(string element);
		List<IWebElement> GetReadonlyInputFieldAttributes();
		By GetWebElementBy(string elementName);
		BasePage Navigate();
	}

	///<summary>
	///The Base page object, every page is extended from this page, contains information shared across every page
	///</summary>
	public class BasePage : BaseSection, IBasePage
	{
		ed readonly string baseUrl;
		ed IWait<IWebDriver> driverWait;
		public virtual string Url { get; set; }
		ed ContextConfiguration contextConfiguration;

		public BasePage(ContextConfiguration currentContext) : base (currentContext)
		{
			contextConfiguration = currentContext;
			baseUrl = contextConfiguration.BaseUrl;
			driver = contextConfiguration.WebDriver;
			driverWait = contextConfiguration.WebDriverWait;
		}


		//goto Home -> go's to base URL
		public BasePage Navigate()
		{
			driver.Navigate().GoToUrl(Url);
			return this;
		}

		/* Compares the url of the driver to the url of this page object after stripping
		 * any trailing whitespaces and forward slashes. */
		public BasePage AssertOnPage() {
			var thisUrl = Url.Trim('/');
			driverWait.Until(driver => driver.Url.Trim('/') == thisUrl);
			Assert.Equal(driver.Url.Trim('/'), thisUrl);
			return this;
		}

		ed IEnumerable<IWebElement> GetAllReadOnlyElements() => selectorDict.Where(e => e.Key != "UserPasswordElement" && e.Key != "UserConfirmPasswordElement").Select(e => FindElementExt(e.Key));

		public List<IWebElement> GetReadonlyInputFieldAttributes()
		{
			driverWait.Until(_ => GetAllReadOnlyElements().ToList().Count > 0);
			return GetAllReadOnlyElements().ToList();
		}
	}
}
