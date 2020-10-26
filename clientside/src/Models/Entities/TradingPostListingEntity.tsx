/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from 'react';
import _ from 'lodash';
import moment from 'moment';
import { action, observable, runInAction } from 'mobx';
import { IAttributeGroup, Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from 'Util/EntityUtils';
import { AdminTradingPostListingEntity } from 'Models/Security/Acl/AdminTradingPostListingEntity';
import { FarmerTradingPostListingEntity } from 'Models/Security/Acl/FarmerTradingPostListingEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

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
	tradingPostCategoriess: Array<Models.TradingPostListingsTradingPostCategories | Models.ITradingPostListingsTradingPostCategoriesAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('TradingPostListingEntity', 'Trading Post Listing')
// % protected region % [Customise your entity metadata here] end
export default class TradingPostListingEntity extends Model implements ITradingPostListingEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTradingPostListingEntity(),
		new FarmerTradingPostListingEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [
		// % protected region % [Add any custom create exclusions here] off begin
		// % protected region % [Add any custom create exclusions here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = [
		// % protected region % [Add any custom update exclusions here] off begin
		// % protected region % [Add any custom update exclusions here] end
	];

	// % protected region % [Modify props to the crud options here for attribute 'Title'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Title',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public title: string;
	// % protected region % [Modify props to the crud options here for attribute 'Title'] end

	// % protected region % [Modify props to the crud options here for attribute 'Email'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Email',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public email: string;
	// % protected region % [Modify props to the crud options here for attribute 'Email'] end

	// % protected region % [Modify props to the crud options here for attribute 'Phone'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Phone',
		displayType: 'textfield',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public phone: string;
	// % protected region % [Modify props to the crud options here for attribute 'Phone'] end

	// % protected region % [Modify props to the crud options here for attribute 'Additional Info'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Additional Info',
		displayType: 'textfield',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public additionalInfo: string;
	// % protected region % [Modify props to the crud options here for attribute 'Additional Info'] end

	// % protected region % [Modify props to the crud options here for attribute 'Address Line 1'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Address Line 1',
		displayType: 'textfield',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public addressLine1: string;
	// % protected region % [Modify props to the crud options here for attribute 'Address Line 1'] end

	// % protected region % [Modify props to the crud options here for attribute 'Address Line 2'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Address Line 2',
		displayType: 'textfield',
		order: 60,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public addressLine2: string;
	// % protected region % [Modify props to the crud options here for attribute 'Address Line 2'] end

	// % protected region % [Modify props to the crud options here for attribute 'Postal Code'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Postal Code',
		displayType: 'textfield',
		order: 70,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public postalCode: string;
	// % protected region % [Modify props to the crud options here for attribute 'Postal Code'] end

	// % protected region % [Modify props to the crud options here for attribute 'Product Image'] off begin
	@observable
	@attribute({file: 'productImage'})
	@CRUD({
		name: 'Product Image',
		displayType: 'file',
		order: 80,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseUuid,
		inputProps: {
			imageOnly: true,
		},
		fileAttribute: 'productImage',
		displayFunction: attr => attr ? <img src={`${SERVER_URL}/api/files/${attr}`} style={{maxWidth: '300px'}} /> : 'No File Attached',
	})
	public productImageId: string;
	@observable
	public productImage: Blob;
	// % protected region % [Modify props to the crud options here for attribute 'Product Image'] end

	// % protected region % [Modify props to the crud options here for attribute 'Price'] off begin
	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: 'Price',
		displayType: 'textfield',
		order: 90,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
	})
	public price: number;
	// % protected region % [Modify props to the crud options here for attribute 'Price'] end

	// % protected region % [Modify props to the crud options here for attribute 'Price Type'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Price Type',
		displayType: 'enum-combobox',
		order: 100,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.priceTypeOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.priceTypeOptions),
		displayFunction: (attribute: Enums.priceType) => Enums.priceTypeOptions[attribute],
	})
	public priceType: Enums.priceType;
	// % protected region % [Modify props to the crud options here for attribute 'Price Type'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Farmer'] off begin
		name: 'Farmer',
		displayType: 'reference-combobox',
		order: 110,
		referenceTypeFunc: () => Models.FarmerEntity,
		// % protected region % [Modify props to the crud options here for reference 'Farmer'] end
	})
	public farmerId?: string;
	@observable
	@attribute({isReference: true})
	public farmer: Models.FarmerEntity;

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Trading Post Categories'] off begin
		name: 'Trading Post Categories',
		displayType: 'reference-multicombobox',
		order: 120,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.TradingPostListingsTradingPostCategories,
		optionEqualFunc: makeJoinEqualsFunc('tradingPostCategoriesId'),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: 'tradingPostListingEntity',
			oppositeEntityName: 'tradingPostCategoryEntity',
			relationName: 'tradingPostListings',
			relationOppositeName: 'tradingPostCategories',
			entity: () => Models.TradingPostListingEntity,
			joinEntity: () => Models.TradingPostListingsTradingPostCategories,
			oppositeEntity: () => Models.TradingPostCategoryEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Trading Post Categories'] end
	})
	public tradingPostCategoriess: Models.TradingPostListingsTradingPostCategories[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ITradingPostListingEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ITradingPostListingEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
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
						this.tradingPostCategoriess.push(new Models.TradingPostListingsTradingPostCategories(model));
					}
				}
			}
			// % protected region % [Override assign attributes here] end

			// % protected region % [Add any extra assign attributes logic here] off begin
			// % protected region % [Add any extra assign attributes logic here] end
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */
	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
		tradingPostCategoriess {
			${Models.TradingPostListingsTradingPostCategories.getAttributes().join('\n')}
			tradingPostCategories {
				${Models.TradingPostCategoryEntity.getAttributes().join('\n')}
			}
		}
		farmer {
			${Models.FarmerEntity.getAttributes().join('\n')}
			${Models.FarmerEntity.getFiles().map(f => f.name).join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			tradingPostCategoriess: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'tradingPostCategoriess',
						]
					},
				],
				contentType: 'multipart/form-data',
			}
		);
	}
	// % protected region % [Customize Save From Crud here] end

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}