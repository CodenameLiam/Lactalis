import { Model } from "Models/Model";
import { Symbols } from "Symbols";
import { ErrorType, IModelAttributeValidationError, initValidators } from "../Util";

export default function validate(min?: number, max?: number) {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Length");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined || (!min && !max)) {
						resolve(null);
						return;
					}

					let isValid = true;
					if (min && model[key].length < min) {
						isValid = false;
					}
					if (max && model[key].length > max) {
						isValid = false;
					}
					if (isValid) {
						resolve(null);
						return;
					}

					let errorMessage;
					if (min && max) {
						errorMessage = `The length of this field is not ${min} and ${max}. Actual Length: ${model[key].length}`;
					} else if (min) {
						errorMessage = `The length of this field is not greater than ${min}. Actual Length: ${model[key].length}`;
					} else {
						errorMessage = `The length of this field is not less than ${max}. Actual Length: ${model[key].length}`;
					}

					resolve({
						errorType: ErrorType.LENGTH,
						errorMessage,
						attributeName: key,
						target: model,
					});
					return;
				})
		);
	};
}
