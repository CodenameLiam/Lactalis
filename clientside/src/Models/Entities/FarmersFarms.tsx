import { Model, IModelAttributes, attribute, entity } from "Models/Model";
import * as Models from "Models/Entities";
import { IAcl } from "../Security/IAcl";
import { observable } from "mobx";
import { AdminFarmsEntity } from "../Security/Acl/AdminFarmsEntity";
import { FarmerFarmsEntity } from "../Security/Acl/FarmerFarmsEntity";

export interface IFarmersFarmsAttributes extends IModelAttributes {
	farmersId: string;
	farmsId: string;

	farmers: Models.FarmerEntity | Models.IFarmerEntityAttributes;
	farms: Models.FarmEntity | Models.IFarmEntityAttributes;
}

@entity("FarmersFarms")
export default class FarmersFarms extends Model implements IFarmersFarmsAttributes {
	public static acls: IAcl[] = [new AdminFarmsEntity(), new FarmerFarmsEntity()];

	@observable
	@attribute()
	public farmersId: string;

	@observable
	@attribute()
	public farmsId: string;

	@observable
	@attribute({ isReference: true })
	public farmers: Models.FarmerEntity;

	@observable
	@attribute({ isReference: true })
	public farms: Models.FarmEntity;

	constructor(attributes?: Partial<IFarmersFarmsAttributes>) {
		super(attributes);

		if (attributes) {
			if (attributes.farmersId) {
				this.farmersId = attributes.farmersId;
			}
			if (attributes.farmsId) {
				this.farmsId = attributes.farmsId;
			}

			if (attributes.farmers) {
				if (attributes.farmers instanceof Models.FarmerEntity) {
					this.farmers = attributes.farmers;
					this.farmersId = attributes.farmers.id;
				} else {
					this.farmers = new Models.FarmerEntity(attributes.farmers);
					this.farmersId = this.farmers.id;
				}
			} else if (attributes.farmersId !== undefined) {
				this.farmersId = attributes.farmersId;
			}

			if (attributes.farms) {
				if (attributes.farms instanceof Models.FarmEntity) {
					this.farms = attributes.farms;
					this.farmsId = attributes.farms.id;
				} else {
					this.farms = new Models.FarmEntity(attributes.farms);
					this.farmsId = this.farms.id;
				}
			} else if (attributes.farmsId !== undefined) {
				this.farmsId = attributes.farmsId;
			}
		}
	}
}
