

using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.CRUDPageObject
{
	public class CrudGenericEntityPage : AdminAuthenticatedPage
	{
		public CrudGenericEntityPage(ContextConfiguration contextConfiguration) : base(contextConfiguration){ }
		public IWebElement CancelButton => driver.FindElementExt(By.XPath("//button[contains(text(), 'Cancel')]"));
		public IWebElement SubmitButton => driver.FindElementExt(By.XPath("//button[@type='submit']"));
	}
}
