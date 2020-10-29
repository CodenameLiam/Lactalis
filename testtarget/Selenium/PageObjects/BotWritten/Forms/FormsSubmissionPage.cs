

using OpenQA.Selenium;
using SeleniumTests.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;

namespace SeleniumTests.PageObjects.BotWritten.Forms
{
	public class FormsSubmissionPage : BasePage
	{
		public IWebElement OpenFormButton => FindElementExt("OpenFormButton");
		public IWebElement SubmitButton => FindElementExt("SubmitButton");

		public FormsSubmissionPage(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			InitializeSelectors();
		}
		private void InitializeSelectors()
		{
			selectorDict.Add("OpenFormButton", (selector: "//button[contains(text(),'Open Form')]", type: SelectorType.XPath));
			selectorDict.Add("SubmitButton", (selector: "//button[contains(text(),'Submit')]", type: SelectorType.XPath));
		}

		public bool SlideExists(string slide)
		{
			return WaitUtils.elementState(driverWait, By.XPath($"//h3[contains(text(),'{slide}')]"), ElementState.EXISTS );
		}
		
		public bool QuestionExists(string question)
		{
			return WaitUtils.elementState(driverWait, By.XPath($"//div[@data-name='{question}']"), ElementState.EXISTS );
		}

		public void AnswerTextQuestion(string question, string answer)
		{
			var input = driver.FindElement(By.XPath($"//div[@data-name='{question}']//input"));
			input.SendKeys(answer);
		}
	}
}