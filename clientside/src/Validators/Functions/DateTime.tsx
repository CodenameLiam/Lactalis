import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import moment from "moment";
import { Model } from "Models/Model";

export default function validate(seconds: boolean = true) {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("DateTime");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> => {
				const format = `YYYY-MM-DD HH:mm${seconds ? ":ss" : ""}`;
				return new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
					} else {
						resolve(
							moment(model[key], format).isValid()
								? null
								: {
										errorType: ErrorType.INVALID,
										errorMessage: `The value ${model[key]} is not in the format ${format}`,
										attributeName: key,
										target: model,
								  }
						);
					}
				});
			}
		);
	};
}
