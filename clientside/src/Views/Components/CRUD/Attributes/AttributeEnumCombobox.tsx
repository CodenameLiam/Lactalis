import * as React from "react";
import { Combobox, ComboboxOption } from "../../Combobox/Combobox";
import { computed } from "mobx";
import { observer } from "mobx-react";
import { IAttributeProps } from "./IAttributeProps";
import { Model } from "Models/Model";

export interface IAttributeEnumComboboxProps<T extends Model> extends IAttributeProps<T> {}

@observer
export default class AttributeEnumCombobox<T extends Model> extends React.Component<
	IAttributeEnumComboboxProps<T>
> {
	@computed
	private get options() {
		return this.props.options.enumResolveFunction || [];
	}

	public render() {
		return (
			<Combobox
				model={this.props.model}
				label={this.props.options.name}
				options={this.options}
				modelProperty={this.props.options.attributeName}
				className={this.props.className}
				isDisabled={this.props.isReadonly}
				isRequired={this.props.isRequired}
				errors={this.props.errors}
			/>
		);
	}
}
