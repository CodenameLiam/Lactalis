export function lowerCaseFirst(str: string) {
	return str[0].toLowerCase() + str.slice(1);
}

export function upperCaseFirst(str: string) {
	return str[0].toUpperCase() + str.slice(1);
}

export function camelCaseIntoWords(name: string) {
	return name.replace(/([A-Z])/g, " $1").replace(/^./, (str) => str.toUpperCase());
}

export function camelCase(str: string) {
	const words = str.split(" ");
	const allLowerCase = words.map((w) => w.toLowerCase());
	return allLowerCase[0] + allLowerCase.slice(1).map(upperCaseFirst).join("");
}

export function lowerCaseNoSpaces(str: string) {
	return str.toLowerCase().replace(/ /g, "");
}

export function noSpaces(str: string) {
	return str.replace(/ /g, "");
}
