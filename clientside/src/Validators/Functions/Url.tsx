import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Url");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else {
						resolve(
							isUrl(model[key])
								? null
								: {
										errorType: ErrorType.INVALID,
										errorMessage: `The value is not a valid url`,
										attributeName: key,
										target: model,
								  }
						);
					}
				})
		);
	};
}

export function isUrl(url: string): boolean {
	return /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/.test(url);
}
