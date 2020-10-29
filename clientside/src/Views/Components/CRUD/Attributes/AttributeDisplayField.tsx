import * as React from "react";
import { Model } from "Models/Model";
import { observer } from "mobx-react";
import { IAttributeProps } from "./IAttributeProps";
import classNames from "classnames";
import { TextField } from "Views/Components/TextBox/TextBox";

interface IAttributeTextFieldProps<T extends Model> extends IAttributeProps<T> {
	displayFunction?: (model: T) => React.ReactNode;
}

@observer
export default class AttributeDisplayField<T extends Model> extends React.Component<
	IAttributeTextFieldProps<T>
> {
	public render() {
		const { model, options, className, displayFunction } = this.props;
		const modelProperty = model[options.attributeName];
		const value = displayFunction
			? displayFunction(modelProperty)
			: modelProperty === null || modelProperty === undefined
			? undefined
			: modelProperty.toString();
		return (
			<TextField
				model={model}
				modelProperty={options.attributeName}
				label={options.displayName}
				className={classNames(className, "input-group__displayfield input-group-block")}
				isReadOnly={true}
				inputProps={{
					value: value,
					onChange: undefined,
				}}
			/>
		);
	}
}
