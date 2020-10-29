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
import { AdminQualityDocumentCategoryEntity } from "Models/Security/Acl/AdminQualityDocumentCategoryEntity";
import { FarmerQualityDocumentCategoryEntity } from "Models/Security/Acl/FarmerQualityDocumentCategoryEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IQualityDocumentCategoryEntityAttributes extends IModelAttributes {
	name: string;

	qualityDocumentss: Array<Models.QualityDocumentEntity | Models.IQualityDocumentEntityAttributes>;
}

@entity("QualityDocumentCategoryEntity", "Quality Document Category")
export default class QualityDocumentCategoryEntity
	extends Model
	implements IQualityDocumentCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new AdminQualityDocumentCategoryEntity(),
		new FarmerQualityDocumentCategoryEntity(),
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
		name: "Quality Documentss",
		displayType: "reference-multicombobox",
		order: 20,
		referenceTypeFunc: () => Models.QualityDocumentEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "qualityDocumentss",
			oppositeEntity: () => Models.QualityDocumentEntity,
		}),
	})
	public qualityDocumentss: Models.QualityDocumentEntity[] = [];

	constructor(attributes?: Partial<IQualityDocumentCategoryEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IQualityDocumentCategoryEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.qualityDocumentss) {
				for (const model of attributes.qualityDocumentss) {
					if (model instanceof Models.QualityDocumentEntity) {
						this.qualityDocumentss.push(model);
					} else {
						this.qualityDocumentss.push(new Models.QualityDocumentEntity(model));
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
		qualityDocumentss {
			${Models.QualityDocumentEntity.getAttributes().join("\n")}
			${Models.QualityDocumentEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			qualityDocumentss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["qualityDocumentss"],
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
