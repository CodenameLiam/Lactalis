

using System;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using System.Linq;

namespace SeleniumTests.Steps.BotWritten.Utility
{
	[Binding]
	public sealed class WaitForSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public WaitForSteps (ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		//Wait For elements

		[ObsoleteAttribute]
		[StepDefinition("I wait for the element with (.*) of (.*) to be present")]
		public void WaitForAnElementToBePresentBy (SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.EXISTS);
		}

		[ObsoleteAttribute]
		[StepDefinition("I wait for the element with (.*) of (.*) to not be present")]
		public void WaitForAnElementToNotBePresentBy(SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.NOT_EXIST);
		}

		[ObsoleteAttribute]
		[StepDefinition("I wait for the element with (.*) of (.*) to be visible")]
		public void WaitForAnElementToBeVisibleBy (SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
		}

		[ObsoleteAttribute]
		[StepDefinition("I wait for the element with (.*) of (.*) to not be visible")]
		public void WaitForAnElementToNotBeVisibleBy(SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.NOT_VISIBLE);
		}

		// page wait
		[ObsoleteAttribute]
		[StepDefinition ("I wait for the url to be (.*)")]
		public void WaitForUrl(string url)
		{
			_driverWait.Until(_ => _driver.Url == url);
		}

		[ObsoleteAttribute]
		[StepDefinition("I wait for (.*) seconds")]
		public static void WaitForSeconds(decimal seconds)
		{
			System.Threading.Thread.Sleep(decimal.ToInt32(Math.Floor(seconds * 1000)));
		}

		[StepArgumentTransformation]
		public static ElementState TransformStringToElementStateEnum(string elementState)
		{
			// case insensitive
			return (elementState.ToLower()) switch
			{
				"exists" => ElementState.EXISTS,
				"not exist" => ElementState.NOT_EXIST,
				"visible" => ElementState.VISIBLE,
				"not visible" => ElementState.NOT_VISIBLE,
				_ => throw new Exception("enum is not handled"),
			};
		}

		[ObsoleteAttribute("This transformation is only needed if absolute methods above are")]
		[StepArgumentTransformation]
		public static SelectorPathType TransformStringToSelectorTypeEnum(string selectorType)
		{
			// case insensitive
			switch (selectorType.ToLower())
			{
				case "id":
					return SelectorPathType.ID;
				case "css":
					return SelectorPathType.CSS;
				case "xpath":
					return SelectorPathType.XPATH;
				case "classname":
					return SelectorPathType.ClASSNAME;
				case "cssselector":
					return SelectorPathType.CSS_SELECTOR;
				default:
					throw new Exception("enum is not handled");
			}
		}
	}
}