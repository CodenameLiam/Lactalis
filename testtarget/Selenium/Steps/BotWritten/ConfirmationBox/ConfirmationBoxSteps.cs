

using System;
using SeleniumTests.Enums;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.AlertBox
{
	[Binding]
	public sealed class ConfirmationBoxSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public ConfirmationBoxSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[Then("I (.*) the confirmation box")]
		public void IPerformActionOnConfirmationBox(UserActionType userAction)
		{
			switch (userAction) {
				case UserActionType.ACCEPT:
					AlertBoxUtils.AlertBoxHandler(_driver, true);
					break;
				case UserActionType.DISMISS:
				case UserActionType.CLOSE:
						AlertBoxUtils.AlertBoxHandler(_driver, false);
					break;
				default:
					throw new Exception("Unable to determine required action on Alert box");
			}
		}
	}
}