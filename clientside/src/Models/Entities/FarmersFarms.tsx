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
import { AdminFarmsEntity } from '../Security/Acl/AdminFarmsEntity';
import { FarmerFarmsEntity } from '../Security/Acl/FarmerFarmsEntity';

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IFarmersFarmsAttributes extends IModelAttributes {
	farmersId: string;
	farmsId: string;

	farmers: Models.FarmerEntity | Models.IFarmerEntityAttributes;
	farms: Models.FarmEntity | Models.IFarmEntityAttributes;
	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

@entity('FarmersFarms')
export default class FarmersFarms extends Model implements IFarmersFarmsAttributes {
	public static acls: IAcl[] = [
		new AdminFarmsEntity(),
		new FarmerFarmsEntity(),
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	@observable
	@attribute()
	public farmersId: string;

	@observable
	@attribute()
	public farmsId: string;

	@observable
	@attribute({isReference: true})
	public farmers: Models.FarmerEntity;

	@observable
	@attribute({isReference: true})
	public farms: Models.FarmEntity;
	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IFarmersFarmsAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

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

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}