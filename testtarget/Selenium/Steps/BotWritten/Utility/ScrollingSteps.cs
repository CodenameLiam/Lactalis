

using System;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class ScrollingSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public ScrollingSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		// Scroll elements
		[ObsoleteAttribute]
		[StepDefinition("I scroll to the element with (.*) of (.*)")]
		public void ScrollToElementBy (SelectorPathType selector, string path)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			ScrollingUtils.scrollToElement(_driver, elementBy);
		}

		[ObsoleteAttribute]
		[StepDefinition("I scroll the page by (.*) pixels")]
		public void ScrollPageByPixels(int numPixels)
		{
			ScrollingUtils.scrollUpOrDown(_driver, numPixels);
		}
	}
}