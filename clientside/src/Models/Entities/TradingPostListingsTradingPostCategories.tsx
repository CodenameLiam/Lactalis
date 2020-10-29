import { Model, IModelAttributes, attribute, entity } from "Models/Model";
import * as Models from "Models/Entities";
import { IAcl } from "../Security/IAcl";
import { observable } from "mobx";
import { AdminTradingPostCategoriesEntity } from "../Security/Acl/AdminTradingPostCategoriesEntity";
import { FarmerTradingPostCategoriesEntity } from "../Security/Acl/FarmerTradingPostCategoriesEntity";

export interface ITradingPostListingsTradingPostCategoriesAttributes extends IModelAttributes {
	tradingPostListingsId: string;
	tradingPostCategoriesId: string;

	tradingPostListings: Models.TradingPostListingEntity | Models.ITradingPostListingEntityAttributes;
	tradingPostCategories:
		| Models.TradingPostCategoryEntity
		| Models.ITradingPostCategoryEntityAttributes;
}

@entity("TradingPostListingsTradingPostCategories")
export default class TradingPostListingsTradingPostCategories
	extends Model
	implements ITradingPostListingsTradingPostCategoriesAttributes {
	public static acls: IAcl[] = [
		new AdminTradingPostCategoriesEntity(),
		new FarmerTradingPostCategoriesEntity(),
	];

	@observable
	@attribute()
	public tradingPostListingsId: string;

	@observable
	@attribute()
	public tradingPostCategoriesId: string;

	@observable
	@attribute({ isReference: true })
	public tradingPostListings: Models.TradingPostListingEntity;

	@observable
	@attribute({ isReference: true })
	public tradingPostCategories: Models.TradingPostCategoryEntity;

	constructor(attributes?: Partial<ITradingPostListingsTradingPostCategoriesAttributes>) {
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
					this.tradingPostListings = new Models.TradingPostListingEntity(
						attributes.tradingPostListings
					);
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
					this.tradingPostCategories = new Models.TradingPostCategoryEntity(
						attributes.tradingPostCategories
					);
					this.tradingPostCategoriesId = this.tradingPostCategories.id;
				}
			} else if (attributes.tradingPostCategoriesId !== undefined) {
				this.tradingPostCategoriesId = attributes.tradingPostCategoriesId;
			}
		}
	}
}
