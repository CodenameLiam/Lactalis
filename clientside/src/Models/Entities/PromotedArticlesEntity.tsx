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
import { AdminPromotedArticlesEntity } from "Models/Security/Acl/AdminPromotedArticlesEntity";
import { FarmerPromotedArticlesEntity } from "Models/Security/Acl/FarmerPromotedArticlesEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IPromotedArticlesEntityAttributes extends IModelAttributes {
	name: string;

	newsArticless: Array<Models.NewsArticleEntity | Models.INewsArticleEntityAttributes>;
}

@entity("PromotedArticlesEntity", "Promoted Articles")
export default class PromotedArticlesEntity
	extends Model
	implements IPromotedArticlesEntityAttributes {
	public static acls: IAcl[] = [
		new AdminPromotedArticlesEntity(),
		new FarmerPromotedArticlesEntity(),
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
		name: "News Articless",
		displayType: "reference-multicombobox",
		order: 20,
		referenceTypeFunc: () => Models.NewsArticleEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: "newsArticless",
			oppositeEntity: () => Models.NewsArticleEntity,
		}),
	})
	public newsArticless: Models.NewsArticleEntity[] = [];

	constructor(attributes?: Partial<IPromotedArticlesEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IPromotedArticlesEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
			if (attributes.newsArticless) {
				for (const model of attributes.newsArticless) {
					if (model instanceof Models.NewsArticleEntity) {
						this.newsArticless.push(model);
					} else {
						this.newsArticless.push(new Models.NewsArticleEntity(model));
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
		newsArticless {
			${Models.NewsArticleEntity.getAttributes().join("\n")}
			${Models.NewsArticleEntity.getFiles()
				.map((f) => f.name)
				.join("\n")}
		}
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			newsArticless: {},
		};
		return this.save(relationPath, {
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: ["newsArticless"],
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
