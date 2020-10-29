import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { EntityFormMode } from "Views/Components/Helpers/Common";

export interface IAttributeProps<T extends Model> {
	/** The model to bind the component to */
	model: T;
	/** The crud options for this attribute */
	options: AttributeCRUDOptions;
	/** The class name for this field */
	className?: string;
	/** If this field is readonly */
	isReadonly?: boolean;
	/** If this field is required */
	isRequired?: boolean;
	/** A list of validation errors for this field */
	errors?: string[];
	/** The call back function to trigger after the model got changed */
	onAfterChange?: (event: any) => void;
	/** The mode the CRUD form is in */
	formMode: EntityFormMode;
}
