

using System;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class ClickingSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public ClickingSteps (ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[ObsoleteAttribute]
		[StepDefinition("I click on the element with (.*) of (.*)")]
		public void ClickOnElementBy (SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			MouseClickUtils.ClickOnElement(_driver, _driverWait, elementBy);
		}
	}
}