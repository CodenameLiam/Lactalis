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

export type priceType =
	// % protected region % [Override priceType keys here] off begin
	'AMOUNT' |
		'NEGOTIABLE' |
		'FREE' |
		'SWAPTRADE';
	// % protected region % [Override priceType keys here] end

export const priceTypeOptions: { [key in priceType]: string } = {
	// % protected region % [Override priceType display fields here] off begin
	AMOUNT: 'Amount',
	NEGOTIABLE: 'Negotiable',
	FREE: 'Free',
	SWAPTRADE: 'SwapTrade',
	// % protected region % [Override priceType display fields here] end
};

export type state =
	// % protected region % [Override state keys here] off begin
	'QLD' |
		'NSW' |
		'VIC' |
		'TAS' |
		'WA' |
		'SA' |
		'NT';
	// % protected region % [Override state keys here] end

export const stateOptions: { [key in state]: string } = {
	// % protected region % [Override state display fields here] off begin
	QLD: 'QLD',
	NSW: 'NSW',
	VIC: 'VIC',
	TAS: 'TAS',
	WA: 'WA',
	SA: 'SA',
	NT: 'NT',
	// % protected region % [Override state display fields here] end
};

// % protected region % [Add any extra enums here] off begin
// % protected region % [Add any extra enums here] end
