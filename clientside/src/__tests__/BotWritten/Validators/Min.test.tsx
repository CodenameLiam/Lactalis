import { Model } from "Models/Model";
import * as Validators from "Validators";

const MinimumValidatorTheoryData = [
	[12, true],
	[11, false],
	[0, false],
	[-12, false],
	[12.1, true],
	[13, true],
	[500, true],
];

const MinTestNumber = 12;

class TestModel extends Model {
	@Validators.Min(MinTestNumber)
	testNumber: number;
}

describe("Minimum Validators", () => {
	test.each(MinimumValidatorTheoryData)(
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
							`The value is ${inputNumber} which is less than the required amount of ${MinTestNumber}`,
						]);
					}
				});
			}
		}
	);
});
