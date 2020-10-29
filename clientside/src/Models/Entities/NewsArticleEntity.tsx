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
import { AdminNewsArticleEntity } from "Models/Security/Acl/AdminNewsArticleEntity";
import { FarmerNewsArticleEntity } from "Models/Security/Acl/FarmerNewsArticleEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface INewsArticleEntityAttributes extends IModelAttributes {
	headline: string;
	description: string;
	featureImageId: string;
	featureImage: Blob;
	content: string;
	qld: boolean;
	nsw: boolean;
	vic: boolean;
	tas: boolean;
	wa: boolean;
	sa: boolean;
	nt: boolean;

	promotedArticlesId?: string;
	promotedArticles?: Models.PromotedArticlesEntity | Models.IPromotedArticlesEntityAttributes;
}

@entity("NewsArticleEntity", "News Article")
export default class NewsArticleEntity extends Model implements INewsArticleEntityAttributes {
	public static acls: IAcl[] = [new AdminNewsArticleEntity(), new FarmerNewsArticleEntity()];

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
		name: "Headline",
		displayType: "textfield",
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public headline: string;

	@observable
	@attribute()
	@CRUD({
		name: "Description",
		displayType: "textarea",
		order: 35,
		headerColumn: false,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public description: string;

	@observable
	@attribute({ file: "featureImage" })
	@CRUD({
		name: "Feature Image",
		displayType: "file",
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseUuid,
		inputProps: {
			imageOnly: true,
		},
		fileAttribute: "featureImage",
		displayFunction: (attr) =>
			attr ? (
				<img src={`${SERVER_URL}/api/files/${attr}`} style={{ maxWidth: "300px" }} />
			) : (
				"No File Attached"
			),
	})
	public featureImageId: string;
	@observable
	public featureImage: Blob;

	@observable
	@attribute()
	@CRUD({
		name: "Content",
		displayType: "textarea",
		order: 120,
		headerColumn: false,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public content: string;

	@observable
	@attribute()
	@CRUD({
		name: "QLD",
		displayType: "checkbox",
		order: 50,
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
		order: 60,
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
		order: 70,
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
		order: 80,
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
		order: 90,
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
		order: 100,
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
		order: 110,
		searchable: true,
		searchFunction: "equal",
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: (attr) => (attr ? "True" : "False"),
	})
	public nt: boolean;

	@observable
	@attribute()
	@CRUD({
		name: "Promoted Articles",
		displayType: "reference-combobox",
		order: 120,
		referenceTypeFunc: () => Models.PromotedArticlesEntity,
	})
	public promotedArticlesId?: string;
	@observable
	@attribute({ isReference: true })
	public promotedArticles: Models.PromotedArticlesEntity;

	constructor(attributes?: Partial<INewsArticleEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<INewsArticleEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.headline) {
				this.headline = attributes.headline;
			}
			if (attributes.description) {
				this.description = attributes.description;
			}
			if (attributes.featureImage) {
				this.featureImage = attributes.featureImage;
			}
			if (attributes.featureImageId) {
				this.featureImageId = attributes.featureImageId;
			}
			if (attributes.content) {
				this.content = attributes.content;
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
			if (attributes.promotedArticles) {
				if (attributes.promotedArticles instanceof Models.PromotedArticlesEntity) {
					this.promotedArticles = attributes.promotedArticles;
					this.promotedArticlesId = attributes.promotedArticles.id;
				} else {
					this.promotedArticles = new Models.PromotedArticlesEntity(attributes.promotedArticles);
					this.promotedArticlesId = this.promotedArticles.id;
				}
			} else if (attributes.promotedArticlesId !== undefined) {
				this.promotedArticlesId = attributes.promotedArticlesId;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
		promotedArticles {
			${Models.PromotedArticlesEntity.getAttributes().join("\n")}
			${Models.PromotedArticlesEntity.getFiles()
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
		return this.headline;
	}
}
