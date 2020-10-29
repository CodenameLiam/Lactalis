import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { TextArea } from "../../TextArea/TextArea";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributeTextAreaProps<T extends Model> extends IAttributeProps<T> {
	onChangeAndBlur?: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
}

class AttributeTextArea<T extends Model> extends React.Component<IAttributeTextAreaProps<T>> {
	public render() {
		const { model, options, errors, className, isReadonly, isRequired } = this.props;
		return (
			<TextArea
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

export default AttributeTextArea;
