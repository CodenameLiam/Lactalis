

using System;
using OpenQA.Selenium;

namespace SeleniumTests.Utils
{
	/// <summary>
	/// <para>Utility class for testing the React Date-Time components.</para>
	/// <para>
	/// Provides useful, reusable methods for interacting with the DatePicker, DateRangePicker, DateTimePicker,
	/// DateTimeRangePicker and TimePicker components.
	/// </para>
	/// <para>Currently private methods may be made public if appropriate.</para>
	/// </summary>
	internal static class DateTimePickerUtils
	{
		/// <summary>
		/// Inputs the given DateTime into the standard date picker opened by clicking on the HTML element with the
		/// given className.
		/// </summary>
		/// <param name="webDriver">
		/// 	currently active Selenium web driver
		/// </param>
		/// <param name="className">
		/// 	.css class name of an element which triggers a Flatpickr - expected to be unique within the DOM
		/// </param>
		/// <param name="date">
		///		DateTime to input
		/// </param>
		public static void EnterDateByClassName(IWebDriver webDriver, string className, DateTime date)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
			var script = $@"var input = document.getElementsByClassName('{className}')[0].lastElementChild;
				var setValue = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
				 setValue.call(input, '{date.ToString("yyyy'-'MM'-'dd")}');
				var e = new Event('input',  {{bubbles: true}} );
				input.dispatchEvent(e);";
			js.ExecuteScript(script);
		}
	}
}
