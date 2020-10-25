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

import { INewsArticleEntityAttributes as INewsArticleEntityAttributesImport } from './NewsArticleEntity';
import { IAdminEntityAttributes as IAdminEntityAttributesImport } from './AdminEntity';
import { IFarmerEntityAttributes as IFarmerEntityAttributesImport } from './FarmerEntity';
import { IFarmEntityAttributes as IFarmEntityAttributesImport } from './FarmEntity';
import { IMilkTestEntityAttributes as IMilkTestEntityAttributesImport } from './MilkTestEntity';
import { IFarmersFarmsAttributes as IFarmersFarmsAttributesImport } from './FarmersFarms';

export { default as User } from './User';

export { default as NewsArticleEntity } from './NewsArticleEntity';
export type INewsArticleEntityAttributes = INewsArticleEntityAttributesImport;

export { default as AdminEntity } from './AdminEntity';
export type IAdminEntityAttributes = IAdminEntityAttributesImport;

export { default as FarmerEntity } from './FarmerEntity';
export type IFarmerEntityAttributes = IFarmerEntityAttributesImport;

export { default as FarmEntity } from './FarmEntity';
export type IFarmEntityAttributes = IFarmEntityAttributesImport;

export { default as MilkTestEntity } from './MilkTestEntity';
export type IMilkTestEntityAttributes = IMilkTestEntityAttributesImport;

export { default as FarmersFarms } from './FarmersFarms';
export type IFarmersFarmsAttributes = IFarmersFarmsAttributesImport;

