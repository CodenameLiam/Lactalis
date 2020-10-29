

using System;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.Components
{
	internal interface ITimePickerComponent
	{
		void SetTime(DateTime time);
		void Close();
	}

	public class TimePickerComponent : BaseSection, ITimePickerComponent
	{
		public IWebElement TimePickerElement { get; set; }
		public IWebElement TriggeredTimeInput => FindElementExt("TriggeredTimeInput");
		public IWebElement TimePickerHourElement => FindElementExt(TriggeredTimeInput, "HourElement");
		public IWebElement TimePickerMinuteElement => FindElementExt(TriggeredTimeInput, "MinuteElement");
		public IWebElement TimePickerAmPmElement => FindElementExt(TriggeredTimeInput, "AmPmElement");

		public TimePickerComponent(ContextConfiguration contextConfiguration, IWebElement timePickerElement) : base(contextConfiguration)
		{
			TimePickerElement = timePickerElement;
			InitializeSelectors();
		}

		public TimePickerComponent(ContextConfiguration contextConfiguration, string className) : base(contextConfiguration)
		{
			TimePickerElement = driver.FindElement(By.CssSelector($".{className}"));
			InitializeSelectors();
		}

		private void InitializeSelectors()
		{
			selectorDict.Add("TriggeredTimeInput", (selector: "//div[contains(@class, 'flatpickr-calendar') and contains(@class ,'open')]", type: SelectorType.XPath));
			selectorDict.Add("HourElement", (selector: ".numInput.flatpickr-hour", type: SelectorType.CSS));
			selectorDict.Add("MinuteElement", (selector: ".numInput.flatpickr-minute", type: SelectorType.CSS));
			selectorDict.Add("AmPmElement", (selector: ".flatpickr-am-pm", type: SelectorType.CSS));
		}

		/// <summary>
		/// Inputs the given time as hour, minute and AM/PM into the time picker
		/// by clicking on the web element with the given className.
		/// </summary>
		/// <param name="time">Time to input</param>
		public void SetTime(DateTime time)
		{
			TimePickerElement.Click();
			SetHour(time.Hour);
			TimePickerMinuteElement.Click();
			SetMinute(time.Minute);
		}

		/// <summary>
		/// Closes the date time picker input
		/// </summary>
		public void Close() => TimePickerAmPmElement.SendKeys(Keys.Enter);

		public void SetHour(int hour) => KeyboardUtils.SendIntAsDigits(hour, TimePickerHourElement);

		public void SetMinute(int minute) => KeyboardUtils.SendIntAsDigits(minute, TimePickerMinuteElement);
	}
}