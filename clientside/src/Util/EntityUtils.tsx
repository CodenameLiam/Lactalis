import * as uuid from "uuid";
import _ from "lodash";
import axios from "axios";
import gql from "graphql-tag";
import { IConditionalFetchArgs, Model } from "Models/Model";
import { AttributeCRUDOptions, ICRUDOptions } from "Models/CRUDOptions";
import {
	crudOptions,
	attributes,
	modelName as modelNameSymbol,
	displayName as displayNameSymbol,
	crudId,
	fileAttributes,
} from "Symbols";
import { lowerCaseFirst } from "./StringUtils";
import { saveAs } from "file-saver";
import { SERVER_URL } from "Constants";
import { store } from "Models/Store";
import { Symbols } from "Symbols";
import {
	IOrderByCondition,
	IWhereCondition,
	IWhereConditionApi,
} from "Views/Components/ModelCollection/ModelQuery";

/**
 * Helper method to get model name based on the modelType - must be a Model or subclass
 * @param modelType The type of model
 */
export function getModelName(modelType: { new (): Model }) {
	return modelType[modelNameSymbol];
}

/**
 * Helper method to get model display name based on the modelType - must be a Model or subclass
 * @param modelType The type of model
 */
export function getModelDisplayName(modelType: { new (): Model }) {
	return modelType[displayNameSymbol];
}

/**
 * Helper method that reads the display metadata (decorators) of this type and returns it in a typed format
 */
export function getAttributeCRUDOptions(modelType: { new (): Model }): AttributeCRUDOptions[] {
	const attributeDisplay = [];
	for (const [attributeName, displayOptions] of Object.entries(modelType.prototype[crudOptions])) {
		attributeDisplay.push(new AttributeCRUDOptions(attributeName, displayOptions as ICRUDOptions));
	}

	return attributeDisplay;
}

/*
 * Gets all attributes for the model
 */
export function getAttributes(modelType: { new (): Model }) {
	return [...modelType.prototype[attributes]];
}

export function getFiles(modelType: { new (): Model }) {
	return [...modelType.prototype[fileAttributes]].map((f) => f.name);
}

export async function exportAll(
	modelType: { new (): Model },
	conditions: IWhereConditionApi<{}>[][] = []
): Promise<void> {
	const result = await axios.post(
		`${SERVER_URL}/api/entity/${getModelName(modelType)}/export`,
		conditions,
		{
			headers: {
				"Content-Type": "application/json",
			},
		}
	);
	const blob = new Blob([result.data], { type: "text/csv;charset=utf-8" });
	saveAs(blob, `export-${getModelName(modelType)}.csv`);
}

/*
 * Gets all models
 */
export function getFetchAllQuery(
	modelType: { new (): Model },
	expandString?: string,
	useListExpands?: boolean
) {
	const model = new modelType();
	const modelsName = lowerCaseFirst(model.getModelName());

	return gql`
		query ${modelsName}($args: [WhereExpressionGraph], $skip:Int, $take:Int, $orderBy: [OrderByGraph], $ids: [ID] ) {
			${modelsName}s(where: $args, skip:$skip, take:$take, orderBy: $orderBy, ids: $ids) {
				${expandString ? expandString : ""}
				${model.attributes.join("\n")}
				${model.files.map((f) => f.name).join("\n")}
				${useListExpands ? new modelType().listExpands : new modelType().defaultExpands}
			}
			count${model.getModelName()}s(where: $args) {
				number
			}
		}`;
}

/**
 * Gets all models
 */
export function getFetchSingleQuery(
	modelType: { new (): Model },
	expandString?: string,
	useListExpands?: boolean
) {
	const modelsName = lowerCaseFirst(getModelName(modelType));

	return gql`
		query ${modelsName} ($args:[WhereExpressionGraph]) {
			${modelsName} (where: $args) {
				${expandString ? expandString : ""}
				${getAttributes(modelType).join("\n")}
				${getFiles(modelType).join("\n")}
				${useListExpands ? new modelType().listExpands : new modelType().defaultExpands}
			}
		}`;
}

/**
 * Creates a conditional query to send over graphql
 * @param modelType The model type to create the query for
 * @param expandString Custom string to be inserted into the graphql query
 * @param useListExpands Should the query only use the list query expands
 */
export function getFetchAllConditional(
	modelType: { new (): Model },
	expandString?: string,
	useListExpands?: boolean
) {
	const modelName: string = modelType[modelNameSymbol];
	const lowerModelName = lowerCaseFirst(modelName);

	return gql`
		query ${lowerModelName}($args: [[WhereExpressionGraph]], $skip:Int, $take:Int, $orderBy: [OrderByGraph], $ids: [ID] ) {
			${lowerModelName}s : ${lowerModelName}sConditional(conditions: $args, skip:$skip, take:$take, orderBy: $orderBy, ids: $ids) {
				${expandString ? expandString : ""}
				${getAttributes(modelType).join("\n")}
				${getFiles(modelType).join("\n")}
				${useListExpands ? new modelType().listExpands : new modelType().defaultExpands}
			}
			count${modelName}s : count${modelName}sConditional(conditions: $args) {
				number
			}
		}`;
}

/**
 * Creates a function that checks if a field in the first argument equals the second argument
 * @param field The field to check
 * @returns A function that compares equality of the field to an option
 */
export function makeJoinEqualsFunc<T>(field: string) {
	return (modelProp: T, option: string) => modelProp[field] === option;
}

export type manyToManyOptions = {
	entityName: string;
	oppositeEntityName: string;
	relationName: string;
	relationOppositeName: string;
	entity: () => {
		new (attrs?: any): Model;
		getAttributes(): string[];
		getFiles(): { name: string; blob: string }[];
	};
	joinEntity: () => {
		new (attrs?: any): Model;
		getAttributes(): string[];
		getFiles(): { name: string; blob: string }[];
	};
	oppositeEntity: () => {
		new (attrs?: any): Model;
		getAttributes(): string[];
		getFiles(): { name: string; blob: string }[];
	};
	args?: (
		query: string,
		existingArgs: Array<Array<IWhereCondition<any>>>
	) => Array<Array<IWhereCondition<any>>>;
	orderBy?: Array<IOrderByCondition<any>>;
	take?: number;
	skip?: number;
	case?: string;
	id?: string;
	ids?: Array<String>;
};

/**
 * Makes a function that fetches the entities needed for many to many queries
 * @param options options for the fetch function
 */
export function makeFetchManyToManyFunc(options: manyToManyOptions) {
	return (query: string | string[], queryOptions: { model: Model }) => {
		const entity = options.entity();
		const joinEntity = options.joinEntity();
		const oppositeEntity = options.oppositeEntity();

		const queryString = typeof query === "string" ? query : "";
		const args = new oppositeEntity().getSearchConditions(queryString) ?? [];

		return store.apolloClient
			.query({
				query: gql`
					query ${
						options.oppositeEntityName
					}($args: [[WhereExpressionGraph]], $skip:Int, $take:Int, $orderBy: [OrderByGraph], $ids: [ID], $id: ID) {
						${options.oppositeEntityName}s: ${
					options.oppositeEntityName
				}sConditional(conditions: $args, skip: $skip, take: $take, orderBy: $orderBy, ids: $ids, id: $id) {
							${oppositeEntity.getAttributes().join("\n")}
							${oppositeEntity
								.getFiles()
								.map((f) => f.name)
								.join("\n")}
							${options.relationName}s {
								${joinEntity.getAttributes().join("\n")}
								${joinEntity
									.getFiles()
									.map((f) => f.name)
									.join("\n")}
								${options.relationName} {
									${entity.getAttributes().join("\n")}
									${entity
										.getFiles()
										.map((f) => f.name)
										.join("\n")}
								}
							}
						}
					}`,
				fetchPolicy: "network-only",
				variables: {
					id: options.id,
					ids: options.ids,
					args: options.args?.(queryString, args) ?? args,
					skip: options.skip,
					take: options.take ?? 10,
					orderBy: options.orderBy,
					case: options.case ?? "INVARIANT_CULTURE_IGNORE_CASE",
				},
			})
			.then((result) => {
				const data = result.data[`${options.oppositeEntityName}s`];
				const serverData = _.flatMap(data, (i) => {
					const join = new joinEntity({
						[`${options.relationOppositeName}Id`]: i.id,
						[`${options.relationOppositeName}`]: i,
					});
					join[crudId] = uuid.v4();
					return {
						display: new oppositeEntity(i).getDisplayName(),
						value: join,
					};
				});
				const existingRelations = queryOptions.model[`${options.relationOppositeName}s`];
				for (const existingEntity of existingRelations) {
					existingEntity[crudId] = existingEntity.id;
					if (
						!_.find(serverData, (o) =>
							makeJoinEqualsFunc(`${options.relationOppositeName}Id`)(
								existingEntity,
								o.value[`${options.relationOppositeName}Id`]
							)
						)
					) {
						serverData.push({
							display: new oppositeEntity(
								existingEntity[options.relationOppositeName]
							).getDisplayName(),
							value: existingEntity,
						});
					}
				}
				return serverData;
			});
	};
}

export type oneToManyOptions<T extends Model> = {
	relationName: string;
	oppositeEntity: () => { new (): T; fetch<T>(variables?: IConditionalFetchArgs<T>): Promise<T[]> };
};

export function makeFetchOneToManyFunc<T extends Model>(options: oneToManyOptions<T>) {
	return async (query: string | string[], queryOptions: { model: Model }) => {
		const oppositeEntity = options.oppositeEntity();

		const serverData = await oppositeEntity.fetch<T>({
			args: typeof query === "string" ? new oppositeEntity().getSearchConditions(query) : undefined,
			take: 10,
		});

		const existingData: T[] = queryOptions.model[options.relationName];

		return _.unionBy(existingData, serverData, "id").map((r) => ({
			display: r.getDisplayName(),
			value: r,
		}));
	};
}

export function makeEnumFetchFunction<T extends string>(
	enumOptions: { [key in T]: string }
): { display: string; value: string }[] {
	return _.map(enumOptions, (display, value) => ({ display, value }));
}

export function isRequired(model: Model, attributeName: string) {
	let required = false;
	if (model[Symbols.validatorMap]) {
		const maybeValidator = model[Symbols.validatorMap][attributeName];
		required = maybeValidator ? maybeValidator.indexOf("Required") > -1 : false;
	}
	return required;
}
