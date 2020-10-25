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
import { AdminNewsArticleEntity } from 'Models/Security/Acl/AdminNewsArticleEntity';
import { FarmerNewsArticleEntity } from 'Models/Security/Acl/FarmerNewsArticleEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface INewsArticleEntityAttributes extends IModelAttributes {
	title: string;
	content: string;
	qld: boolean;
	nsw: boolean;
	vic: boolean;
	tas: boolean;
	wa: boolean;
	sa: boolean;
	nt: boolean;

	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('NewsArticleEntity', 'News Article')
// % protected region % [Customise your entity metadata here] end
export default class NewsArticleEntity extends Model implements INewsArticleEntityAttributes {
	public static acls: IAcl[] = [
		new AdminNewsArticleEntity(),
		new FarmerNewsArticleEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'Title'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Title',
		displayType: 'textfield',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public title: string;
	// % protected region % [Modify props to the crud options here for attribute 'Title'] end

	// % protected region % [Modify props to the crud options here for attribute 'Content'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Content',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public content: string;
	// % protected region % [Modify props to the crud options here for attribute 'Content'] end

	// % protected region % [Modify props to the crud options here for attribute 'QLD'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'QLD',
		displayType: 'checkbox',
		order: 30,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public qld: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'QLD'] end

	// % protected region % [Modify props to the crud options here for attribute 'NSW'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'NSW',
		displayType: 'checkbox',
		order: 40,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public nsw: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'NSW'] end

	// % protected region % [Modify props to the crud options here for attribute 'VIC'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'VIC',
		displayType: 'checkbox',
		order: 50,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public vic: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'VIC'] end

	// % protected region % [Modify props to the crud options here for attribute 'TAS'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'TAS',
		displayType: 'checkbox',
		order: 60,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public tas: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'TAS'] end

	// % protected region % [Modify props to the crud options here for attribute 'WA'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'WA',
		displayType: 'checkbox',
		order: 70,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public wa: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'WA'] end

	// % protected region % [Modify props to the crud options here for attribute 'SA'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'SA',
		displayType: 'checkbox',
		order: 80,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public sa: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'SA'] end

	// % protected region % [Modify props to the crud options here for attribute 'NT'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'NT',
		displayType: 'checkbox',
		order: 90,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseBoolean,
		displayFunction: attr => attr ? 'True' : 'False',
	})
	public nt: boolean;
	// % protected region % [Modify props to the crud options here for attribute 'NT'] end

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<INewsArticleEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<INewsArticleEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.title) {
				this.title = attributes.title;
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
	`;
	// % protected region % [Customize Default Expands here] end

	/**
	 * The save method that is called from the admin CRUD components.
	 */
	// % protected region % [Customize Save From Crud here] off begin
	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
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