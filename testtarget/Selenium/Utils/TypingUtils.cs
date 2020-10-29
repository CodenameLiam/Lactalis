

using OpenQA.Selenium;

namespace SeleniumTests.Utils
{
	internal static class TypingUtils
	{
		public static void InputEntityAttributeByClass(IWebDriver driver, string className, string inputString, bool isFastText)
		{
			var Element = driver.FindElementExt(By.XPath($"//div[contains(@class, '{className}')]//input"));
			KeyboardUtils.DeleteAllFromWebElement(Element);

			if (isFastText)
			{
				var js = (IJavaScriptExecutor)driver;
				var script = $@"var input = document.getElementsByClassName('{className}')[0].getElementsByTagName('input')[0];
					var setValue = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
					setValue.call(input, '{inputString}');
					var e = new Event('input',  {{bubbles: true}} );
					input.dispatchEvent(e);";
				js.ExecuteScript(script);
			}
			else
			{
				Element.SendKeys(inputString);
			}
		}

		public static void ClearAndTypeElement(IWebElement element, string text)
		{
			KeyboardUtils.DeleteAllFromWebElement(element);
			element.SendKeys(text);
		}

		public static void ClearAndTypeElement(IWebDriver driver, By elementBy, string text)
		{
			var element = driver.FindElement(elementBy);
			ClearAndTypeElement(element, text);
		}

		public static void TypeElement(IWebElement element, string text)
		{
			element.SendKeys(text);
		}

		public static void TypeElement(IWebDriver driver, By elementBy, string text)
		{
			var element = driver.FindElement(elementBy);
			TypeElement(element, text);
		}
	}
}