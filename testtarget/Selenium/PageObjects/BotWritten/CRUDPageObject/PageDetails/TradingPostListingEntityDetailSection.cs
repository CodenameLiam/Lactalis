/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using EntityObject.Enums;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class TradingPostListingEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By FarmerIdElementBy => By.XPath("//*[contains(@class, 'farmer')]//div[contains(@class, 'dropdown__container')]");
		private static By FarmerIdInputElementBy => By.XPath("//*[contains(@class, 'farmer')]/div/input");
		private static By TradingPostCategoriessElementBy => By.XPath("//*[contains(@class, 'tradingPostCategories')]//div[contains(@class, 'dropdown__container')]/a");
		private static By TradingPostCategoriessInputElementBy => By.XPath("//*[contains(@class, 'tradingPostCategories')]/div/input");

		//FlatPickr Elements

		//Attribute Headers
		private readonly TradingPostListingEntity _tradingPostListingEntity;

		//Attribute Header Titles
		private IWebElement TitleHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Title']"));
		private IWebElement EmailHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Email']"));
		private IWebElement PhoneHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Phone']"));
		private IWebElement AdditionalInfoHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Additional Info']"));
		private IWebElement AddressLine1HeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Address Line 1']"));
		private IWebElement AddressLine2HeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Address Line 2']"));
		private IWebElement PostalCodeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Postal Code']"));
		private IWebElement ProductImageHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Product Image']"));
		private IWebElement PriceHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Price']"));
		private IWebElement PriceTypeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Price Type']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public TradingPostListingEntityDetailSection(ContextConfiguration contextConfiguration, TradingPostListingEntity tradingPostListingEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_tradingPostListingEntity = tradingPostListingEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("TitleElement", (selector: "//div[contains(@class, 'title')]//input", type: SelectorType.XPath));
			selectorDict.Add("EmailElement", (selector: "//div[contains(@class, 'email')]//input", type: SelectorType.XPath));
			selectorDict.Add("PhoneElement", (selector: "//div[contains(@class, 'phone')]//input", type: SelectorType.XPath));
			selectorDict.Add("AdditionalInfoElement", (selector: "//div[contains(@class, 'additionalInfo')]//input", type: SelectorType.XPath));
			selectorDict.Add("AddressLine1Element", (selector: "//div[contains(@class, 'addressLine1')]//input", type: SelectorType.XPath));
			selectorDict.Add("AddressLine2Element", (selector: "//div[contains(@class, 'addressLine2')]//input", type: SelectorType.XPath));
			selectorDict.Add("PostalCodeElement", (selector: "//div[contains(@class, 'postalCode')]//input", type: SelectorType.XPath));
			selectorDict.Add("ProductImageElement", (selector: "//div[contains(@class, 'productImage')]//input", type: SelectorType.XPath));
			selectorDict.Add("PriceElement", (selector: "//div[contains(@class, 'price')]//input", type: SelectorType.XPath));
			selectorDict.Add("PriceTypeElement", (selector: "//div[contains(@class, 'priceType')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("FarmerElement", (selector: ".input-group__dropdown.farmerId > .dropdown.dropdown__container", type: SelectorType.CSS));
			selectorDict.Add("TradingpostcategoriesElement", (selector: ".input-group__dropdown.tradingPostCategoriess > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement FarmerElement => FindElementExt("FarmerElement");

		//Attribute web Elements
		private IWebElement TitleElement => FindElementExt("TitleElement");
		private IWebElement EmailElement => FindElementExt("EmailElement");
		private IWebElement PhoneElement => FindElementExt("PhoneElement");
		private IWebElement AdditionalInfoElement => FindElementExt("AdditionalInfoElement");
		private IWebElement AddressLine1Element => FindElementExt("AddressLine1Element");
		private IWebElement AddressLine2Element => FindElementExt("AddressLine2Element");
		private IWebElement PostalCodeElement => FindElementExt("PostalCodeElement");
		private IWebElement ProductImageElement => FindElementExt("ProductImageElement");
		private IWebElement PriceElement => FindElementExt("PriceElement");
		private IWebElement PriceTypeElement => FindElementExt("PriceTypeElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Title" => TitleHeaderTitle,
				"Email" => EmailHeaderTitle,
				"Phone" => PhoneHeaderTitle,
				"Additional Info" => AdditionalInfoHeaderTitle,
				"Address Line 1" => AddressLine1HeaderTitle,
				"Address Line 2" => AddressLine2HeaderTitle,
				"Postal Code" => PostalCodeHeaderTitle,
				"Product Image" => ProductImageHeaderTitle,
				"Price" => PriceHeaderTitle,
				"Price Type" => PriceTypeHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Title":
					return TitleElement;
				case "Email":
					return EmailElement;
				case "Phone":
					return PhoneElement;
				case "Additional Info":
					return AdditionalInfoElement;
				case "Address Line 1":
					return AddressLine1Element;
				case "Address Line 2":
					return AddressLine2Element;
				case "Postal Code":
					return PostalCodeElement;
				case "Product Image":
					return ProductImageElement;
				case "Price":
					return PriceElement;
				case "Price Type":
					return PriceTypeElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Title":
					SetTitle(value);
					break;
				case "Email":
					SetEmail(value);
					break;
				case "Phone":
					SetPhone(value);
					break;
				case "Additional Info":
					SetAdditionalInfo(value);
					break;
				case "Address Line 1":
					SetAddressLine1(value);
					break;
				case "Address Line 2":
					SetAddressLine2(value);
					break;
				case "Postal Code":
					SetPostalCode(value);
					break;
				case "Product Image":
					SetProductImage(value);
					break;
				case "Price":
					int? price = null;
					if (int.TryParse(value, out var intPrice))
					{
						price = intPrice;
					}
					SetPrice(price);
					break;
				case "Price Type":
					SetPriceType((PriceType)Enum.Parse(typeof(PriceType), value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Title" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.title > div > p"),
				"Email" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.email > div > p"),
				"Phone" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.phone > div > p"),
				"Additional Info" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.additionalInfo > div > p"),
				"Address Line 1" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.addressLine1 > div > p"),
				"Address Line 2" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.addressLine2 > div > p"),
				"Postal Code" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.postalCode > div > p"),
				"Product Image" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.productImage > div > p"),
				"Price" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.price > div > p"),
				"Price Type" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.priceType > div > p"),
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
			// % protected region % [Configure entity application here] off begin
			SetTitle(_tradingPostListingEntity.Title);
			SetEmail(_tradingPostListingEntity.Email);
			SetPhone(_tradingPostListingEntity.Phone);
			SetAdditionalInfo(_tradingPostListingEntity.AdditionalInfo);
			SetAddressLine1(_tradingPostListingEntity.AddressLine1);
			SetAddressLine2(_tradingPostListingEntity.AddressLine2);
			SetPostalCode(_tradingPostListingEntity.PostalCode);
			SetProductImage(_tradingPostListingEntity.ProductImageId.ToString());
			SetPrice(_tradingPostListingEntity.Price);
			SetPriceType(_tradingPostListingEntity.PriceType);

			SetFarmerId(_tradingPostListingEntity.FarmerId?.ToString());
			if (_tradingPostListingEntity.TradingPostCategoriesIds != null)
			{
				SetTradingPostCategoriess(_tradingPostListingEntity.TradingPostCategoriesIds.Select(x => x.ToString()));
			}
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "farmer":
					return new List<Guid>() {GetFarmerId()};
				case "tradingpostcategories":
					return GetTradingPostCategoriess();
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetFarmerId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, FarmerIdInputElementBy, ElementState.VISIBLE);
			var farmerIdInputElement = _driver.FindElementExt(FarmerIdInputElementBy);

			if (id != null)
			{
				farmerIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				farmerIdInputElement.SendKeys(Keys.Return);
			}
		}
		private void SetTradingPostCategoriess(IEnumerable<string> ids)
		{
			WaitUtils.elementState(_driverWait, TradingPostCategoriessInputElementBy, ElementState.VISIBLE);
			var tradingPostCategoriessInputElement = _driver.FindElementExt(TradingPostCategoriessInputElementBy);

			foreach(var id in ids)
			{
				tradingPostCategoriessInputElement.SendKeys(id);
				WaitForDropdownOptions();
				tradingPostCategoriessInputElement.SendKeys(Keys.Return);
			}
		}


		// get associations
		private Guid GetFarmerId()
		{
			WaitUtils.elementState(_driverWait, FarmerIdElementBy, ElementState.VISIBLE);
			var farmerIdElement = _driver.FindElementExt(FarmerIdElementBy);
			return new Guid(farmerIdElement.GetAttribute("data-id"));
		}
		private List<Guid> GetTradingPostCategoriess()
		{
			var guids = new List<Guid>();
			WaitUtils.elementState(_driverWait, TradingPostCategoriessElementBy, ElementState.VISIBLE);
			var tradingPostCategoriessElement = _driver.FindElements(TradingPostCategoriessElementBy);

			foreach(var element in tradingPostCategoriessElement)
			{
				guids.Add(new Guid (element.GetAttribute("data-id")));
			}
			return guids;
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetTitle (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "title", value, _isFastText);
			TitleElement.SendKeys(Keys.Tab);
			TitleElement.SendKeys(Keys.Escape);
		}

		private String GetTitle =>
			TitleElement.Text;

		private void SetEmail (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "email", value, _isFastText);
			EmailElement.SendKeys(Keys.Tab);
			EmailElement.SendKeys(Keys.Escape);
		}

		private String GetEmail =>
			EmailElement.Text;

		private void SetPhone (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "phone", value, _isFastText);
			PhoneElement.SendKeys(Keys.Tab);
			PhoneElement.SendKeys(Keys.Escape);
		}

		private String GetPhone =>
			PhoneElement.Text;

		private void SetAdditionalInfo (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "additionalInfo", value, _isFastText);
			AdditionalInfoElement.SendKeys(Keys.Tab);
			AdditionalInfoElement.SendKeys(Keys.Escape);
		}

		private String GetAdditionalInfo =>
			AdditionalInfoElement.Text;

		private void SetAddressLine1 (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "addressLine1", value, _isFastText);
			AddressLine1Element.SendKeys(Keys.Tab);
			AddressLine1Element.SendKeys(Keys.Escape);
		}

		private String GetAddressLine1 =>
			AddressLine1Element.Text;

		private void SetAddressLine2 (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "addressLine2", value, _isFastText);
			AddressLine2Element.SendKeys(Keys.Tab);
			AddressLine2Element.SendKeys(Keys.Escape);
		}

		private String GetAddressLine2 =>
			AddressLine2Element.Text;

		private void SetPostalCode (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "postalCode", value, _isFastText);
			PostalCodeElement.SendKeys(Keys.Tab);
			PostalCodeElement.SendKeys(Keys.Escape);
		}

		private String GetPostalCode =>
			PostalCodeElement.Text;

		private void SetProductImage (String value)
		{
			const string script = "document.querySelector('.productImageId>div>input').removeAttribute('style')";
			var js = (IJavaScriptExecutor)driver;
			js.ExecuteScript(script);
			var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources/RedCircle.svg"));
			var fileUploadElement = driver.FindElementExt(By.CssSelector(".productImageId>div>input"));
			fileUploadElement.SendKeys(path);
		}

		private String GetProductImage =>
			ProductImageElement.Text;

		private void SetPrice (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "price", intValue.ToString(), _isFastText);
			}
		}

		private int? GetPrice =>
			int.Parse(PriceElement.Text);

		private void SetPriceType (PriceType value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "priceType", value.ToString(), _isFastText);
		}

		private PriceType GetPriceType =>
			(PriceType)Enum.Parse(typeof(PriceType), PriceTypeElement.Text);
			

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}