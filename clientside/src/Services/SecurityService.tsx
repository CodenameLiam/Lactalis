import { IModelAttributes, Model } from "Models/Model";
import { store } from "Models/Store";
import { IAcl } from "Models/Security/IAcl";

export class SecurityService {
	public static canRead<T extends { acls?: IAcl[]; new (...args: any[]): InstanceType<T> }>(
		modelType: T
	): boolean {
		if (
			modelType.acls &&
			modelType.acls.some((acl) => {
				if (acl.isVisitorAcl && acl.canRead()) {
					return true;
				}
				return store.userGroups.some((ug) => {
					if (acl.group === ug.name) {
						return acl.canRead();
					} else {
						return false;
					}
				});
			})
		) {
			return true;
		} else {
			return false;
		}
	}
	public static canCreate<T extends { acls?: IAcl[]; new (...args: any[]): InstanceType<T> }>(
		modelType: T
	): boolean {
		if (
			modelType.acls &&
			modelType.acls.some((acl) => {
				if (acl.isVisitorAcl && acl.canCreate()) {
					return true;
				}
				return store.userGroups.some((ug) => {
					if (acl.group === ug.name) {
						return acl.canCreate();
					} else {
						return false;
					}
				});
			})
		) {
			return true;
		} else {
			return false;
		}
	}
	public static canUpdate<T extends { acls?: IAcl[]; new (...args: any[]): InstanceType<T> }>(
		modelType: T
	): boolean {
		if (
			modelType.acls &&
			modelType.acls.some((acl) => {
				if (acl.isVisitorAcl && acl.canUpdate()) {
					return true;
				}
				return store.userGroups.some((ug) => {
					if (acl.group === ug.name) {
						return acl.canUpdate();
					} else {
						return false;
					}
				});
			})
		) {
			return true;
		} else {
			return false;
		}
	}
	public static canDelete<T extends { acls?: IAcl[]; new (...args: any[]): InstanceType<T> }>(
		modelType: T
	): boolean {
		if (
			modelType.acls &&
			modelType.acls.some((acl) => {
				if (acl.isVisitorAcl && acl.canDelete()) {
					return true;
				}
				return store.userGroups.some((ug) => {
					if (acl.group === ug.name) {
						return acl.canDelete();
					} else {
						return false;
					}
				});
			})
		) {
			return true;
		} else {
			return false;
		}
	}
}
