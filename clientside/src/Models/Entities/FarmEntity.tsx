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
import { AdminFarmEntity } from "Models/Security/Acl/AdminFarmEntity";
import { FarmerFarmEntity } from "Models/Security/Acl/FarmerFarmEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IFarmEntityAttributes extends IModelAttributes {
	code: string;
	name: string;
	state: Enums.state;

	pickupss: Array<Models.MilkTestEntity | Models.IMilkTestEntityAttributes>;
	farmerss: Array<Models.FarmersFarms | Models.IFarmersFarmsAttributes>;
}

@entity("FarmEntity", "Farm")
export default class FarmEntity extends Model implements IFarmEntityAttributes {
	public static acls: IAcl[] = [new AdminFarmEntity(), new FarmerFarmEntity()];

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
		name: "Code",
		displayType: "textfield",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public code: string;

	@observable
	@attribute()
	@CRUD({
		name: "Name",
		displayType: "textfield",
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;

	@observable
	@attribute()
	@CRUD({
		name: "State",
		displayType: "enum-combobox",
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.stateOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.stateOptions),
		displayFunction: (attribute: Enums.state) => Enums.stateOptions[attribute],
	})
	public state: Enums.state;

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Pickupss",
		displayType: "reference-multicombobox",
		order: 40,
		referenceTypeFunc: () => Models.MilkTestEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "pickupss",
			oppositeEntity: () => Models.MilkTestEntity,
		}),
	})
	public pickupss: Models.MilkTestEntity[] = [];

	@observable
	@attribute({ isReference: true })
	@CRUD({
		name: "Farmers",
		displayType: "reference-multicombobox",
		order: 50,
		isJoinEntity: true,
		referenceTypeFunc: () => Models.FarmersFarms,
		optionEqualFunc: makeJoinEqualsFunc("farmersId"),
		referenceResolveFunction: makeFetchManyToManyFunc({
			entityName: "farmEntity",
			oppositeEntityName: "farmerEntity",
			relationName: "farms",
			relationOppositeName: "farmers",
			entity: () => Models.FarmEntity,
			joinEntity: () => Models.FarmersFarms,
			oppositeEntity: () => Models.FarmerEntity,
		}),
	})
	public farmerss: Models.FarmersFarms[] = [];

	constructor(attributes?: Partial<IFarmEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IFarmEntityAttributes>) {
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
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		farmerss {
			${Models.FarmersFarms.getAttributes().join("\n")}
			farmers {
				${Models.FarmerEntity.getAttributes().join("\n")}
			}
		}
		pickupss {
			${Models.MilkTestEntity.getAttributes().join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			farmerss: {},
			pickupss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["pickupss", "farmerss"],
				},
			],
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.code;
	}
}
