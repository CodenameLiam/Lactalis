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
import { AdminFarmerEntity } from "Models/Security/Acl/AdminFarmerEntity";
import { FarmerFarmerEntity } from "Models/Security/Acl/FarmerFarmerEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IFarmerEntityAttributes extends IModelAttributes {
	email: string;

	tradingPostListingss: Array<
		Models.TradingPostListingEntity | Models.ITradingPostListingEntityAttributes
	>;
	farmss: Array<Models.FarmersFarms | Models.IFarmersFarmsAttributes>;
}

@entity("FarmerEntity", "Farmer")
export default class FarmerEntity extends Model implements IFarmerEntityAttributes {
	public static acls: IAcl[] = [new AdminFarmerEntity(), new FarmerFarmerEntity()];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = ["email"];

	@Validators.Email()
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: "Email",
		displayType: "displayfield",
		order: 10,
		createFieldType: "textfield",
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public email: string;

	@Validators.Length(6)
	@observable
	@CRUD({
		name: "Password",
		displayType: "hidden",
		order: 20,
		createFieldType: "password",
	})
	public password: string;

	@Validators.Custom("Password Match", (e: string, target: FarmerEntity) => {
		return new Promise((resolve) =>
			resolve(target.password !== e ? "Password fields do not match" : null)
		);
	})
	@observable
	@CRUD({
		name: "Confirm Password",
		displayType: "hidden",
		order: 30,
		createFieldType: "password",
	})
	public _confirmPassword: string;

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Trading Post Listingss",
		displayType: "reference-multicombobox",
		order: 40,
		referenceTypeFunc: () => Models.TradingPostListingEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "tradingPostListingss",
			oppositeEntity: () => Models.TradingPostListingEntity,
		}),
	})
	public tradingPostListingss: Models.TradingPostListingEntity[] = [];

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Farms",
		displayType: "reference-multicombobox",
		order: 50,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.FarmersFarms,
		optionEqualFunc: makeJoinEqualsFunc("farmsId"),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: "farmerEntity",
			oppositeEntityName: "farmEntity",
			relationName: "farmers",
			relationOppositeName: "farms",
			entity: () => Models.FarmerEntity,
			joinEntity: () => Models.FarmersFarms,
			oppositeEntity: () => Models.FarmEntity,
		}),
	})
	public farmss: Models.FarmersFarms[] = [];

	constructor(attributes?: Partial<IFarmerEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IFarmerEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email) {
				this.email = attributes.email;
			}
			if (attributes.tradingPostListingss) {
				for (const model of attributes.tradingPostListingss) {
					if (model instanceof Models.TradingPostListingEntity) {
						this.tradingPostListingss.push(model);
					} else {
						this.tradingPostListingss.push(new Models.TradingPostListingEntity(model));
					}
				}
			}
			if (attributes.farmss) {
				for (const model of attributes.farmss) {
					if (model instanceof Models.FarmersFarms) {
						this.farmss.push(model);
					} else {
						this.farmss.push(new Models.FarmersFarms(model));
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
		farmss {
			${Models.FarmersFarms.getAttributes().join("\n")}
			farms {
				${Models.FarmEntity.getAttributes().join("\n")}
			}
		}
		tradingPostListingss {
			${Models.TradingPostListingEntity.getAttributes().join("\n")}
			${Models.TradingPostListingEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			farmss: {},
			tradingPostListingss: {},
		};

		if (formMode === "create") {
			relationPath["password"] = {};

			if (this.password !== this._confirmPassword) {
				throw "Password fields do not match";
			}
		}
		return this.save(relationPath, {
			graphQlInputType:
				formMode === "create"
					? `[${this.getModelName()}CreateInput]`
					: `[${this.getModelName()}Input]`,
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["tradingPostListingss", "farmss"],
				},
			],
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.email;
	}
}
