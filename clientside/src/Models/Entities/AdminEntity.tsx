import * as React from "react";
import _ from "lodash";
import moment from "moment";
import { action, observable, runInAction } from "mobx";
import {
	IAttributeGroup,
	Model,
	IModelAttributes,
	attribute,
	entity,
	jsonReplacerFn,
} from "Models/Model";
import * as Validators from "Validators";
import * as Models from "../Entities";
import { CRUD } from "../CRUDOptions";
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from "Models/Security/IAcl";
import {
	makeFetchManyToManyFunc,
	makeFetchOneToManyFunc,
	makeJoinEqualsFunc,
	makeEnumFetchFunction,
} from "Util/EntityUtils";
import { AdminAdminEntity } from "Models/Security/Acl/AdminAdminEntity";
import * as Enums from "../Enums";
import { IOrderByCondition } from "Views/Components/ModelCollection/ModelQuery";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { SERVER_URL } from "Constants";

export interface IAdminEntityAttributes extends IModelAttributes {
	email: string;
}

@entity("AdminEntity", "Admin")
export default class AdminEntity extends Model implements IAdminEntityAttributes {
	public static acls: IAcl[] = [new AdminAdminEntity()];

	/**
	 * Fields to exclude from the JSON serialization in create operations.
	 */
	public static excludeFromCreate: string[] = [];

	/**
	 * Fields to exclude from the JSON serialization in update operations.
	 */
	public static excludeFromUpdate: string[] = ["email"];

	@Validators.Email()
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		name: "Email",
		displayType: "displayfield",
		order: 10,
		createFieldType: "textfield",
		headerColumn: true,
		searchable: true,
		searchFunction: "like",
		searchTransform: AttrUtils.standardiseString,
	})
	public email: string;

	@Validators.Length(6)
	@observable
	@CRUD({
		name: "Password",
		displayType: "hidden",
		order: 20,
		createFieldType: "password",
	})
	public password: string;

	@Validators.Custom("Password Match", (e: string, target: AdminEntity) => {
		return new Promise((resolve) =>
			resolve(target.password !== e ? "Password fields do not match" : null)
		);
	})
	@observable
	@CRUD({
		name: "Confirm Password",
		displayType: "hidden",
		order: 30,
		createFieldType: "password",
	})
	public _confirmPassword: string;

	constructor(attributes?: Partial<IAdminEntityAttributes>) {
		super(attributes);
	}

	/**
	 * Assigns fields from a passed in JSON object to the fields in this model.
	 * Any reference objects that are passed in are converted to models if they are not already.
	 * This function is called from the constructor to assign the initial fields.
	 */
	@action
	public assignAttributes(attributes?: Partial<IAdminEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.email) {
				this.email = attributes.email;
			}
		}
	}

	/**
	 * Additional fields that are added to GraphQL queries when using the
	 * the managed model APIs.
	 */

	public defaultExpands = `
	`;

	/**
	 * The save method that is called from the admin CRUD components.
	 */

	public async saveFromCrud(formMode: EntityFormMode) {
		const relationPath = {};

		if (formMode === "create") {
			relationPath["password"] = {};

			if (this.password !== this._confirmPassword) {
				throw "Password fields do not match";
			}
		}
		return this.save(relationPath, {
			graphQlInputType:
				formMode === "create"
					? `[${this.getModelName()}CreateInput]`
					: `[${this.getModelName()}Input]`,
			options: [
				{
					key: "mergeReferences",
					graphQlType: "[String]",
					value: [],
				},
			],
		});
	}

	/**
	 * Returns the string representation of this entity to display on the UI.
	 */
	public getDisplayName() {
		return this.email;
	}
}
