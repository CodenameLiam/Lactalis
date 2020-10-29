import { Symbols } from "Symbols";
import { initValidators, IModelAttributeValidationError, ErrorType } from "../Util";
import { Model } from "Models/Model";
import { getFetchAllQuery, getModelName } from "Util/EntityUtils";
import { store } from "Models/Store";
import { modelName as modelNameSymbol } from "Symbols";
import { lowerCaseFirst } from "Util/StringUtils";

export default function validate() {
	return (target: object, key: string) => {
		initValidators(target, key);
		target[Symbols.validatorMap][key].push("Unique");
		target[Symbols.validator].push(
			(model: Model): Promise<IModelAttributeValidationError | null> =>
				new Promise((resolve) => {
					if (model[key] === null || model[key] === undefined) {
						resolve(null);
						return;
					}
					const modelName = lowerCaseFirst(target.constructor[modelNameSymbol]);
					const query = getFetchAllQuery(target.constructor as { new (): Model });
					const variables = { args: [{ path: key, comparison: "equal", value: model[key] }] };
					store.apolloClient
						.query({ query: query, fetchPolicy: "network-only", variables: variables })
						.then(
							({ data }) => {
								if (
									!!data[`${modelName}s`] &&
									!!data[`${modelName}s`][0] &&
									!!data[`${modelName}s`][0]["id"] &&
									model["id"] !== data[`${modelName}s`][0]["id"]
								) {
									const errorMessage = `This value entered has already been used`;
									resolve({
										errorType: ErrorType.EXISTS,
										errorMessage,
										attributeName: key,
										target: model,
									});
								}
								resolve(null);
							},
							() => {
								// if error happens to asynchronous validation, just consider this as valid for now
								resolve(null);
							}
						);
				})
		);
	};
}
