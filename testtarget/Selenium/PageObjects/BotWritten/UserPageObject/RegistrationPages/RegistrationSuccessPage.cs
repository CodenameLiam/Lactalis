

using OpenQA.Selenium;
using SeleniumTests.Setup;

namespace SeleniumTests.PageObjects.BotWritten.UserPageObjects
{
	public class RegistrationSuccessPage : BasePage
	{
		public IWebElement successHeader => driver.FindElement(By.CssSelector("h2"));
		public IWebElement registrationEmail => driver.FindElement(By.CssSelector("span.bold"));
		public RegistrationSuccessPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
		}
	}
}