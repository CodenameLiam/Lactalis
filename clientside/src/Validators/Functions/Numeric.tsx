import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Numeric");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined || !isNaN(model[key])) {
						resolve(null);
						return;
					}
					const errorMessage = `This field must be a number`;
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
