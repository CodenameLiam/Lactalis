import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import moment from "moment";
import { Model } from "Models/Model";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Date");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else {
						resolve(
							moment(model[key], "YYYY-MM-DD").isValid()
								? null
								: {
										errorType: ErrorType.INVALID,
										errorMessage: `The value ${model[key]} is not in the format YYYY-MM-DD`,
										attributeName: key,
										target: model,
								  }
						);
					}
				})
		);
	};
}
