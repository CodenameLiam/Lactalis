

using System;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class KeyboardSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public KeyboardSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[ObsoleteAttribute]
		[StepDefinition("I press the (.*) key on element with (.*) of (.*)")]
		public void PresssKeyOnElementWith(KeyboardActionType keyName, SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var element = _driver.FindElement(elementBy);
			KeyboardUtils.EnterKeyToWebElement(element, keyName);
		}

		[ObsoleteAttribute]
		[StepDefinition("I copy from element with (.*) of (.*)")]
		public void CopyFromElement(SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var element = _driver.FindElement(elementBy);
			KeyboardUtils.CopyFromWebElement(element);
		}

		[ObsoleteAttribute]
		[StepDefinition("I paste to element with (.*) of (.*)")]
		public void PasteToElement(SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var element = _driver.FindElement(elementBy);
			KeyboardUtils.PasteToWebElement(element);
		}

		[ObsoleteAttribute]
		[StepDefinition("I select all in element with (.*) of (.*)")]
		public void SelectAllInElement(SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			var element = _driver.FindElement(elementBy);
			KeyboardUtils.SelectAllFromWebElement(element);
		}

		[ObsoleteAttribute("Will be used when the above step defs are no longer absolute")]
		[StepArgumentTransformation]
		public static KeyboardActionType TransformStringToKeyboardActionTypeEnum(string keyboardAction)
		{
			return (keyboardAction.ToLower()) switch
			{
				"enter" => KeyboardActionType.ENTER,
				"escape" => KeyboardActionType.ESCAPE,
				"tab" => KeyboardActionType.TAB,
				_ => throw new Exception($"{keyboardAction} enum is not handled"),
			};
		}
	}
}