import { Model } from "Models/Model";
import * as Validators from "Validators";

const RequiredValidatorTheoryData = [
	["", false],
	[undefined, false],
	[null, false],
	["Hello Wordl!!", true],
];

class TestModel extends Model {
	@Validators.Required()
	testName: string | null | undefined | boolean;
}

describe("Required Validators", () => {
	//@ts-ignore
	test.each(RequiredValidatorTheoryData)(
		"we expect %p, validation to be %p",
		async (inputString, isValid) => {
			expect.assertions(1);

			let testModel = new TestModel();
			testModel.testName = inputString;

			await testModel.validate().then((x) => {
				const errors = testModel.getErrorsForAttribute("testName");

				if (isValid) {
					expect(errors).toEqual([]);
				} else {
					expect(errors).toEqual(["This field is required"]);
				}
			});
		}
	);
});
