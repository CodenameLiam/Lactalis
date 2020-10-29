import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { TextField } from "../../TextBox/TextBox";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributeTextFieldProps<T extends Model> extends IAttributeProps<T> {
	onChangeAndBlur?: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

class AttributeTextField<T extends Model> extends React.Component<IAttributeTextFieldProps<T>> {
	public render() {
		const { model, options, errors, className, isReadonly, isRequired } = this.props;
		return (
			<TextField
				model={model}
				modelProperty={options.attributeName}
				label={options.displayName}
				errors={errors}
				className={className}
				isReadOnly={isReadonly}
				isRequired={isRequired}
				onAfterChange={this.props.onAfterChange}
				onChangeAndBlur={this.props.onChangeAndBlur}
			/>
		);
	}
}

export default AttributeTextField;
