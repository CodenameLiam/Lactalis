import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { TimePicker } from "../../TimePicker/TimePicker";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributeTimePickerProps<T extends Model> extends IAttributeProps<T> {}

class AttributeTimePicker<T extends Model> extends React.Component<IAttributeTimePickerProps<T>> {
	public render() {
		const { model, options, className, isReadonly, errors, isRequired } = this.props;
		return (
			<TimePicker
				model={model}
				modelProperty={options.attributeName}
				label={options.displayName}
				className={className}
				isReadOnly={isReadonly}
				isRequired={isRequired}
				errors={errors}
				onAfterChange={this.props.onAfterChange}
			/>
		);
	}
}

export default AttributeTimePicker;
