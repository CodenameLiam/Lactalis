export type IconPositions = "icon-top" | "icon-left" | "icon-bottom" | "icon-right";

export interface IIconProps {
	icon?: string;
	iconPos?: IconPositions;
}

export enum EntityFormMode {
	VIEW = "view",
	CREATE = "create",
	EDIT = "edit",
}

export enum AttributeFormMode {
	VIEW = "view",
	EDIT = "edit",
	HIDE = "hide",
}
