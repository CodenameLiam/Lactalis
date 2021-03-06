

using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.BotWritten.Forms
{
	public class FormsLandingPage : BasePage
	{
		public override string Url => baseUrl + "/admin/forms";

		public FormsLandingPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			
		}

		public IEnumerable<string> GetAccordionFormHeaderNames() => 
			AccordionHeaders().Select(x => x.Text);

		public IEnumerable<string> GetFormItemNames() =>
			FormItems().Select(x => x.Text);

		private IEnumerable<IWebElement> AccordionHeaders() =>
			driver.FindElements(By.CssSelector(".accordion > button"));

		private IEnumerable<IWebElement> NewFormButtons() => 
			driver.FindElements(By.CssSelector(".form-item__new > h3"));

		private IEnumerable<IWebElement> FormItems() =>
			driver.FindElements(By.CssSelector("div.form-item__heading"));

		public void ToggleAccordionWithWait(string formName)
		{
			var success = driverWait.Until(_ => ToggleAccordion(formName));
			contextConfiguration.TestOutputHelper.WriteLine(success
				? $"Successfully found and toggled the {formName} accordion"
				: $"Failed to toggle the {formName} accordion");
		}

		private bool ToggleAccordion(string formName)
		{
			var formHeader = AccordionHeaders().FirstOrDefault(x => x.Text == formName);

			if (formHeader != null)
			{
				formHeader.Click();
				return true;
			}
			return false;
		}

		public void ClickNewFormItemWithWait(string formName)
		{
			var success = driverWait.Until(_ => ClickNewFormItem(formName));
			contextConfiguration.TestOutputHelper.WriteLine(success
				? $"Successfully found and clicked new form item {formName}"
				: $"Failed to find and click new form item {formName}");
		}

		private bool ClickNewFormItem(string formName)
		{
			var createButtonNames = NewFormButtons().Select(x => x.Text);
			
			if (!createButtonNames.Contains($"New {formName}"))
			{
				ToggleAccordion(formName);
			}

			var newFormButton = NewFormButtons().FirstOrDefault(x => x.Text == $"New {formName}");

			if (newFormButton != null)
			{
				newFormButton.Click();
				return true;
			}
			return false;
		}

		public void ClickFormItemWithWait(string formInstanceName)
		{
			var success = driverWait.Until(_ => ClickFormItem(formInstanceName));
			contextConfiguration.TestOutputHelper.WriteLine(success
				? $"Successfully found and clicked form item {formInstanceName}"
				: $"Failed to find and click form item {formInstanceName}");
		}

		private bool ClickFormItem(string formInstanceName)
		{
			var formItem = FormItems()
				.FirstOrDefault(x => x.FindElement(By.CssSelector("h3")).Text == formInstanceName);
			if (formItem == null)
			{
				return false;
			}
			formItem.Click();
			return true;
		}
	}
}
