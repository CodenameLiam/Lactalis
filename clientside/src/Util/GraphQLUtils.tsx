import { ServerError, ServerParseError } from "apollo-link-http-common";
import { ErrorResponse } from "apollo-link-error";
import {
	CaseComparison,
	CaseComparisonPascalCase,
	IWhereCondition,
} from "Views/Components/ModelCollection/ModelQuery";

/**
 * Check if a GraphQL error is a server error(instead of a GraphQL error)
 * @param error the returned error from server GraphQL
 * @param params The query params to use
 * @returns boolean, if the error is ServerError and have error['statusCode']
 */
export function isServerError(
	error: Error | ServerError | ServerParseError | undefined
): error is ServerError | ServerParseError {
	if (error === undefined) {
		return false;
	}
	if (error["statusCode"] === undefined) {
		return false;
	}
	return true;
}

export function getTheNetworkError(response: ErrorResponse): string | null {
	if (
		!!response.networkError &&
		!!response.networkError["result"] &&
		!!response.networkError["result"]["errors"] &&
		!!response.networkError["result"]["errors"]["length"]
	) {
		var error = response.networkError["result"]["errors"][0];
		return error["message"];
	} else {
		return null;
	}
}

/**
 * Return an array of error messages from graphQl error response
 * @param response
 */
export function getErrorMessages(response: ErrorResponse): string[] {
	const errors =
		!!response.response && !!response.response.data
			? !!response.response.data.errors && Array.isArray(response.response.data.errors)
				? response.response.data.errors
				: [{ message: response.response.data }]
			: [];
	return errors;
}

/**
 * Checks if the condition has any OR conditions
 * A query has OR conditions if it is an array of arrays
 * @param conditions The condition to check
 */
export function isOrCondition<T>(
	conditions: Array<IWhereCondition<T>> | Array<Array<IWhereCondition<T>>> | undefined
): conditions is Array<Array<IWhereCondition<T>>> {
	if (conditions === undefined) {
		return false;
	}

	if (conditions[0]) {
		return Array.isArray(conditions[0]);
	}
	return false;
}

/**
 * Converts a case comparison to its pascal case version
 * @param comparison The comparison to convert
 */
export function convertCaseComparisonToPascalCase(
	comparison: CaseComparison
): CaseComparisonPascalCase {
	const comparisonMap: { [key in CaseComparison]: CaseComparisonPascalCase } = {
		CURRENT_CULTURE: "CurrentCulture",
		CURRENT_CULTURE_IGNORE_CASE: "CurrentCultureIgnoreCase",
		INVARIANT_CULTURE: "InvariantCulture",
		INVARIANT_CULTURE_IGNORE_CASE: "InvariantCultureIgnoreCase",
		ORDINAL: "Ordinal",
		ORDINAL_IGNORE_CASE: "OrdinalIgnoreCase",
	};

	return comparisonMap[comparison];
}
