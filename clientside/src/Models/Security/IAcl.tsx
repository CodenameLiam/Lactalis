export interface IAcl {
	group?: string;
	isVisitorAcl: boolean;
	canRead: () => boolean;
	canCreate: () => boolean;
	canUpdate: () => boolean;
	canDelete: () => boolean;
}
