import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Email");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else {
						resolve(
							isEmail(model[key])
								? null
								: {
										errorType: ErrorType.INVALID,
										errorMessage: `The value is not a valid email`,
										attributeName: key,
										target: model,
								  }
						);
					}
				})
		);
	};
}

export function isEmail(email: string): boolean {
	return /[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/.test(
		email
	);
}
