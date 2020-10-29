import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { DateTimePicker } from "../../DateTimePicker/DateTimePicker";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributeDateTimePickerProps<T extends Model> extends IAttributeProps<T> {}

class AttributeDateTimePicker<T extends Model> extends React.Component<
	IAttributeDateTimePickerProps<T>
> {
	public render() {
		const { model, options, className, isReadonly, errors, isRequired } = this.props;
		return (
			<DateTimePicker
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

export default AttributeDateTimePicker;
