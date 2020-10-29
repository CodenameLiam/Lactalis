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
import { AdminTechnicalDocumentEntity } from "Models/Security/Acl/AdminTechnicalDocumentEntity";
import { FarmerTechnicalDocumentEntity } from "Models/Security/Acl/FarmerTechnicalDocumentEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";
import { FileListPreview } from "Views/Components/CRUD/Attributes/AttributeFile";

export interface ITechnicalDocumentEntityAttributes extends IModelAttributes {
	fileId: string;
	file: Blob;
	name: string;

	technicalDocumentCategoryId?: string;
	technicalDocumentCategory?:
		| Models.TechnicalDocumentCategoryEntity
		| Models.ITechnicalDocumentCategoryEntityAttributes;
}

@entity("TechnicalDocumentEntity", "Technical Document")
export default class TechnicalDocumentEntity
	extends Model
	implements ITechnicalDocumentEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTechnicalDocumentEntity(),
		new FarmerTechnicalDocumentEntity(),
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
	@attribute({ file: "file" })
	@CRUD({
		name: "File",
		displayType: "file",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseUuid,
		fileAttribute: "file",
		displayFunction: (attr) => (attr ? <FileListPreview url={attr} /> : "No File Attached"),
	})
	public fileId: string;
	@observable
	public file: Blob;

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
		name: "Technical Document Category",
		displayType: "reference-combobox",
		order: 30,
		referenceTypeFunc: () => Models.TechnicalDocumentCategoryEntity,
	})
	public technicalDocumentCategoryId?: string;
	@observable
	@attribute({ isReference: true })
	public technicalDocumentCategory: Models.TechnicalDocumentCategoryEntity;

	constructor(attributes?: Partial<ITechnicalDocumentEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ITechnicalDocumentEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.file) {
				this.file = attributes.file;
			}
			if (attributes.fileId) {
				this.fileId = attributes.fileId;
			}
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.technicalDocumentCategory) {
				if (
					attributes.technicalDocumentCategory instanceof Models.TechnicalDocumentCategoryEntity
				) {
					this.technicalDocumentCategory = attributes.technicalDocumentCategory;
					this.technicalDocumentCategoryId = attributes.technicalDocumentCategory.id;
				} else {
					this.technicalDocumentCategory = new Models.TechnicalDocumentCategoryEntity(
						attributes.technicalDocumentCategory
					);
					this.technicalDocumentCategoryId = this.technicalDocumentCategory.id;
				}
			} else if (attributes.technicalDocumentCategoryId !== undefined) {
				this.technicalDocumentCategoryId = attributes.technicalDocumentCategoryId;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		technicalDocumentCategory {
			${Models.TechnicalDocumentCategoryEntity.getAttributes().join("\n")}
			${Models.TechnicalDocumentCategoryEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
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
