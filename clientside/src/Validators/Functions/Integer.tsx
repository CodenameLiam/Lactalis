import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Integer");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else if (
						!(typeof model[key] === "string" && (model[key] as string).indexOf(".") >= 0) &&
						!isNaN(model[key])
					) {
						const number = Number(model[key]);
						if (Number.isInteger(number)) {
							resolve(null);
						}
					}

					const errorMessage = `This field must be an integer`;
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
