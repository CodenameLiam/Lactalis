import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { IAttributeProps } from "./IAttributeProps";
import { Checkbox } from "../../Checkbox/Checkbox";

interface IAttributeCheckboxProps<T extends Model> extends IAttributeProps<T> {}

class AttributeCheckbox<T extends Model> extends React.Component<IAttributeCheckboxProps<T>> {
	public render() {
		const { model, options, className, isReadonly, isRequired, errors } = this.props;
		return (
			<Checkbox
				model={model}
				modelProperty={options.attributeName}
				label={options.displayName}
				className={className}
				isReadOnly={isReadonly}
				isRequired={isRequired}
				errors={errors}
				onAfterChecked={this.props.onAfterChange}
			/>
		);
	}
}

export default AttributeCheckbox;
