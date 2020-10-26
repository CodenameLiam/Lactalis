/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from 'react';
import _ from 'lodash';
import moment from 'moment';
import { action, observable, runInAction } from 'mobx';
import { IAttributeGroup, Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from 'Util/EntityUtils';
import { AdminPromotedArticlesEntity } from 'Models/Security/Acl/AdminPromotedArticlesEntity';
import { FarmerPromotedArticlesEntity } from 'Models/Security/Acl/FarmerPromotedArticlesEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IPromotedArticlesEntityAttributes extends IModelAttributes {
	state: Enums.state;

	newsArticless: Array<Models.NewsArticleEntity | Models.INewsArticleEntityAttributes>;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('PromotedArticlesEntity', 'Promoted Articles')
// % protected region % [Customise your entity metadata here] end
export default class PromotedArticlesEntity extends Model implements IPromotedArticlesEntityAttributes {
	public static acls: IAcl[] = [
		new AdminPromotedArticlesEntity(),
		new FarmerPromotedArticlesEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [
		// % protected region % [Add any custom create exclusions here] off begin
		// % protected region % [Add any custom create exclusions here] end
	];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = [
		// % protected region % [Add any custom update exclusions here] off begin
		// % protected region % [Add any custom update exclusions here] end
	];

	// % protected region % [Modify props to the crud options here for attribute 'State'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'State',
		displayType: 'enum-combobox',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: (attr: string) => {
			return AttrUtils.standardiseEnum(attr, Enums.stateOptions);
		},
		enumResolveFunction: makeEnumFetchFunction(Enums.stateOptions),
		displayFunction: (attribute: Enums.state) => Enums.stateOptions[attribute],
	})
	public state: Enums.state;
	// % protected region % [Modify props to the crud options here for attribute 'State'] end

	@observable
	@attribute({isReference: true})
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'News Articles'] off begin
		name: "News Articless",
		displayType: 'reference-multicombobox',
		order: 20,
		referenceTypeFunc: () => Models.NewsArticleEntity,
		referenceResolveFunction: makeFetchOneToManyFunc({
			relationName: 'newsArticless',
			oppositeEntity: () => Models.NewsArticleEntity,
		}),
		// % protected region % [Modify props to the crud options here for reference 'News Articles'] end
	})
	public newsArticless: Models.NewsArticleEntity[] = [];

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IPromotedArticlesEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IPromotedArticlesEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.state) {
				this.state = attributes.state;
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
			// % protected region % [Override assign attributes here] end

			// % protected region % [Add any extra assign attributes logic here] off begin
			// % protected region % [Add any extra assign attributes logic here] end
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */
	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
		newsArticless {
			${Models.NewsArticleEntity.getAttributes().join('\n')}
			${Models.NewsArticleEntity.getFiles().map(f => f.name).join('\n')}
		}
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
			newsArticless: {},
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
							'newsArticless',
						]
					},
				],
			}
		);
	}
	// % protected region % [Customize Save From Crud here] end

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}


	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}