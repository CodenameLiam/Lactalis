import * as React from "react";
import { QueryResult, Query, OperationVariables } from "react-apollo";
import { DocumentNode } from "graphql";
import { Model } from "Models/Model";
import { PaginationQueryOptions } from "Models/PaginationData";
import { getFetchAllQuery, getFetchAllConditional } from "Util/EntityUtils";
import { observer } from "mobx-react";
import { isOrCondition } from "Util/GraphQLUtils";

export type Comparators =
	| "contains"
	| "endsWith"
	| "equal"
	| "greaterThan"
	| "greaterThanOrEqual"
	| "in"
	| "notIn"
	| "lessThan"
	| "lessThanOrEqual"
	| "like"
	| "notEqual"
	| "startsWith";

export interface IOrderByCondition<T> {
	path: string;
	descending?: boolean;
}

export type CaseComparison =
	| "CURRENT_CULTURE"
	| "CURRENT_CULTURE_IGNORE_CASE"
	| "INVARIANT_CULTURE"
	| "INVARIANT_CULTURE_IGNORE_CASE"
	| "ORDINAL"
	| "ORDINAL_IGNORE_CASE";

export type CaseComparisonPascalCase =
	| "CurrentCulture"
	| "CurrentCultureIgnoreCase"
	| "InvariantCulture"
	| "InvariantCultureIgnoreCase"
	| "Ordinal"
	| "OrdinalIgnoreCase";

interface BaseWhereCondition<T> {
	path: string;
	comparison: Comparators;
	value: any;
}

export interface IWhereCondition<T> extends BaseWhereCondition<T> {
	case?: CaseComparison;
}

export interface IWhereConditionApi<T> extends BaseWhereCondition<T> {
	case?: CaseComparisonPascalCase;
}

export interface IModelQueryVariables<T> {
	skip?: number;
	take?: number;
	args?: Array<IWhereCondition<T>>;
	orderBy: Array<IOrderByCondition<T>>;
	ids?: string[];
}

export interface IModelQueryProps<T extends Model, TData = any> {
	children: (result: QueryResult<TData, OperationVariables>) => JSX.Element | null;
	model: { new (json?: {}): T };
	conditions?: Array<IWhereCondition<T>> | Array<Array<IWhereCondition<T>>>;
	ids?: string[];
	orderBy?: IOrderByCondition<T>;
	customQuery?: DocumentNode;
	pagination: PaginationQueryOptions;
	useListExpands?: boolean;
	expandString?: string;
}

@observer
class ModelQuery<T extends Model, TData = any> extends React.Component<IModelQueryProps<T, TData>> {
	public render() {
		let fetchAllQuery;

		if (isOrCondition(this.props.conditions)) {
			fetchAllQuery = getFetchAllConditional(
				this.props.model,
				this.props.expandString,
				this.props.useListExpands
			);
		} else {
			fetchAllQuery = getFetchAllQuery(
				this.props.model,
				this.props.expandString,
				this.props.useListExpands
			);
		}

		return (
			<Query
				fetchPolicy="network-only"
				notifyOnNetworkStatusChange={true}
				query={this.props.customQuery || fetchAllQuery}
				variables={this.constructVariables()}>
				{this.props.children}
			</Query>
		);
	}

	private constructVariables() {
		const { conditions, ids, orderBy: orderByProp, pagination } = this.props;
		const { page, perPage } = pagination;

		let orderBy: IOrderByCondition<T> = {
			path: new this.props.model().getDisplayAttribute(),
			descending: false,
		};

		if (orderByProp) {
			orderBy = orderByProp;
		}

		return {
			skip: page * perPage,
			take: perPage,
			args: conditions,
			orderBy: [orderBy],
			ids,
		};
	}
}

export default ModelQuery;
