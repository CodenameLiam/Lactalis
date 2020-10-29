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
import { AdminTechnicalDocumentCategoryEntity } from "Models/Security/Acl/AdminTechnicalDocumentCategoryEntity";
import { FarmerTechnicalDocumentCategoryEntity } from "Models/Security/Acl/FarmerTechnicalDocumentCategoryEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface ITechnicalDocumentCategoryEntityAttributes extends IModelAttributes {
	name: string;

	technicalDocumentss: Array<
		Models.TechnicalDocumentEntity | Models.ITechnicalDocumentEntityAttributes
	>;
}

@entity("TechnicalDocumentCategoryEntity", "Technical Document Category")
export default class TechnicalDocumentCategoryEntity
	extends Model
	implements ITechnicalDocumentCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTechnicalDocumentCategoryEntity(),
		new FarmerTechnicalDocumentCategoryEntity(),
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
		name: "Technical Documentss",
		displayType: "reference-multicombobox",
		order: 20,
		referenceTypeFunc: () => Models.TechnicalDocumentEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "technicalDocumentss",
			oppositeEntity: () => Models.TechnicalDocumentEntity,
		}),
	})
	public technicalDocumentss: Models.TechnicalDocumentEntity[] = [];

	constructor(attributes?: Partial<ITechnicalDocumentCategoryEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ITechnicalDocumentCategoryEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.technicalDocumentss) {
				for (const model of attributes.technicalDocumentss) {
					if (model instanceof Models.TechnicalDocumentEntity) {
						this.technicalDocumentss.push(model);
					} else {
						this.technicalDocumentss.push(new Models.TechnicalDocumentEntity(model));
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
		technicalDocumentss {
			${Models.TechnicalDocumentEntity.getAttributes().join("\n")}
			${Models.TechnicalDocumentEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			technicalDocumentss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["technicalDocumentss"],
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
