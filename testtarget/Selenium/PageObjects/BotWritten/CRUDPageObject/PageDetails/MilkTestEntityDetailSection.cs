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
using System.Collections.Generic;
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
	public class MilkTestEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements
		private static By FarmIdElementBy => By.XPath("//*[contains(@class, 'farm')]//div[contains(@class, 'dropdown__container')]");
		private static By FarmIdInputElementBy => By.XPath("//*[contains(@class, 'farm')]/div/input");

		//FlatPickr Elements
		private DateTimePickerComponent TimeElement => new DateTimePickerComponent(_contextConfiguration, "time");

		//Attribute Headers
		private readonly MilkTestEntity _milkTestEntity;

		//Attribute Header Titles
		private IWebElement TimeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Time']"));
		private IWebElement VolumeHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Volume']"));
		private IWebElement TemperatureHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Temperature']"));
		private IWebElement MilkFatHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Milk Fat']"));
		private IWebElement ProteinHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='Protein']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public MilkTestEntityDetailSection(ContextConfiguration contextConfiguration, MilkTestEntity milkTestEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_milkTestEntity = milkTestEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("VolumeElement", (selector: "//div[contains(@class, 'volume')]//input", type: SelectorType.XPath));
			selectorDict.Add("TemperatureElement", (selector: "//div[contains(@class, 'temperature')]//input", type: SelectorType.XPath));
			selectorDict.Add("MilkFatElement", (selector: "//div[contains(@class, 'milkFat')]//input", type: SelectorType.XPath));
			selectorDict.Add("ProteinElement", (selector: "//div[contains(@class, 'protein')]//input", type: SelectorType.XPath));

			// Reference web elements
			selectorDict.Add("FarmElement", (selector: ".input-group__dropdown.farmId > .dropdown.dropdown__container", type: SelectorType.CSS));

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements
		//get the input path as set by the selector library
		private IWebElement FarmElement => FindElementExt("FarmElement");

		//Attribute web Elements
		private IWebElement VolumeElement => FindElementExt("VolumeElement");
		private IWebElement TemperatureElement => FindElementExt("TemperatureElement");
		private IWebElement MilkFatElement => FindElementExt("MilkFatElement");
		private IWebElement ProteinElement => FindElementExt("ProteinElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"Time" => TimeHeaderTitle,
				"Volume" => VolumeHeaderTitle,
				"Temperature" => TemperatureHeaderTitle,
				"Milk Fat" => MilkFatHeaderTitle,
				"Protein" => ProteinHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "Time":
					return TimeElement.DateTimePickerElement;
				case "Volume":
					return VolumeElement;
				case "Temperature":
					return TemperatureElement;
				case "Milk Fat":
					return MilkFatElement;
				case "Protein":
					return ProteinElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "Time":
					SetTime(Convert.ToDateTime(value));
					break;
				case "Volume":
					int? volume = null;
					if (int.TryParse(value, out var intVolume))
					{
						volume = intVolume;
					}
					SetVolume(volume);
					break;
				case "Temperature":
					SetTemperature(Convert.ToDouble(value));
					break;
				case "Milk Fat":
					SetMilkFat(Convert.ToDouble(value));
					break;
				case "Protein":
					SetProtein(Convert.ToDouble(value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"Time" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.time > div > p"),
				"Volume" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.volume > div > p"),
				"Temperature" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.temperature > div > p"),
				"Milk Fat" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.milkFat > div > p"),
				"Protein" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.protein > div > p"),
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
			SetTime(_milkTestEntity.Time);
			SetVolume(_milkTestEntity.Volume);
			SetTemperature(_milkTestEntity.Temperature);
			SetMilkFat(_milkTestEntity.MilkFat);
			SetProtein(_milkTestEntity.Protein);

			SetFarmId(_milkTestEntity.FarmId?.ToString());
			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				case "farm":
					return new List<Guid>() {GetFarmId()};
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations
		private void SetFarmId(string id)
		{
			if (id == "") { return; }
			WaitUtils.elementState(_driverWait, FarmIdInputElementBy, ElementState.VISIBLE);
			var farmIdInputElement = _driver.FindElementExt(FarmIdInputElementBy);

			if (id != null)
			{
				farmIdInputElement.SendKeys(id);
				WaitForDropdownOptions();
				WaitUtils.elementState(_driverWait, By.XPath($"//*/div[@role='option']/span[text()='{id}']"), ElementState.EXISTS);
				farmIdInputElement.SendKeys(Keys.Return);
			}
		}

		// get associations
		private Guid GetFarmId()
		{
			WaitUtils.elementState(_driverWait, FarmIdElementBy, ElementState.VISIBLE);
			var farmIdElement = _driver.FindElementExt(FarmIdElementBy);
			return new Guid(farmIdElement.GetAttribute("data-id"));
		}

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetTime (DateTime? value)
		{
			if (value is DateTime datetimeValue)
			{
				TimeElement.SetDate(datetimeValue);
			}
		}

		private DateTime? GetTime =>
			Convert.ToDateTime(TimeElement.DateTimePickerElement.Text);
		private void SetVolume (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "volume", intValue.ToString(), _isFastText);
			}
		}

		private int? GetVolume =>
			int.Parse(VolumeElement.Text);

		private void SetTemperature (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "temperature", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetTemperature =>
			Convert.ToDouble(TemperatureElement.Text);
		private void SetMilkFat (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "milkFat", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetMilkFat =>
			Convert.ToDouble(MilkFatElement.Text);
		private void SetProtein (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "protein", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetProtein =>
			Convert.ToDouble(ProteinElement.Text);

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}