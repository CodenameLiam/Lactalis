import { Symbols } from "Symbols";
import { Model } from "Models/Model";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";

export function isIP(url: string): boolean {
	return /^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(
		url
	);
}

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
							isIP(model[key])
								? null
								: {
										errorType: ErrorType.INVALID,
										errorMessage: "The value is not a valid IP address",
										attributeName: key,
										target: model,
								  }
						);
					}
				})
		);
	};
}
