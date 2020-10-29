import { Model } from "Models/Model";
import * as Validators from "Validators";

const MaximumValidatorTheoryData = [
	[12, true],
	[11, true],
	[0, true],
	[-12, true],
	[12.1, false],
	[13, false],
	[500, false],
];

const MaxTestNumber = 12;

class TestModel extends Model {
	@Validators.Max(MaxTestNumber)
	testNumber: number;
}

describe("Max Validators", () => {
	test.each(MaximumValidatorTheoryData)(
		"we expect %p, validation to be %p",
		async (inputNumber, isValid) => {
			expect.assertions(1);
			if (typeof inputNumber === "number") {
				let testModel = new TestModel();
				testModel.testNumber = inputNumber;

				await testModel.validate().then((x) => {
					const errors = testModel.getErrorsForAttribute("testNumber");
					if (isValid) {
						expect(errors).toEqual([]);
					} else {
						expect(errors).toEqual([
							`The value is ${inputNumber} which is greater than the required amount of ${MaxTestNumber}`,
						]);
					}
				});
			}
		}
	);
});
