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
import { AdminMilkTestEntity } from "Models/Security/Acl/AdminMilkTestEntity";
import { FarmerMilkTestEntity } from "Models/Security/Acl/FarmerMilkTestEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IMilkTestEntityAttributes extends IModelAttributes {
	time: Date;
	volume: number;
	temperature: number;
	milkFat: number;
	protein: number;

	farmId?: string;
	farm?: Models.FarmEntity | Models.IFarmEntityAttributes;
}

@entity("MilkTestEntity", "Milk Test")
export default class MilkTestEntity extends Model implements IMilkTestEntityAttributes {
	public static acls: IAcl[] = [new AdminMilkTestEntity(), new FarmerMilkTestEntity()];

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
		name: "Time",
		displayType: "datetimepicker",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseDate,
	})
	public time: Date;

	@Validators.Integer()
	@observable
	@attribute()
	@CRUD({
		name: "Volume",
		displayType: "textfield",
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseInteger,
	})
	public volume: number;

	@Validators.Numeric()
	@observable
	@attribute()
	@CRUD({
		name: "Temperature",
		displayType: "textfield",
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseFloat,
	})
	public temperature: number;

	@Validators.Numeric()
	@observable
	@attribute()
	@CRUD({
		name: "Milk Fat",
		displayType: "textfield",
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseFloat,
	})
	public milkFat: number;

	@Validators.Numeric()
	@observable
	@attribute()
	@CRUD({
		name: "Protein",
		displayType: "textfield",
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseFloat,
	})
	public protein: number;

	@observable
	@attribute()
	@CRUD({
		name: "Farm",
		displayType: "reference-combobox",
		order: 60,
		referenceTypeFunc: () => Models.FarmEntity,
	})
	public farmId?: string;
	@observable
	@attribute({ isReference: true })
	public farm: Models.FarmEntity;

	constructor(attributes?: Partial<IMilkTestEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IMilkTestEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.time) {
				this.time = moment(attributes.time).toDate();
			}
			if (attributes.volume) {
				this.volume = attributes.volume;
			}
			if (attributes.temperature) {
				this.temperature = attributes.temperature;
			}
			if (attributes.milkFat) {
				this.milkFat = attributes.milkFat;
			}
			if (attributes.protein) {
				this.protein = attributes.protein;
			}
			if (attributes.farm) {
				if (attributes.farm instanceof Models.FarmEntity) {
					this.farm = attributes.farm;
					this.farmId = attributes.farm.id;
				} else {
					this.farm = new Models.FarmEntity(attributes.farm);
					this.farmId = this.farm.id;
				}
			} else if (attributes.farmId !== undefined) {
				this.farmId = attributes.farmId;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		farm {
			${Models.FarmEntity.getAttributes().join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: [],
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
