

using OpenQA.Selenium;
using SeleniumTests.PageObjects.BotWritten.CRUDPageObject.Modals;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.CRUDPageObject
{
	public class EntityOnPage : BaseSection
	{
		ed IWebElement ListElement { get; set; }
		ed ModalOnPage _modalOnPage;

		public EntityOnPage(ContextConfiguration contextConfiguration, IWebElement listElement) : base(contextConfiguration)
		{
			ListElement = listElement;
			_modalOnPage = new ModalOnPage(contextConfiguration);
		}

		public readonly By DeleteButtonBy = By.XPath(".//*[contains(@class,'icon-bin-full')]");
		public IWebElement DeleteButton => ListElement.FindElement(DeleteButtonBy);
		public readonly By EditButtonBy = By.XPath(".//*[contains(@class,'icon-edit')]");
		public IWebElement EditButton => ListElement.FindElement(EditButtonBy);
		public readonly By SelectCheckboxBy = By.XPath(".//td[@class='select-box']//input");
		public IWebElement SelectCheckbox => ListElement.FindElement(SelectCheckboxBy);
		public readonly By ViewButtonBy = By.XPath(".//*[contains(@class,'icon-look')]");
		public IWebElement ViewButton => ListElement.FindElement(ViewButtonBy);

		public void SelectItem(bool select)
		{
			if (SelectCheckbox.Selected != select)
			{
				SelectCheckbox.Click();
			}
		}

		public void EditItem() => EditButton.Click();

		public void DeleteItem()
		{
			DeleteButton.Click();
			_modalOnPage.ConfirmDeleteButton.Click();
		}
	}
}