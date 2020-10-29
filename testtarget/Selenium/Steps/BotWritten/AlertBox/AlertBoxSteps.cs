

using System;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using Xunit;
using SeleniumTests.PageObjects;

namespace SeleniumTests.Steps.BotWritten.AlertBox
{
	[Binding]
	public sealed class AlertBoxSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public AlertBoxSteps(ContextConfiguration contextConfiguration)  : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[StepDefinition("I assert the message box reads (.*)")]
		public void GivenINavigateToTheEntityPage(string expectedMessage)
		{
			var messageBoxContents = AlertBoxUtils.ReadAlertBoxMessage(_driver);
			Assert.Equal(messageBoxContents, expectedMessage);
		}

		[StepDefinition("I (.*) the alert")]
		public void AcceptDismissAlert(UserActionType acceptance)
		{
			switch(acceptance)
			{
				case UserActionType.ACCEPT:
					AlertBoxUtils.AlertBoxHandler(_driver, true);
					break;
				case UserActionType.DISMISS:
					AlertBoxUtils.AlertBoxHandler(_driver, false);
					break;
			}
		}

		[StepDefinition("I type (.*) and (.*) the alert")]
		public void TypeAcceptDismissAlert(string text, UserActionType acceptance)
		{
			AlertBoxUtils.WriteToAlertBox(_driver, text);
			switch (acceptance)
			{
				case UserActionType.ACCEPT:
					AlertBoxUtils.AlertBoxHandler(_driver, true);
					break;
				case UserActionType.DISMISS:
					AlertBoxUtils.AlertBoxHandler(_driver, false);
					break;
			}
		}

		[StepDefinition("I expect the alert message to be '(.*)'")]
		public void ExpectAlertMessage(string expectedMessage)
		{
			var displayedMessage = AlertBoxUtils.ReadAlertBoxMessage(_driver);
			Assert.Equal(expectedMessage, displayedMessage);
		}

		[Then(@"I assert that I can see a popup displays a message: (.*)")]
		public void ThenIAssertThatICanSeeTheToasterWithAEntityAdddedSuccessMessage( string expectedSuccessMsg)
		{
			var toaster = new ToasterAlert(_contextConfiguration);
			_driverWait.Until(_ => toaster.ToasterBody.Displayed);
			Assert.Equal(expectedSuccessMsg, toaster.ToasterBody.Text);
		}
	}
}