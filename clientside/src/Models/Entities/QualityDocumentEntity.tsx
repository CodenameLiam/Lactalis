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
import { AdminQualityDocumentEntity } from "Models/Security/Acl/AdminQualityDocumentEntity";
import { FarmerQualityDocumentEntity } from "Models/Security/Acl/FarmerQualityDocumentEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";
import { FileListPreview } from "Views/Components/CRUD/Attributes/AttributeFile";

export interface IQualityDocumentEntityAttributes extends IModelAttributes {
	fileId: string;
	file: Blob;
	name: string;

	qualityDocumentCategoryId?: string;
	qualityDocumentCategory?:
		| Models.QualityDocumentCategoryEntity
		| Models.IQualityDocumentCategoryEntityAttributes;
}

@entity("QualityDocumentEntity", "Quality Document")
export default class QualityDocumentEntity
	extends Model
	implements IQualityDocumentEntityAttributes {
	public static acls: IAcl[] = [
		new AdminQualityDocumentEntity(),
		new FarmerQualityDocumentEntity(),
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
		name: "Quality Document Category",
		displayType: "reference-combobox",
		order: 30,
		referenceTypeFunc: () => Models.QualityDocumentCategoryEntity,
	})
	public qualityDocumentCategoryId?: string;
	@observable
	@attribute({ isReference: true })
	public qualityDocumentCategory: Models.QualityDocumentCategoryEntity;

	constructor(attributes?: Partial<IQualityDocumentEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IQualityDocumentEntityAttributes>) {
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
			if (attributes.qualityDocumentCategory) {
				if (attributes.qualityDocumentCategory instanceof Models.QualityDocumentCategoryEntity) {
					this.qualityDocumentCategory = attributes.qualityDocumentCategory;
					this.qualityDocumentCategoryId = attributes.qualityDocumentCategory.id;
				} else {
					this.qualityDocumentCategory = new Models.QualityDocumentCategoryEntity(
						attributes.qualityDocumentCategory
					);
					this.qualityDocumentCategoryId = this.qualityDocumentCategory.id;
				}
			} else if (attributes.qualityDocumentCategoryId !== undefined) {
				this.qualityDocumentCategoryId = attributes.qualityDocumentCategoryId;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		qualityDocumentCategory {
			${Models.QualityDocumentCategoryEntity.getAttributes().join("\n")}
			${Models.QualityDocumentCategoryEntity.getFiles()
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
