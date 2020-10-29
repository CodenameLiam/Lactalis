import { IAcl } from "../IAcl";

export class AdminAdminEntity implements IAcl {
	public group?: string;
	public isVisitorAcl = false;
	public constructor() {
		this.group = "Admin";
	}
	public canRead(): boolean {
		return true;
	}
	public canCreate(): boolean {
		return true;
	}
	public canUpdate(): boolean {
		return true;
	}
	public canDelete(): boolean {
		return true;
	}
}
