import * as React from "react";
import { Model, IModelType } from "Models/Model";
import { RouteComponentProps } from "react-router";
import { observer, inject } from "mobx-react";
import EntityAttributeList from "./EntityAttributeList";
import { EntityFormMode } from "../Helpers/Common";
import { Query, QueryResult } from "react-apollo";
import { getFetchSingleQuery, getModelDisplayName, getModelName } from "Util/EntityUtils";
import { lowerCaseFirst } from "Util/StringUtils";

interface IEntityEditProps<T extends Model> extends RouteComponentProps<IEntityEditRouteParams> {
	modelType: IModelType;
	formMode: EntityFormMode;
}

interface IEntityEditRouteParams {
	id?: string;
}

@inject("store")
@observer
class EntityEdit<T extends Model> extends React.Component<IEntityEditProps<T>, any> {
	public render() {
		const { modelType } = this.props;
		const query = getFetchSingleQuery(modelType);
		const modelName = getModelDisplayName(modelType);
		const dataReturnName = lowerCaseFirst(getModelName(modelType));

		const title = `${
			this.props.formMode === "create" ? "Create" : this.props.formMode === "edit" ? "Edit" : "View"
		} ${modelName}`;
		const sectionClassName = "crud__" + this.props.formMode;
		const options = { title, sectionClassName };

		if (this.props.match.params.id === null) {
			throw new Error("Expected id of model to fetch for edit");
		}

		/* Refetch the model */
		return (
			<Query
				query={query}
				fetchPolicy="network-only"
				variables={{
					args: [{ path: "id", comparison: "equal", value: this.props.match.params.id }],
				}}>
				{({ loading, error, data }: QueryResult) => {
					if (loading) {
						return <div>Loading {modelName}...</div>;
					}
					if (error) {
						return <div>Error Loading {modelName}</div>;
					}
					return (
						<EntityAttributeList
							{...this.props}
							model={new modelType(data[dataReturnName])}
							{...options}
							formMode={this.props.formMode}
							modelType={this.props.modelType}
						/>
					);
				}}
			</Query>
		);
	}
}

export default EntityEdit;
