
using System;
using System.Linq;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class AdminEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements

		//FlatPickr Elements

		//Attribute Headers
		private readonly AdminEntity _adminEntity;

		//Attribute Header Titles

		// User Entity specific web Elements
		private IWebElement UserEmailElement => FindElementExt("UserEmailElement");
		private IWebElement UserPasswordElement => FindElementExt("UserPasswordElement");
		private IWebElement UserConfirmPasswordElement => FindElementExt("UserConfirmPasswordElement");
		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public AdminEntityDetailSection(ContextConfiguration contextConfiguration, AdminEntity adminEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_adminEntity = adminEntity;

			InitializeSelectors();
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements

			// Reference web elements

			// User Entity specific web Elements
			selectorDict.Add("UserEmailElement", (selector: "div.email > input", type: SelectorType.CSS));
			selectorDict.Add("UserPasswordElement", (selector: "div.password> input", type: SelectorType.CSS));
			selectorDict.Add("UserConfirmPasswordElement", (selector: "div._confirmPassword > input", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = GetErrorAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public void Apply()
		{
			if (_driver.Url == $"{_contextConfiguration.BaseUrl}/admin/adminentity/create")
			{
				SetUserFields(_adminEntity);
			}
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations

		// get associations

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		// set the email, password and confirm password fields
		private void SetUserFields(AdminEntity adminEntity)
		{
			UserEmailElement.SendKeys(adminEntity.EmailAddress);
			UserPasswordElement.SendKeys(adminEntity.Password);
			UserConfirmPasswordElement.SendKeys(adminEntity.Password);
		}

	}
}