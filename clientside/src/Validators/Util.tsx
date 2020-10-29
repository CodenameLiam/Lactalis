import { Symbols } from "Symbols";
import { Model } from "Models/Model";

export type IModelValidator = (model: Model) => Promise<IModelAttributeValidationError | null>;

export function initValidators(target: any, key: string) {
	if (!target[Symbols.validator]) {
		target[Symbols.validator] = Array<IModelValidator>();
	}
	if (!target[Symbols.validatorMap]) {
		target[Symbols.validatorMap] = {};
	}
	if (!target[Symbols.validatorMap][key]) {
		target[Symbols.validatorMap][key] = [];
	}
}

export interface IModelAttributeValidationError {
	errorType: ErrorType;
	attributeName: string;
	errorMessage: string;
	target: Model;
}

export enum ErrorType {
	REQUIRED = "required",
	EXISTS = "exists",
	NOT_EXIST = "notExist",
	LENGTH = "length",
	INVALID = "invalid",
	RANGE = "range",
	UNKNOW = "unknow",
}

export enum PropertyType {
	OWN = "own",
	REFERENCE = "reference",
	CHILDREN = "children",
}

export type IFormFieldValidationError = {
	[key in ErrorType]?: string[];
};

export interface IAttributeValidationErrorInfo {
	type: PropertyType;
	target?: Model;
	errors: IFormFieldValidationError | IEntityValidationErrors | Array<IEntityValidationErrors>;
}

export interface IEntityValidationErrors {
	[prop: string]: IAttributeValidationErrorInfo;
}

// todo: not finished
export function getFieldErrorMessages(fieldName: string, fieldErrors: IEntityValidationErrors) {
	if (fieldErrors[fieldName]) {
		return Object.keys(fieldErrors[fieldName]).map((errorType, i) => {
			return fieldErrors[fieldName][errorType];
		});
	} else {
		return undefined;
	}
}

// todo: not finished
export function setFieldValidate(
	fieldName: string,
	fieldErrors: IEntityValidationErrors,
	valid?: boolean
) {
	if (fieldErrors[fieldName]) {
		let fieldError = fieldErrors[fieldName];
		if (fieldError) {
			if (valid !== undefined) {
			}
		}
	}
}
