import * as React from "react";
import _ from "lodash";
import moment from "moment";
import { action, observable, runInAction } from "mobx";
import {
	IAttributeGroup,
	Model,
	IModelAttributes,
	attribute,
	entity,
	jsonReplacerFn,
} from "Models/Model";
import * as Validators from "Validators";
import * as Models from "../Entities";
import { CRUD } from "../CRUDOptions";
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from "Models/Security/IAcl";
import {
	makeFetchManyToManyFunc,
	makeFetchOneToManyFunc,
	makeJoinEqualsFunc,
	makeEnumFetchFunction,
} from "Util/EntityUtils";
import { AdminTradingPostCategoryEntity } from "Models/Security/Acl/AdminTradingPostCategoryEntity";
import { FarmerTradingPostCategoryEntity } from "Models/Security/Acl/FarmerTradingPostCategoryEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface ITradingPostCategoryEntityAttributes extends IModelAttributes {
	name: string;

	tradingPostListingss: Array<
		| Models.TradingPostListingsTradingPostCategories
		| Models.ITradingPostListingsTradingPostCategoriesAttributes
	>;
}

@entity("TradingPostCategoryEntity", "Trading Post Category")
export default class TradingPostCategoryEntity
	extends Model
	implements ITradingPostCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTradingPostCategoryEntity(),
		new FarmerTradingPostCategoryEntity(),
	];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = [];

	@observable
	@attribute()
	@CRUD({
		name: "Name",
		displayType: "textfield",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Trading Post Listings",
		displayType: "reference-multicombobox",
		order: 20,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.TradingPostListingsTradingPostCategories,
		optionEqualFunc: makeJoinEqualsFunc("tradingPostListingsId"),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: "tradingPostCategoryEntity",
			oppositeEntityName: "tradingPostListingEntity",
			relationName: "tradingPostCategories",
			relationOppositeName: "tradingPostListings",
			entity: () => Models.TradingPostCategoryEntity,
			joinEntity: () => Models.TradingPostListingsTradingPostCategories,
			oppositeEntity: () => Models.TradingPostListingEntity,
		}),
	})
	public tradingPostListingss: Models.TradingPostListingsTradingPostCategories[] = [];

	constructor(attributes?: Partial<ITradingPostCategoryEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ITradingPostCategoryEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.tradingPostListingss) {
				for (const model of attributes.tradingPostListingss) {
					if (model instanceof Models.TradingPostListingsTradingPostCategories) {
						this.tradingPostListingss.push(model);
					} else {
						this.tradingPostListingss.push(
							new Models.TradingPostListingsTradingPostCategories(model)
						);
					}
				}
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		tradingPostListingss {
			${Models.TradingPostListingsTradingPostCategories.getAttributes().join("\n")}
			tradingPostListings {
				${Models.TradingPostListingEntity.getAttributes().join("\n")}
				${Models.TradingPostListingEntity.getFiles()
					.map((f) => f.name)
					.join("\n")}
			}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			tradingPostListingss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["tradingPostListingss"],
				},
			],
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.id;
	}
}
