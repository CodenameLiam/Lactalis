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
import { AdminImportantDocumentCategoryEntity } from "Models/Security/Acl/AdminImportantDocumentCategoryEntity";
import { FarmerImportantDocumentCategoryEntity } from "Models/Security/Acl/FarmerImportantDocumentCategoryEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IImportantDocumentCategoryEntityAttributes extends IModelAttributes {
	name: string;

	importantDocumentss: Array<
		Models.ImportantDocumentEntity | Models.IImportantDocumentEntityAttributes
	>;
}

@entity("ImportantDocumentCategoryEntity", "Important Document Category")
export default class ImportantDocumentCategoryEntity
	extends Model
	implements IImportantDocumentCategoryEntityAttributes {
	public static acls: IAcl[] = [
		new AdminImportantDocumentCategoryEntity(),
		new FarmerImportantDocumentCategoryEntity(),
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
		name: "Important Documentss",
		displayType: "reference-multicombobox",
		order: 20,
		referenceTypeFunc: () => Models.ImportantDocumentEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "importantDocumentss",
			oppositeEntity: () => Models.ImportantDocumentEntity,
		}),
	})
	public importantDocumentss: Models.ImportantDocumentEntity[] = [];

	constructor(attributes?: Partial<IImportantDocumentCategoryEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IImportantDocumentCategoryEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.importantDocumentss) {
				for (const model of attributes.importantDocumentss) {
					if (model instanceof Models.ImportantDocumentEntity) {
						this.importantDocumentss.push(model);
					} else {
						this.importantDocumentss.push(new Models.ImportantDocumentEntity(model));
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
		importantDocumentss {
			${Models.ImportantDocumentEntity.getAttributes().join("\n")}
			${Models.ImportantDocumentEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			importantDocumentss: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["importantDocumentss"],
				},
			],
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.name;
	}
}
