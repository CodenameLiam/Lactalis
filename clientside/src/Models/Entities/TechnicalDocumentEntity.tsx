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
import { AdminTechnicalDocumentEntity } from 'Models/Security/Acl/AdminTechnicalDocumentEntity';
import { FarmerTechnicalDocumentEntity } from 'Models/Security/Acl/FarmerTechnicalDocumentEntity';
import * as Enums from '../Enums';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
import { SERVER_URL } from 'Constants';
import { FileListPreview } from 'Views/Components/CRUD/Attributes/AttributeFile';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ITechnicalDocumentEntityAttributes extends IModelAttributes {
	fileId: string;
	file: Blob;
	name: string;

	technicalDocumentCategoryId?: string;
	technicalDocumentCategory?: Models.TechnicalDocumentCategoryEntity | Models.ITechnicalDocumentCategoryEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('TechnicalDocumentEntity', 'Technical Document')
// % protected region % [Customise your entity metadata here] end
export default class TechnicalDocumentEntity extends Model implements ITechnicalDocumentEntityAttributes {
	public static acls: IAcl[] = [
		new AdminTechnicalDocumentEntity(),
		new FarmerTechnicalDocumentEntity(),
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

	// % protected region % [Modify props to the crud options here for attribute 'File'] off begin
	@observable
	@attribute({file: 'file'})
	@CRUD({
		name: 'File',
		displayType: 'file',
		order: 10,
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseUuid,
		fileAttribute: 'file',
		displayFunction: attr => attr
			? <FileListPreview url={attr} />
			: 'No File Attached',
	})
	public fileId: string;
	@observable
	public file: Blob;
	// % protected region % [Modify props to the crud options here for attribute 'File'] end

	// % protected region % [Modify props to the crud options here for attribute 'Name'] off begin
	@observable
	@attribute()
	@CRUD({
		name: 'Name',
		displayType: 'textfield',
		order: 20,
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
	})
	public name: string;
	// % protected region % [Modify props to the crud options here for attribute 'Name'] end

	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for reference 'Technical Document Category'] off begin
		name: 'Technical Document Category',
		displayType: 'reference-combobox',
		order: 30,
		referenceTypeFunc: () => Models.TechnicalDocumentCategoryEntity,
		// % protected region % [Modify props to the crud options here for reference 'Technical Document Category'] end
	})
	public technicalDocumentCategoryId?: string;
	@observable
	@attribute({isReference: true})
	public technicalDocumentCategory: Models.TechnicalDocumentCategoryEntity;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ITechnicalDocumentEntityAttributes>) {
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
	public assignAttributes(attributes?: Partial<ITechnicalDocumentEntityAttributes>) {
		// % protected region % [Override assign attributes here] off begin
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
				if (attributes.technicalDocumentCategory instanceof Models.TechnicalDocumentCategoryEntity) {
					this.technicalDocumentCategory = attributes.technicalDocumentCategory;
					this.technicalDocumentCategoryId = attributes.technicalDocumentCategory.id;
				} else {
					this.technicalDocumentCategory = new Models.TechnicalDocumentCategoryEntity(attributes.technicalDocumentCategory);
					this.technicalDocumentCategoryId = this.technicalDocumentCategory.id;
				}
			} else if (attributes.technicalDocumentCategoryId !== undefined) {
				this.technicalDocumentCategoryId = attributes.technicalDocumentCategoryId;
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
		technicalDocumentCategory {
			${Models.TechnicalDocumentCategoryEntity.getAttributes().join('\n')}
			${Models.TechnicalDocumentCategoryEntity.getFiles().map(f => f.name).join('\n')}
		}
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
				contentType: 'multipart/form-data',
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