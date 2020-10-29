import * as React from "react";
import { Model } from "Models/Model";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { Password } from "../../Password/Password";
import { IAttributeProps } from "./IAttributeProps";

interface IAttributePasswordProps<T extends Model> extends IAttributeProps<T> {}

class AttributePassword<T extends Model> extends React.Component<IAttributePasswordProps<T>> {
	public render() {
		const { model, options, errors, className, isReadonly, isRequired } = this.props;
		return (
			<Password
				model={model}
				modelProperty={options.attributeName}
				label={options.displayName}
				errors={errors}
				className={className}
				isReadOnly={isReadonly}
				isRequired={isRequired}
				onChangeAndBlur={this.props.onAfterChange}
				inputProps={{
					autoComplete: "new-password",
				}}
			/>
		);
	}
}

export default AttributePassword;
