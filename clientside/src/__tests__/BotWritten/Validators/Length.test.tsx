import { Model } from "Models/Model";
import * as Validators from "Validators";

// TODO: add a rand string library to replace these hard coded strings below.
const MinimumLengthValidatorTheoryData = [
	["Star Wars", true],
	["Star Wars The Empire Strikes Back", true],
	["Star W", true],
	["Star", false],
	["", false],
];

const MaximumLengthValidatorTheoryData = [
	["Star Wars", true],
	["Star Wars Th", true],
	["Star W", true],
	["Star", true],
	["", true],
	["Star Wars The", false],
	["Star Wars The Empire Strikes Back", false],
];

const MinimumMaximumLengthValidatorTheoryData = [
	["Star Wars", true],
	["Star Wars Th", true],
	["Star W", true],
	["Star", false],
	["", false],
	["Star Wars The", false],
	["Star Wars The Empire Strikes Back", false],
];

const MinTestNumber = 6;
const MaxTestNumber = 12;

class TestModel extends Model {
	@Validators.Length(MinTestNumber, MaxTestNumber)
	testString: string;

	@Validators.Length(MinTestNumber)
	minimumString: string;

	@Validators.Length(undefined, MaxTestNumber)
	maximumString: string;
}

describe("Length Validators", () => {
	test.each(MinimumMaximumLengthValidatorTheoryData)(
		"we expect %p, min and max validation to be %p",
		async (inputString, isValid) => {
			expect.assertions(1);
			if (typeof inputString !== "string" || typeof isValid !== "boolean") {
				throw "Invalid test arguments";
			}

			let testModel = new TestModel();
			testModel.testString = inputString;

			await testModel.validate().then((x) => {
				const errors = testModel.getErrorsForAttribute("testString");
				if (isValid) {
					expect(errors).toEqual([]);
				} else {
					expect(errors).toEqual([
						`The length of this field is not ${MinTestNumber} and ${MaxTestNumber}. Actual Length: ${inputString.length}`,
					]);
				}
			});
		}
	);

	test.each(MinimumLengthValidatorTheoryData)(
		"we expect %p, min validation to be %p",
		async (inputString, isValid) => {
			expect.assertions(1);
			if (typeof inputString !== "string" || typeof isValid !== "boolean") {
				throw "Invalid test arguments";
			}

			let testModel = new TestModel();
			testModel.minimumString = inputString;

			await testModel.validate().then((x) => {
				const errors = testModel.getErrorsForAttribute("minimumString");
				if (isValid) {
					expect(errors).toEqual([]);
				} else {
					expect(errors).toEqual([
						`The length of this field is not greater than ${MinTestNumber}. Actual Length: ${inputString.length}`,
					]);
				}
			});
		}
	);

	test.each(MaximumLengthValidatorTheoryData)(
		"we expect %p, max validation to be %p",
		async (inputString, isValid) => {
			expect.assertions(1);
			if (typeof inputString !== "string" || typeof isValid !== "boolean") {
				throw "Invalid test arguments";
			}

			let testModel = new TestModel();
			testModel.maximumString = inputString;

			await testModel.validate().then((x) => {
				const errors = testModel.getErrorsForAttribute("maximumString");
				if (isValid) {
					expect(errors).toEqual([]);
				} else {
					expect(errors).toEqual([
						`The length of this field is not less than ${MaxTestNumber}. Actual Length: ${inputString.length}`,
					]);
				}
			});
		}
	);
});
