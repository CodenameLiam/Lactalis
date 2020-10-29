import {
	camelCase,
	camelCaseIntoWords,
	lowerCaseFirst,
	lowerCaseNoSpaces,
	noSpaces,
} from "../../../Util/StringUtils";

const camelCasesTheoryData = [
	["star wars", "starWars"],
	["star Wars", "starWars"],
	["starWars", "starwars"],
	["StarWars", "starwars"],
	["STARWARS", "starwars"],
	["star", "star"],
	["star wars a new hope", "starWarsANewHope"],
];

describe("Test camel casing string function with different inputs", () => {
	test.each(camelCasesTheoryData)(
		"given %p input we expect to return %p",
		(inputString, expectedOutput) => {
			expect(camelCase(inputString)).toEqual(expectedOutput);
		}
	);
});
