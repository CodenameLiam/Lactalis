

using OpenQA.Selenium;

namespace SeleniumTests.Utils
{
	internal static class AlertBoxUtils
	{
		/// <summary>
		/// Check if an alert box is currently present on the page
		/// </summary>
		/// <param name="webDriver"></param>
		/// <returns></returns>
		public static bool AlertBoxIsPresent(IWebDriver webDriver)
		{
			try
			{
				var alertBox = webDriver.SwitchTo().Alert();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the message contained in the current alert box
		/// </summary>
		/// <param name="webDriver"></param>
		/// <returns></returns>
		public static string ReadAlertBoxMessage(IWebDriver webDriver)
		{
			var alertBox = webDriver.SwitchTo().Alert();
			var alertBoxMessage = alertBox.Text;
			return alertBoxMessage;
		}

		/// <summary>
		/// Writes a message to the current alert box
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="message"></param>
		public static void WriteToAlertBox(IWebDriver webDriver, string message)
		{
			var alertBox = webDriver.SwitchTo().Alert();
			alertBox.SendKeys(message);
		}

		/// <summary>
		/// Accepts of Dismissed the current alert box
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="confirmation"></param>
		public static void AlertBoxHandler(IWebDriver webDriver, bool confirmation)
		{
			var alertBox = webDriver.SwitchTo().Alert();
			if (confirmation)
			{
				alertBox.Accept();
			}
			else
			{
				alertBox.Dismiss();
			}
			webDriver.SwitchTo().DefaultContent();
		}
	}
}