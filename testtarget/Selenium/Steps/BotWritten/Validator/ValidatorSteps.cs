

using System;
using APITests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Xunit;

namespace SeleniumTests.Steps.BotWritten.Validator
{
	[Binding]
	public sealed class ValidatorSteps  : BaseStepDefinition
	{
		private readonly ContextConfiguration _contextConfiguration;
		private string _entityName;
		private GenericEntityEditPage _createPage;

		public ValidatorSteps(ContextConfiguration contextConfiguration) : base(contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		[StepDefinition("I navigate to the (.*) create page")]
		public void WhenINavigateToTheCreatePage(string entityName)
		{
			_entityName = entityName;
			var page = new GenericEntityPage(entityName, _contextConfiguration);
			page.Navigate();
			_createPage = page.ClickCreateButton();
		}

		[Then(@"I verify the (.*) Validator for the (.*) attribute for (.*) validation")]
		public void IVerifyAWarningForLiveValidators(ValidatorType validator, string attribute, ValidatorActionType validatorType)
		{
			var factory = new EntityFactory(_entityName);
			var entity = factory.Construct();
			var invalidAttribute = entity.GetInvalidAttribute(attribute, validator.ToString().ToLower().Capitalize());
			var createPageDetailsSection = EntityDetailUtils.GetEntityDetailsSection(_entityName, _contextConfiguration);
			createPageDetailsSection.SetInputElement(attribute, invalidAttribute);

			switch (validatorType)
			{
				case ValidatorActionType.ON_SUBMIT:
					_createPage.SubmitButton.Click();
					break;
				case ValidatorActionType.LIVE:
					createPageDetailsSection.GetInputElement(attribute).SendKeys(Keys.Tab);
					break;
			}

			var errors = createPageDetailsSection.GetErrorMessagesForAttribute(attribute);

			switch (validator)
			{
				case ValidatorType.LENGTH:
					//(int min, int max) attributeLengthMinMax = entity.GetLengthValidatorMinMax(attribute);
					//Assert.Contains($"The length of this field is not less than {attributeLengthMinMax.max}. Actual Length: {attributeLengthMinMax.max + 1}", errors);
					(_, int max) = entity.GetLengthValidatorMinMax(attribute);
					Assert.Contains($"The length of this field is not less than {max}. Actual Length: {max + 1}", errors);
					break;
				case ValidatorType.EMAIL:
					Assert.Contains($"The value is not a valid email", errors);
					break;
				case ValidatorType.NUMERIC:
					Assert.Contains("You can only use numbers in this field", errors);
					break;
				case ValidatorType.REQUIRED:
					Assert.Contains("This field is required", errors);
					break;
				default:
					throw new Exception($"{validator} is not a valid validator");
			}
		}

		[StepArgumentTransformation]
		public static ValidatorType TransformStringToValidatorTypeEnum(string validatorType)
		{
			return (validatorType.ToLower()) switch
			{
				"length" => ValidatorType.LENGTH,
				"email" => ValidatorType.EMAIL,
				"numeric" => ValidatorType.NUMERIC,
				"required" => ValidatorType.REQUIRED,
				_ => throw new Exception($"{validatorType} enum is not handled"),
			};
		}

		[StepArgumentTransformation]
		public static ValidatorActionType TransformStringToValidatorActionTypeEnum(string validatorActionType)
		{
			switch (validatorActionType.ToLower())
			{
				case "on-submit":
					return ValidatorActionType.ON_SUBMIT;
				case "live":
					return ValidatorActionType.LIVE;
				default:
					throw new Exception($"{validatorActionType} enum is not handled");
			}
		}
	}
}