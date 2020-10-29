

using System;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class TypingSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public TypingSteps (ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		//Type to elements
		[ObsoleteAttribute]
		[StepDefinition("I clear and type (.*) into the element with (.*) of (.*)")]
		public void ClearAndTypeToElementBy(string text, SelectorPathType selector, string path)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			TypingUtils.ClearAndTypeElement(_driver, elementBy, text);
		}

		[ObsoleteAttribute]
		[StepDefinition("I type (.*) into the element with (.*) of (.*)")]
		public void TypeElementBy(string text, SelectorPathType selector, string path)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			TypingUtils.TypeElement(_driver, elementBy, text);
		}

		[ObsoleteAttribute]
		[StepDefinition("I type the date (.*) into the element with class of (.*)")]
		public void TypeDateWithClass(string date, string className)
		{
			DateTimePickerUtils.EnterDateByClassName(_driver, className, DateTime.Parse(date));
		}
	}
}