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
import { Model, IModelAttributes, attribute, entity } from 'Models/Model';
import * as Models from 'Models/Entities';
import { IAcl } from '../Security/IAcl';
import { observable } from 'mobx';
import { AdminTradingPostCategoriesEntity } from '../Security/Acl/AdminTradingPostCategoriesEntity';
import { FarmerTradingPostCategoriesEntity } from '../Security/Acl/FarmerTradingPostCategoriesEntity';

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface ITradingPostListingsTradingPostCategoriesAttributes extends IModelAttributes {
	tradingPostListingsId: string;
	tradingPostCategoriesId: string;

	tradingPostListings: Models.TradingPostListingEntity | Models.ITradingPostListingEntityAttributes;
	tradingPostCategories: Models.TradingPostCategoryEntity | Models.ITradingPostCategoryEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('TradingPostListingsTradingPostCategories')
export default class TradingPostListingsTradingPostCategories extends Model implements ITradingPostListingsTradingPostCategoriesAttributes {
	public static acls: IAcl[] = [
		new AdminTradingPostCategoriesEntity(),
		new FarmerTradingPostCategoriesEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	@observable
	@attribute()
	public tradingPostListingsId: string;

	@observable
	@attribute()
	public tradingPostCategoriesId: string;

	@observable
	@attribute({isReference: true})
	public tradingPostListings: Models.TradingPostListingEntity;

	@observable
	@attribute({isReference: true})
	public tradingPostCategories: Models.TradingPostCategoryEntity;
	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<ITradingPostListingsTradingPostCategoriesAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		if (attributes) {
			if (attributes.tradingPostListingsId) {
				this.tradingPostListingsId = attributes.tradingPostListingsId;
			}
			if (attributes.tradingPostCategoriesId) {
				this.tradingPostCategoriesId = attributes.tradingPostCategoriesId;
			}

			if (attributes.tradingPostListings) {
				if (attributes.tradingPostListings instanceof Models.TradingPostListingEntity) {
					this.tradingPostListings = attributes.tradingPostListings;
					this.tradingPostListingsId = attributes.tradingPostListings.id;
				} else {
					this.tradingPostListings = new Models.TradingPostListingEntity(attributes.tradingPostListings);
					this.tradingPostListingsId = this.tradingPostListings.id;
				}
			} else if (attributes.tradingPostListingsId !== undefined) {
				this.tradingPostListingsId = attributes.tradingPostListingsId;
			}

			if (attributes.tradingPostCategories) {
				if (attributes.tradingPostCategories instanceof Models.TradingPostCategoryEntity) {
					this.tradingPostCategories = attributes.tradingPostCategories;
					this.tradingPostCategoriesId = attributes.tradingPostCategories.id;
				} else {
					this.tradingPostCategories = new Models.TradingPostCategoryEntity(attributes.tradingPostCategories);
					this.tradingPostCategoriesId = this.tradingPostCategories.id;
				}
			} else if (attributes.tradingPostCategoriesId !== undefined) {
				this.tradingPostCategoriesId = attributes.tradingPostCategoriesId;
			}
		}

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}