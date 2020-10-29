export type priceType = "AMOUNT" | "NEGOTIABLE" | "FREE" | "SWAPTRADE";

export const priceTypeOptions: { [key in priceType]: string } = {
	AMOUNT: "Amount",
	NEGOTIABLE: "Negotiable",
	FREE: "Free",
	SWAPTRADE: "SwapTrade",
};

export type state = "QLD" | "NSW" | "VIC" | "WA" | "SA" | "TAS" | "NT";

export const stateOptions: { [key in state]: string } = {
	QLD: "QLD",
	NSW: "NSW",
	VIC: "VIC",
	WA: "WA",
	SA: "SA",
	TAS: "TAS",
	NT: "NT",
};
