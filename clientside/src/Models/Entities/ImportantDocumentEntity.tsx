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
import { AdminImportantDocumentEntity } from "Models/Security/Acl/AdminImportantDocumentEntity";
import { FarmerImportantDocumentEntity } from "Models/Security/Acl/FarmerImportantDocumentEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";
import { FileListPreview } from "Views/Components/CRUD/Attributes/AttributeFile";

export interface IImportantDocumentEntityAttributes extends IModelAttributes {
	fileId: string;
	file: Blob;
	name: string;
	qld: boolean;
	nsw: boolean;
	vic: boolean;
	tas: boolean;
	wa: boolean;
	sa: boolean;
	nt: boolean;

	documentCategoryId?: string;
	documentCategory?:
		| Models.ImportantDocumentCategoryEntity
		| Models.IImportantDocumentCategoryEntityAttributes;
}

@entity("ImportantDocumentEntity", "Important Document")
export default class ImportantDocumentEntity
	extends Model
	implements IImportantDocumentEntityAttributes {
	public static acls: IAcl[] = [
		new AdminImportantDocumentEntity(),
		new FarmerImportantDocumentEntity(),
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
		name: "QLD",
		displayType: "checkbox",
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public qld: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "NSW",
		displayType: "checkbox",
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public nsw: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "VIC",
		displayType: "checkbox",
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public vic: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "TAS",
		displayType: "checkbox",
		order: 60,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public tas: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "WA",
		displayType: "checkbox",
		order: 70,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public wa: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "SA",
		displayType: "checkbox",
		order: 80,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public sa: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "NT",
		displayType: "checkbox",
		order: 90,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public nt: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "Document Category",
		displayType: "reference-combobox",
		order: 100,
		referenceTypeFunc: () => Models.ImportantDocumentCategoryEntity,
	})
	public documentCategoryId?: string;
	@observable
	@attribute({ isReference: true })
	public documentCategory: Models.ImportantDocumentCategoryEntity;

	constructor(attributes?: Partial<IImportantDocumentEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IImportantDocumentEntityAttributes>) {
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
			if (attributes.qld) {
				this.qld = attributes.qld;
			}
			if (attributes.nsw) {
				this.nsw = attributes.nsw;
			}
			if (attributes.vic) {
				this.vic = attributes.vic;
			}
			if (attributes.tas) {
				this.tas = attributes.tas;
			}
			if (attributes.wa) {
				this.wa = attributes.wa;
			}
			if (attributes.sa) {
				this.sa = attributes.sa;
			}
			if (attributes.nt) {
				this.nt = attributes.nt;
			}
			if (attributes.documentCategory) {
				if (attributes.documentCategory instanceof Models.ImportantDocumentCategoryEntity) {
					this.documentCategory = attributes.documentCategory;
					this.documentCategoryId = attributes.documentCategory.id;
				} else {
					this.documentCategory = new Models.ImportantDocumentCategoryEntity(
						attributes.documentCategory
					);
					this.documentCategoryId = this.documentCategory.id;
				}
			} else if (attributes.documentCategoryId !== undefined) {
				this.documentCategoryId = attributes.documentCategoryId;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		documentCategory {
			${Models.ImportantDocumentCategoryEntity.getAttributes().join("\n")}
			${Models.ImportantDocumentCategoryEntity.getFiles()
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
		return this.name;
	}
}
