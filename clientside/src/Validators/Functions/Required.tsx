import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Required");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (
						(model[key] || model[key] === 0) &&
						!(typeof model[key] === "string" && model[key].trim() === "")
					) {
						resolve(null);
						return;
					}
					const errorMessage = "This field is required";
					resolve({
						errorType: ErrorType.REQUIRED,
						errorMessage,
						attributeName: key,
						target: model,
					});
				})
		);
	};
}
