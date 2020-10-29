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
import { AdminTradingPostListingEntity } from "Models/Security/Acl/AdminTradingPostListingEntity";
import { FarmerTradingPostListingEntity } from "Models/Security/Acl/FarmerTradingPostListingEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface ITradingPostListingEntityAttributes extends IModelAttributes {
	title: string;
	email: string;
	phone: string;
	additionalInfo: string;
	addressLine1: string;
	addressLine2: string;
	postalCode: string;
	productImageId: string;
	productImage: Blob;
	price: number;
	priceType: Enums.priceType;

	farmerId?: string;
	farmer?: Models.FarmerEntity | Models.IFarmerEntityAttributes;
	tradingPostCategoriess: Array<
		| Models.TradingPostListingsTradingPostCategories
		| Models.ITradingPostListingsTradingPostCategoriesAttributes
	>;
}

@entity("TradingPostListingEntity", "Trading Post Listing")
export default class TradingPostListingEntity
	extends Model
	implements ITradingPostListingEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTradingPostListingEntity(),
		new FarmerTradingPostListingEntity(),
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
		name: "Title",
		displayType: "textfield",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public title: string;

	@observable
	@attribute()
	@CRUD({
		name: "Email",
		displayType: "textfield",
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public email: string;

	@observable
	@attribute()
	@CRUD({
		name: "Phone",
		displayType: "textfield",
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public phone: string;

	@observable
	@attribute()
	@CRUD({
		name: "Additional Info",
		displayType: "textfield",
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public additionalInfo: string;

	@observable
	@attribute()
	@CRUD({
		name: "Address Line 1",
		displayType: "textfield",
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public addressLine1: string;

	@observable
	@attribute()
	@CRUD({
		name: "Address Line 2",
		displayType: "textfield",
		order: 60,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public addressLine2: string;

	@observable
	@attribute()
	@CRUD({
		name: "Postal Code",
		displayType: "textfield",
		order: 70,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public postalCode: string;

	@observable
	@attribute({ file: "productImage" })
	@CRUD({
		name: "Product Image",
		displayType: "file",
		order: 80,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseUuid,
		inputProps: {
			imageOnly: true,
		},
		fileAttribute: "productImage",
		displayFunction: (attr) =>
			attr ? (
				<img src={`${SERVER_URL}/api/files/${attr}`} style={{ maxWidth: "300px" }} />
			) : (
				"No File Attached"
			),
	})
	public productImageId: string;
	@observable
	public productImage: Blob;

	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: "Price",
		displayType: "textfield",
		order: 90,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseInteger,
	})
	public price: number;

	@observable
	@attribute()
	@CRUD({
		name: "Price Type",
		displayType: "enum-combobox",
		order: 100,
		searchable: true,
		searchFunction: "equal",
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.priceTypeOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.priceTypeOptions),
		displayFunction: (attribute: Enums.priceType) => Enums.priceTypeOptions[attribute],
	})
	public priceType: Enums.priceType;

	@observable
	@attribute()
	@CRUD({
		name: "Farmer",
		displayType: "reference-combobox",
		order: 110,
		referenceTypeFunc: () => Models.FarmerEntity,
	})
	public farmerId?: string;
	@observable
	@attribute({ isReference: true })
	public farmer: Models.FarmerEntity;

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Trading Post Categories",
		displayType: "reference-multicombobox",
		order: 120,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.TradingPostListingsTradingPostCategories,
		optionEqualFunc: makeJoinEqualsFunc("tradingPostCategoriesId"),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: "tradingPostListingEntity",
			oppositeEntityName: "tradingPostCategoryEntity",
			relationName: "tradingPostListings",
			relationOppositeName: "tradingPostCategories",
			entity: () => Models.TradingPostListingEntity,
			joinEntity: () => Models.TradingPostListingsTradingPostCategories,
			oppositeEntity: () => Models.TradingPostCategoryEntity,
		}),
	})
	public tradingPostCategoriess: Models.TradingPostListingsTradingPostCategories[] = [];

	constructor(attributes?: Partial<ITradingPostListingEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ITradingPostListingEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.title) {
				this.title = attributes.title;
			}
			if (attributes.email) {
				this.email = attributes.email;
			}
			if (attributes.phone) {
				this.phone = attributes.phone;
			}
			if (attributes.additionalInfo) {
				this.additionalInfo = attributes.additionalInfo;
			}
			if (attributes.addressLine1) {
				this.addressLine1 = attributes.addressLine1;
			}
			if (attributes.addressLine2) {
				this.addressLine2 = attributes.addressLine2;
			}
			if (attributes.postalCode) {
				this.postalCode = attributes.postalCode;
			}
			if (attributes.productImage) {
				this.productImage = attributes.productImage;
			}
			if (attributes.productImageId) {
				this.productImageId = attributes.productImageId;
			}
			if (attributes.price) {
				this.price = attributes.price;
			}
			if (attributes.priceType) {
				this.priceType = attributes.priceType;
			}
			if (attributes.farmer) {
				if (attributes.farmer instanceof Models.FarmerEntity) {
					this.farmer = attributes.farmer;
					this.farmerId = attributes.farmer.id;
				} else {
					this.farmer = new Models.FarmerEntity(attributes.farmer);
					this.farmerId = this.farmer.id;
				}
			} else if (attributes.farmerId !== undefined) {
				this.farmerId = attributes.farmerId;
			}
			if (attributes.tradingPostCategoriess) {
				for (const model of attributes.tradingPostCategoriess) {
					if (model instanceof Models.TradingPostListingsTradingPostCategories) {
						this.tradingPostCategoriess.push(model);
					} else {
						this.tradingPostCategoriess.push(
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
		tradingPostCategoriess {
			${Models.TradingPostListingsTradingPostCategories.getAttributes().join("\n")}
			tradingPostCategories {
				${Models.TradingPostCategoryEntity.getAttributes().join("\n")}
			}
		}
		farmer {
			${Models.FarmerEntity.getAttributes().join("\n")}
			${Models.FarmerEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			tradingPostCategoriess: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["tradingPostCategoriess"],
				},
			],
			contentType: "multipart/form-data",
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.id;
	}
}
