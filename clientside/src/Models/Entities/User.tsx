import { CRUD } from "../CRUDOptions";
import { IModelAttributes, Model, attribute, entity } from "../Model";
import { createUser, updateUser, deleteUser } from "../../Views/Components/UserCRUD/UserService";
import { action, observable } from "mobx";
import { SERVER_URL } from "Constants";
import * as Validators from "../../Validators";
import axios from "axios";

export interface IGroupAttributes extends IModelAttributes {
	name: string;
}

@entity("group")
export class Group extends Model implements IGroupAttributes {
	@attribute()
	@observable
	public name: string;

	constructor(attributes?: Partial<IGroupAttributes>) {
		super(attributes);
		if (attributes) {
			if (attributes.name) {
				this.name = attributes.name;
			}
		}
	}

	public getDisplayName(): string {
		return this.name;
	}
}

export interface IUserAttributes extends IModelAttributes {
	email: string;
	password: string;
	groups: string[];
}

function getGroups() {
	return axios.get(`${SERVER_URL}/api/account/groups`).then(({ data }) => {
		return data.map((groupName: any) => {
			return { display: groupName, value: groupName };
		});
	});
}

@entity("user")
export default class User extends Model implements IUserAttributes {
	@Validators.Required()
	@Validators.Length(0, 255)
	@Validators.Email()
	@attribute()
	@observable
	@CRUD({ name: "Username", displayType: "displayfield", headerColumn: true, searchable: true })
	public email: string;

	@Validators.Min(6)
	@Validators.Regex(
		new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])"),
		"The password must contain an uppercase letter, a number and a symbol"
	)
	@attribute()
	public password: string;

	@attribute()
	@observable
	@CRUD({
		name: "Groups",
		displayType: "reference-multicombobox",
		referenceTypeFunc: () => Group,
		headerColumn: false,
		searchable: false,
		referenceResolveFunction: getGroups,
	})
	public groups: string[];

	constructor(attributes?: Partial<IUserAttributes>) {
		super(attributes);

		if (attributes) {
			if (attributes.email) {
				this.email = attributes.email;
			}
			if (attributes.password) {
				this.password = attributes.password;
			}
			if (attributes.groups) {
				this.groups = attributes.groups;
			}
		}
	}

	public async save(relationPath: {} = {}) {
		if (this.id === undefined) {
			return createUser(this.toJSON(relationPath)).then(({ data }) => this.updateUser(data));
		}

		return updateUser(this.toJSON(relationPath)).then(({ data }) => this.updateUser(data));
	}

	public async delete() {
		await deleteUser(this.id);
	}

	@action
	private updateUser(data: {}) {
		Object.assign(this, data);
	}

	public toJSON(path: {} = {}): {} {
		return {
			id: this.id,
			email: this.email,
			groups: this.groups,
		};
	}
}
