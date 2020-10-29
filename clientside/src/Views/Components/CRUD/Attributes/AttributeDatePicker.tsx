import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { DatePicker } from "../../DatePicker/DatePicker";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributeDatePickerProps<T extends Model> extends IAttributeProps<T> {}

class AttributeDatePicker<T extends Model> extends React.Component<IAttributeDatePickerProps<T>> {
	public render() {
		const { model, options, className, isReadonly, errors, isRequired } = this.props;
		return (
			<DatePicker
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

export default AttributeDatePicker;
