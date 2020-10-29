import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";

export default function validate(value: number) {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Max");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else {
						resolve(
							model[key] <= value
								? null
								: {
										errorType: ErrorType.RANGE,
										errorMessage: `The value is ${model[key]} which is greater than the required amount of ${value}`,
										attributeName: key,
										target: model,
								  }
						);
					}
				})
		);
	};
}
