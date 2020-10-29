import * as queryString from "querystring";
import { convertCaseComparisonToPascalCase } from "Util/GraphQLUtils";
import { IWhereCondition, IWhereConditionApi } from "Views/Components/ModelCollection/ModelQuery";

/**
 * Handles the response from a javascript fetch call
 * @param response The response to handle
 * @returns the response as a json
 */
export function handleFetchedResponse(response: Response) {
	const responseJson = response.json();
	if (!response.ok) {
		return responseJson.then(Promise.reject.bind(Promise));
	}
	return responseJson;
}

/**
 * Builds a url from a route and query params
 * @param route The route of the url
 * @param params The query params to use
 * @returns The built url
 */
export function buildUrl(route: string, params?: { [key: string]: string }) {
	if (params) {
		return `${route}?${queryString.stringify(params)}`;
	}

	return route;
}

/**
 * Formats a collection filter for use with rest endpoints
 * @param collectionFilters an array of array of IWhereCondition
 */
export function formatConditionsForRest<T>(collectionFilters: Array<Array<IWhereCondition<T>>>) {
	return [
		...collectionFilters.map((andCondition) =>
			andCondition.map((orCondition) => {
				if (orCondition.case) {
					return {
						...orCondition,
						case: convertCaseComparisonToPascalCase(orCondition.case),
						value: [orCondition.value],
					};
				}
				return orCondition as IWhereConditionApi<T>;
			})
		),
	];
}
