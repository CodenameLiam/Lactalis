import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate(
	validatorName: string,
	validatorFunction: (property: any, target: any) => Promise<string | null>
) {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push(validatorName);
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise(async (resolve) => {
					const error = await validatorFunction(model[key], model);
					if (error === null) {
						resolve(null);
						return;
					}
					resolve({
						errorType: ErrorType.INVALID,
						errorMessage: error,
						attributeName: key,
						target: model,
					});
				})
		);
	};
}
