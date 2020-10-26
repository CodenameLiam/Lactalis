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
import { AdminFarmEntity } from 'Models/Security/Acl/AdminFarmEntity';
import { FarmerFarmEntity } from 'Models/Security/Acl/FarmerFarmEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IFarmEntityAttributes extends IModelAttributes {
	code: string;
	name: string;
	state: Enums.state;

	pickupss: Array<Models.MilkTestEntity | Models.IMilkTestEntityAttributes>;
	farmerss: Array<Models.FarmersFarms | Models.IFarmersFarmsAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('FarmEntity', 'Farm')
// % protected region % [Customise your entity metadata here] end
export default class FarmEntity extends Model implements IFarmEntityAttributes {
	public static acls: IAcl[] = [
		new AdminFarmEntity(),
		new FarmerFarmEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Code'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Code',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public code: string;
	// % protected region % [Modify props to the crud options here for attribute 'Code'] end

	// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;
	// % protected region % [Modify props to the crud options here for attribute 'Name'] end

	// % protected region % [Modify props to the crud options here for attribute 'State'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'State',
		displayType: 'enum-combobox',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.stateOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.stateOptions),
		displayFunction: (attribute: Enums.state) => Enums.stateOptions[attribute],
	})
	public state: Enums.state;
	// % protected region % [Modify props to the crud options here for attribute 'State'] end

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Pickups'] off begin
		name: "Pickupss",
		displayType: 'reference-multicombobox',
		order: 40,
		referenceTypeFunc: () => Models.MilkTestEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'pickupss',
			oppositeEntity: () => Models.MilkTestEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Pickups'] end
	})
	public pickupss: Models.MilkTestEntity[] = [];

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Farmers'] off begin
		name: 'Farmers',
		displayType: 'reference-multicombobox',
		order: 50,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.FarmersFarms,
		optionEqualFunc: makeJoinEqualsFunc('farmersId'),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: 'farmEntity',
			oppositeEntityName: 'farmerEntity',
			relationName: 'farms',
			relationOppositeName: 'farmers',
			entity: () => Models.FarmEntity,
			joinEntity: () => Models.FarmersFarms,
			oppositeEntity: () => Models.FarmerEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'Farmers'] end
	})
	public farmerss: Models.FarmersFarms[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IFarmEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<IFarmEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.code) {
				this.code = attributes.code;
			}
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.state) {
				this.state = attributes.state;
			}
			if (attributes.pickupss) {
				for (const model of attributes.pickupss) {
					if (model instanceof Models.MilkTestEntity) {
						this.pickupss.push(model);
					} else {
						this.pickupss.push(new Models.MilkTestEntity(model));
					}
				}
			}
			if (attributes.farmerss) {
				for (const model of attributes.farmerss) {
					if (model instanceof Models.FarmersFarms) {
						this.farmerss.push(model);
					} else {
						this.farmerss.push(new Models.FarmersFarms(model));
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
		farmerss {
			${Models.FarmersFarms.getAttributes().join('\n')}
			farmers {
				${Models.FarmerEntity.getAttributes().join('\n')}
			}
		}
		pickupss {
			${Models.MilkTestEntity.getAttributes().join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			farmerss: {},
			pickupss: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'pickupss',
							'farmerss',
						]
					},
				],
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