import * as React from "react";
import { observer } from "mobx-react";
import { Route, RouteComponentProps, Switch } from "react-router";
import { IModelType, Model } from "Models/Model";
import EntityCollection, {
	AdditionalBulkActions,
	IEntityCollectionProps,
	viewActionOptions,
} from "./EntityCollection";
import EntityAttributeList from "./EntityAttributeList";
import EntityEdit from "./EntityEdit";
import { getModelDisplayName } from "Util/EntityUtils";
import SecuredAdminPage from "../Security/SecuredAdminPage";
import { SecurityService } from "Services/SecurityService";
import { expandFn, ICollectionItemActionProps } from "../Collection/Collection";
import { IEntityContextMenuActions } from "../EntityContextMenu/EntityContextMenu";
import { EntityFormMode } from "../Helpers/Common";
import { IFilter } from "../Collection/CollectionFilterPanel";

interface IEntityCRUDProps<T extends Model> extends RouteComponentProps {
	/** The type of model to render */
	modelType: IModelType;
	/** Function to determine the expanded content of the list */
	expandList?: expandFn<T>;
	/** Number of entities per page */
	perPage?: number;
	/** Context menu actions for each row */
	actionsMore?: IEntityContextMenuActions<T>;
	/** Url suffix to add to the route */
	URLExtension?: string;
	/** Additional actions for the bulk actions menu */
	additionalBulkActions?: Array<AdditionalBulkActions<T>>;
	/** Additional table actions for the collection view */
	additionalTableActions?: Array<ICollectionItemActionProps<T>>;
	/** Additional filters to add to the collection view */
	additionalFilters?: Array<IFilter<T>>;
	/** Remove the view action from the collection */
	removeViewAction?: boolean;
	/** Override for the collection component */
	collectionComponent?: (routeProps: RouteComponentProps) => React.ReactNode;
	/** Override for the create component */
	createComponent?: (routeProps: RouteComponentProps) => React.ReactNode;
	/** If this is set to true then remove the created date filter from the collection view */
	removeCreatedFilter?: boolean;
	/** If this is set to true then remove the modified date filter from the collection view */
	removeModifiedFilter?: boolean;
	/** Change the filter orientation to row */
	filterOrientationRow?: boolean;
	/** Override for the view component */
	viewComponent?: (routeProps: RouteComponentProps) => React.ReactNode;
	/** Override for the edit component */
	editComponent?: (routeProps: RouteComponentProps) => React.ReactNode;
	/** Custom action for view on the collection. If this function returns undefined then it will disable view */
	collectionViewAction?: (
		options: viewActionOptions<T>
	) => ICollectionItemActionProps<T> | undefined;
	/** Custom action for create on the collection. If this function returns undefined then it will disable create */
	collectionCreateAction?: (options: viewActionOptions<T>) => React.ReactNode;
	/** Custom action for delete on the collection. If this function returns undefined then it will disable delete */
	collectionDeleteAction?: (
		options: viewActionOptions<T>
	) => ICollectionItemActionProps<T> | undefined;
	/** Custom action for update on the collection. If this function returns undefined then it will disable update */
	collectionUpdateAction?: (
		options: viewActionOptions<T>
	) => ICollectionItemActionProps<T> | undefined;
	/** Custom props to be passed to the entity collection for when it is being rendered. */
	entityCollectionProps?: Partial<IEntityCollectionProps<T>>;
	/** Override for disabling bulk export on the bulk select all */
	disableBulkExport?: boolean;
	/** Override for disabling bulk delete on the bulk select all */
	disableBulkDelete?: boolean;
}

/**
 * This component is used to render a CRUD (create, read, update, delete) view for a specific entity type.
 */
@observer
class EntityCRUD<T extends Model> extends React.Component<IEntityCRUDProps<T>> {
	private url = () => {
		const { URLExtension, match } = this.props;

		if (URLExtension) {
			return `${match.url}/${URLExtension}`;
		}

		return match.url;
	};

	public render() {
		const {
			match,
			modelType,
			collectionComponent,
			createComponent,
			editComponent,
			viewComponent,
		} = this.props;

		// Wrap the pages with secured page component
		const entityCollectionPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canRead(modelType)}>
					<this.renderEntityCollection {...pageProps} />
				</SecuredAdminPage>
			);
		};

		const entityCreatePage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canCreate(modelType)}>
					<this.renderEntityCreate {...pageProps} />
				</SecuredAdminPage>
			);
		};

		const entityViewPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canRead(modelType)}>
					<this.renderEntityView {...pageProps} />
				</SecuredAdminPage>
			);
		};

		const entityEditPage = (pageProps: RouteComponentProps) => {
			return (
				<SecuredAdminPage canDo={SecurityService.canUpdate(modelType)}>
					<this.renderEntityEdit {...pageProps} />
				</SecuredAdminPage>
			);
		};

		return (
			<div>
				<Switch>
					<Route
						exact={true}
						path={`${match.url}`}
						render={collectionComponent ?? entityCollectionPage}
					/>
					<Route path={`${this.url()}/view/:id`} render={viewComponent ?? entityViewPage} />
					<Route
						exact={true}
						path={`${this.url()}/create`}
						render={createComponent ?? entityCreatePage}
					/>
					<Route path={`${this.url()}/edit/:id`} render={editComponent ?? entityEditPage} />
				</Switch>
			</div>
		);
	}

	ed renderEntityCollection = (routeProps: RouteComponentProps) => {
		const {
			modelType,
			expandList,
			perPage,
			actionsMore,
			additionalBulkActions,
			additionalFilters,
			additionalTableActions,
			collectionViewAction,
			collectionCreateAction,
			collectionUpdateAction,
			collectionDeleteAction,
			entityCollectionProps,
			filterOrientationRow,
			disableBulkDelete,
			disableBulkExport,
			removeCreatedFilter,
			removeModifiedFilter,
		} = this.props;

		return (
			<EntityCollection
				{...routeProps}
				modelType={modelType}
				expandList={expandList}
				perPage={perPage}
				actionsMore={actionsMore}
				url={this.url()}
				disableBulkDelete={disableBulkDelete}
				disableBulkExport={disableBulkExport}
				additionalBulkActions={additionalBulkActions}
				additionalTableActions={additionalTableActions}
				additionalFilters={additionalFilters}
				filterOrientationRow={filterOrientationRow}
				viewAction={collectionViewAction}
				createAction={collectionCreateAction}
				deleteAction={collectionDeleteAction}
				updateAction={collectionUpdateAction}
				removeCreatedFilter={removeCreatedFilter}
				removeModifiedFilter={removeModifiedFilter}
				{...entityCollectionProps}
			/>
		);
	};

	ed renderEntityCreate = (routeProps: RouteComponentProps) => {
		const { modelType } = this.props;
		const modelDisplayName = getModelDisplayName(modelType);
		return (
			<EntityAttributeList
				{...routeProps}
				model={new modelType()}
				sectionClassName="crud__create"
				title={`Create New ${modelDisplayName}`}
				formMode={EntityFormMode.CREATE}
				modelType={modelType}
			/>
		);
	};

	ed renderEntityEdit = (routeProps: RouteComponentProps) => {
		const { modelType } = this.props;
		return <EntityEdit {...routeProps} modelType={modelType} formMode={EntityFormMode.EDIT} />;
	};

	ed renderEntityView = (routeProps: RouteComponentProps) => {
		const { modelType } = this.props;
		return <EntityEdit {...routeProps} modelType={modelType} formMode={EntityFormMode.VIEW} />;
	};

}

export default EntityCRUD;
