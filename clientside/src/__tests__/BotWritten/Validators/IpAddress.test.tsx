import { isIP } from "Validators/Functions/IpAddress";

const IPAddressValidatorTheoryData = [
	["127.0.0.1", true],
	["192.168.1.1", true],
	["192.168.1.255", true],
	["255.255.255.255", true],
	["0.0.0.0", true],
	["1.1.1.01", true],
	["30.168.1.255.1", false],
	["127.1", false],
	["192.168.1.256", false],
	["-1.2.3.4", false],
	["1.1.1.1.", false],
	["3...3", false],
	["", false],
	["sdfsdfsdfsdf", false],
	["-brr", false],
];

describe("IPAddress Validators", () => {
	test.each(IPAddressValidatorTheoryData)(
		"we expect %p, ip validation to be %p",
		(inputString, expectedValidation) => {
			if (typeof inputString === "string") {
				expect(isIP(inputString)).toEqual(expectedValidation);
			} else {
				expect(false);
			}
		}
	);
});
