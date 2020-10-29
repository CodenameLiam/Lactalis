import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate(pattern: RegExp, message?: string) {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Regex");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined || pattern.test(model[key])) {
						resolve(null);
						return;
					}
					const errorMessage = message
						? message
						: `This fields requires value to match the pattern ${pattern}`;
					resolve({
						errorType: ErrorType.INVALID,
						errorMessage,
						attributeName: key,
						target: model,
					});
				})
		);
	};
}
