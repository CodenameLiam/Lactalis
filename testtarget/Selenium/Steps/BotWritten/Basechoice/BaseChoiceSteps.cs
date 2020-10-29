

using System;
using OpenQA.Selenium;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using TestDataLib;

namespace SeleniumTests.Steps.BotWritten.Basechoice
{
	[Binding]
	public sealed class BaseChoiceSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;

		public BaseChoiceSteps (ContextConfiguration contextConfiguration)  : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[ObsoleteAttribute]
		[StepDefinition("I insert valid basechoice of type (.*) with length (.*) to (.*) into element with (.*) of (.*)")]
		public void IInsertValidStringBaseChoiceIntoElement(string baseChoiceType, int min, int max, SelectorPathType selector, string path)
		{
			var elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.EXISTS);

			switch (baseChoiceType)
			{
				case "wordystring":
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandString(min, max));
					break;
				case "string":
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandString(min, max));
					break;
			}
		}

		[ObsoleteAttribute]
		[StepDefinition("I insert valid basechoice of type (.*) into element with (.*) of (.*)")]
		public void IInsertValidBaseChoiceIntoElement(BaseChoiceType baseChoiceType, SelectorPathType selector, string path)
		{
			By elementBy = WebElementUtils.GetElementAsBy(selector, path);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.EXISTS);

			switch (baseChoiceType)
			{
				case BaseChoiceType.INT:
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandInt().ToString());
					break;
				case BaseChoiceType.DOUBLE:
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandDouble().ToString());
					break;
				case BaseChoiceType.BOOL:
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandBool().ToString());
					break;
				case BaseChoiceType.EMAIL:
					TypingUtils.TypeElement(_driver, elementBy, DataUtils.RandEmail());
					break;
			}
		}

		[ObsoleteAttribute("This transformation is only needed if absolute methods above are")]
		[StepArgumentTransformation]
		public static BaseChoiceType TransformStringToBaseChoiceTypeEnum (string baseChoiceType)
		{
			return (baseChoiceType.ToLower()) switch
			{
				"int" => BaseChoiceType.INT,
				"double" => BaseChoiceType.DOUBLE,
				"bool" => BaseChoiceType.BOOL,
				"email" => BaseChoiceType.EMAIL,
				_ => throw new Exception($"{baseChoiceType} enum is not handled"),
			};
		}
	}
}