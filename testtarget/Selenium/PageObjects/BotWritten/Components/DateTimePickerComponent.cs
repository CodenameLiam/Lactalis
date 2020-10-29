

using System;
using OpenQA.Selenium;
using SeleniumTests.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;



namespace SeleniumTests.PageObjects.Components
{
	public class DateTimePickerComponent : IDatePickerComponent, ITimePickerComponent
	{
		public DatePickerComponent datePickerComponent;
		public TimePickerComponent timePickerComponent;

		public IWebElement DateTimePickerElement { get; set; }
		internal IWebElement TriggeredCalendar => datePickerComponent.TriggeredCalendar;
		internal IWebElement DatePickerYearElement => datePickerComponent.DatePickerYearElement;
		internal IWebElement DatePickerMonthElement => datePickerComponent.DatePickerMonthElement;

		public DateTimePickerComponent(ContextConfiguration contextConfiguration, IWebElement dateTimePickerElement)
		{
			DateTimePickerElement = dateTimePickerElement;
			datePickerComponent = new DatePickerComponent(contextConfiguration, dateTimePickerElement);
			timePickerComponent = new TimePickerComponent(contextConfiguration, dateTimePickerElement);
		}

		public DateTimePickerComponent(ContextConfiguration contextConfiguration, string className)
		{
			WaitUtils.elementState(contextConfiguration.WebDriverWait, By.CssSelector($".{className}"), ElementState.EXISTS);
			DateTimePickerElement = contextConfiguration.WebDriver.FindElement(By.CssSelector($".{className}"));
			datePickerComponent = new DatePickerComponent(contextConfiguration, className);
			timePickerComponent = new TimePickerComponent(contextConfiguration, className);
		}

		public void SetDate(DateTime dateTime) => datePickerComponent.SetDate(dateTime);

		public void SetDateRange(DateTime startDate, DateTime endDate)
		{
			datePickerComponent.SetDateRange(startDate, endDate);
		}

		/// <summary>
		/// Sets a date and a time
		/// </summary>
		/// <param name="dateTime">The datetime that should be set</param>
		public void SetDateTime(DateTime dateTime)
		{
			SetDate(dateTime);
			SetTime(dateTime);
		}

		/// <summary>
		/// Sets a date time range
		/// </summary>
		/// <param name="startDateTime">The starting date of the range</param>
		/// <param name="endDateTime">The end date of the range</param>
		public void SetDateTimeRange(DateTime startDateTime, DateTime endDateTime)
		{
			SetDate(startDateTime);
			SetTime(startDateTime);
			SetDate(endDateTime);
			SetTime(endDateTime);
		}

		/// <summary>
		/// Inputs the given time as hour, minute and AM/PM into the time picker
		/// by clicking on the web element with the given className.
		/// </summary>
		/// <param name="time">Time to input</param>
		public void SetTime(DateTime time) => timePickerComponent.SetTime(time);

		public void Close() => timePickerComponent.Close();
	}
}