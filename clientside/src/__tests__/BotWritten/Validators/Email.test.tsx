import { isEmail } from "Validators/Functions/Email";

const EmailValidatorTheoryData = [
	["a", false],
	["a@examplecom", false],
	["a@.com", false],
	["a@", false],
	["", false],
	["test@@@example.com", false],
	["a@example.com", true],
	["test@example.org", true],
	["test@example.com.au", true],
	["test@example.edu.au", true],
];

describe("Email Validators", () => {
	test.each(EmailValidatorTheoryData)(
		"we expect %p, email validation to be %p",
		(inputString, expectedValidation) => {
			if (typeof inputString === "string") {
				expect(isEmail(inputString)).toEqual(expectedValidation);
			} else {
				expect(false);
			}
		}
	);
});
