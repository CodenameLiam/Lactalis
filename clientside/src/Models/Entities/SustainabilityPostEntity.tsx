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
import { AdminSustainabilityPostEntity } from "Models/Security/Acl/AdminSustainabilityPostEntity";
import { FarmerSustainabilityPostEntity } from "Models/Security/Acl/FarmerSustainabilityPostEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";
import { FileListPreview } from "Views/Components/CRUD/Attributes/AttributeFile";

export interface ISustainabilityPostEntityAttributes extends IModelAttributes {
	title: string;
	imageId: string;
	image: Blob;
	fileId: string;
	file: Blob;
	content: string;
}

@entity("SustainabilityPostEntity", "Sustainability Post")
export default class SustainabilityPostEntity
	extends Model
	implements ISustainabilityPostEntityAttributes {
	public static acls: IAcl[] = [
		new AdminSustainabilityPostEntity(),
		new FarmerSustainabilityPostEntity(),
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
		name: "Title",
		displayType: "textfield",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public title: string;

	@observable
	@attribute({ file: "image" })
	@CRUD({
		name: "Image",
		displayType: "file",
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseUuid,
		inputProps: {
			imageOnly: true,
		},
		fileAttribute: "image",
		displayFunction: (attr) =>
			attr ? (
				<img src={`${SERVER_URL}/api/files/${attr}`} style={{ maxWidth: "300px" }} />
			) : (
				"No File Attached"
			),
	})
	public imageId: string;
	@observable
	public image: Blob;

	@observable
	@attribute({ file: "file" })
	@CRUD({
		name: "File",
		displayType: "file",
		order: 30,
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
		name: "Content",
		displayType: "textfield",
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public content: string;

	constructor(attributes?: Partial<ISustainabilityPostEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<ISustainabilityPostEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.title) {
				this.title = attributes.title;
			}
			if (attributes.image) {
				this.image = attributes.image;
			}
			if (attributes.imageId) {
				this.imageId = attributes.imageId;
			}
			if (attributes.file) {
				this.file = attributes.file;
			}
			if (attributes.fileId) {
				this.fileId = attributes.fileId;
			}
			if (attributes.content) {
				this.content = attributes.content;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
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
