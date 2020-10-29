

using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten.CRUDPageObject.Modals
{
	 public class ModalOnPage
	{
		private readonly IWebDriver _driver;

		public ModalOnPage(ContextConfiguration contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
		}

		public IWebElement ModalElement => _driver.FindElementExt(By.XPath("//*[contains(@class,'ReactModal__Content ReactModal__Content--after-open modal-content confirm-modal')]"));
		public IWebElement CloseModalButtton => _driver.FindElementExt(By.ClassName("modal--close"));
		public IWebElement ConfirmDeleteButton => _driver.FindElementExt(By.ClassName("modal--confirm"));
		public IWebElement CancelDeleteButton => _driver.FindElementExt(By.ClassName("modal--cancel"));
	}
}