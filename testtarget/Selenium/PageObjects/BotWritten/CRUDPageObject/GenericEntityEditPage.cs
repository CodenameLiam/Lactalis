

using OpenQA.Selenium;
using SeleniumTests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.CRUDPageObject
{
	public class GenericEntityEditPage : CrudGenericEntityPage
	{
		private readonly string _entityName;

		public IDetailSection detailsSection;

		public GenericEntityEditPage(string entityName, ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			Url = baseUrl + "/admin/" +entityName +"/edit";
			_entityName = entityName;
			detailsSection = EntityDetailUtils.GetEntityDetailsSection(entityName, contextConfiguration);
		}

		public GenericEntityEditPage(ContextConfiguration contextConfiguration) : base(contextConfiguration){}

		public void Fill()
		{
			var factory = new EntityDetailFactory(contextConfiguration);
			factory.ApplyDetails(_entityName, true);
		}

		public void Cancel() => CancelButton.ClickWithWait(driverWait);
		public void Submit() => SubmitButton.ClickWithWait(driverWait);

		// Created/Modified Datepickers
		public IWebElement CreateAtDatepickerField => driver.FindElementExt(By.XPath("//label[text()='Created']/following-sibling::input[@class='flatpickr-input']"));
		public IWebElement ModifiedAtDatepickerField => driver.FindElementExt(By.XPath("//label[text()='Modified']/following-sibling::input[@class='flatpickr-input']"));
	}
}