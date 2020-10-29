

using System;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class NavigationSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public NavigationSteps (ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[ObsoleteAttribute]
		[StepDefinition("I navigate to url (.*)")]
		public void NavigateToUrl(string url)
		{
			NavigationUtils.GoToUrl(_driver, _driverWait, url);
		}

		[StepDefinition("I refresh the page")]
		public void RefreshThePage()
		{
			NavigationUtils.RefreshPage(_driver, _driverWait);
		}
	}
}