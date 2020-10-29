import { standardiseInteger, standardiseBoolean } from "../../../Util/AttributeUtils";

const standardIntTheoryData = [
	["52", { query: "52" }],
	["52.5", null],
	["NotANumber", null],
	[undefined, null],
	["NaN", null],
];

const standardBoolTheoryData = [
	["52", null],
	["52.5", null],
	["NotANumber", null],
	[undefined, null],
	["false", { query: "false" }],
	["NaN", null],
];

describe("Test standardise integer function with different inputs", () => {
	test.each(standardIntTheoryData)(
		"given %p input we expect to return %p",
		(inputString, expectedOutput) => {
			if (typeof inputString === "string") {
				expect(standardiseInteger(inputString)).toEqual(expectedOutput);
			} else {
				expect(standardiseInteger(inputString?.query)).toEqual(expectedOutput);
			}
		}
	);
});

describe("Test standardise bool function with different inputs", () => {
	test.each(standardBoolTheoryData)(
		"given %p input we expect to return %p",
		(inputString, expectedOutput) => {
			if (typeof inputString === "string") {
				expect(standardiseBoolean(inputString)).toEqual(expectedOutput);
			} else {
				expect(standardiseInteger(inputString?.query)).toEqual(expectedOutput);
			}
		}
	);
});
