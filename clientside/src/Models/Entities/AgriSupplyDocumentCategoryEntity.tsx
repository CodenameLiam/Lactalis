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
import { AdminAgriSupplyDocumentCategoryEntity } from "Models/Security/Acl/AdminAgriSupplyDocumentCategoryEntity";
import { FarmerAgriSupplyDocumentCategoryEntity } from "Models/Security/Acl/FarmerAgriSupplyDocumentCategoryEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IAgriSupplyDocumentCategoryEntityAttributes extends IModelAttributes {
	name: string;

	agriSupplyDocumentss: Array<
		Models.AgriSupplyDocumentEntity | Models.IAgriSupplyDocumentEntityAttributes
	>;
}

@entity("AgriSupplyDocumentCategoryEntity", "Agri Supply Document Category")
export default class AgriSupplyDocumentCategoryEntity
	extends Model
	implements IAgriSupplyDocumentCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new AdminAgriSupplyDocumentCategoryEntity(),
		new FarmerAgriSupplyDocumentCategoryEntity(),
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
		name: "Agri Supply Documentss",
		displayType: "reference-multicombobox",
		order: 20,
		referenceTypeFunc: () => Models.AgriSupplyDocumentEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "agriSupplyDocumentss",
			oppositeEntity: () => Models.AgriSupplyDocumentEntity,
		}),
	})
	public agriSupplyDocumentss: Models.AgriSupplyDocumentEntity[] = [];

	constructor(attributes?: Partial<IAgriSupplyDocumentCategoryEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IAgriSupplyDocumentCategoryEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.agriSupplyDocumentss) {
				for (const model of attributes.agriSupplyDocumentss) {
					if (model instanceof Models.AgriSupplyDocumentEntity) {
						this.agriSupplyDocumentss.push(model);
					} else {
						this.agriSupplyDocumentss.push(new Models.AgriSupplyDocumentEntity(model));
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
		agriSupplyDocumentss {
			${Models.AgriSupplyDocumentEntity.getAttributes().join("\n")}
			${Models.AgriSupplyDocumentEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			agriSupplyDocumentss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["agriSupplyDocumentss"],
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
