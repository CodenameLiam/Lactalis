import { IAcl } from "../IAcl";

export class FarmerMilkTestEntity implements IAcl {
	public group?: string;
	public isVisitorAcl = false;
	public constructor() {
		this.group = "Farmer";
	}
	public canRead(): boolean {
		return true;
	}
	public canCreate(): boolean {
		return false;
	}
	public canUpdate(): boolean {
		return false;
	}
	public canDelete(): boolean {
		return false;
	}
}
