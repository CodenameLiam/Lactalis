import * as React from "react";
import { observer } from "mobx-react";
import * as uuid from "uuid";
import { action } from "mobx";
import InputWrapper, { InputType } from "../Inputs/InputWrapper";
import { DisplayType } from "../Models/Enums";
import InputsHelper from "../Helpers/InputsHelper";
import classNames from "classnames";

interface ITextAreaProps<T> {
	model: T;
	modelProperty: string;
	id?: string;
	name?: string;
	className?: string;
	displayType?: DisplayType;
	label?: string;
	labelVisible?: boolean;
	isRequired?: boolean;
	isDisabled?: boolean;
	isReadOnly?: boolean;
	staticInput?: boolean;
	tooltip?: string;
	subDescription?: string;
	textAreaProps?: React.DetailedHTMLProps<
		React.TextareaHTMLAttributes<HTMLTextAreaElement>,
		HTMLTextAreaElement
	>;
	placeholder?: string;
	errors?: string | string[];
	onAfterChange?: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
	onChangeAndBlur?: (event: React.ChangeEvent<HTMLTextAreaElement>) => void;
}

@observer
export class TextArea<T> extends React.Component<ITextAreaProps<T>, any> {
	private uuid = uuid.v4();
	public textAreaInput = React.createRef<HTMLTextAreaElement>();
	private valueWhenFocused: string = "";

	public render() {
		const {
			model,
			modelProperty,
			name,
			className,
			displayType,
			label,
			isRequired,
			isDisabled,
			isReadOnly,
			staticInput,
			tooltip,
			subDescription,
			placeholder,
			errors,
		} = this.props;
		const id = this.props.id || this.uuid.toString();
		const fieldId = `${id}-field`;
		const labelVisible = this.props.labelVisible === undefined ? true : this.props.labelVisible;
		const ariaLabel = !labelVisible ? label : undefined;
		const ariaDescribedby = InputsHelper.getAriaDescribedBy(id, tooltip, subDescription);

		return (
			<InputWrapper
				inputType={InputType.TEXTAREA}
				id={id}
				inputId={fieldId}
				className={className}
				staticInput={staticInput}
				displayType={displayType}
				isRequired={isRequired}
				tooltip={tooltip}
				subDescription={subDescription}
				label={label}
				labelVisible={labelVisible}
				errors={errors}>
				<textarea
					name={name}
					id={fieldId}
					value={this.props.model[this.props.modelProperty] || ""}
					onChange={this.onChange}
					onBlur={this.onBlur}
					onFocus={this.onFocus}
					placeholder={this.props.placeholder}
					disabled={isDisabled}
					readOnly={staticInput || isReadOnly}
					aria-label={ariaLabel}
					aria-describedby={ariaDescribedby}
					ref={this.textAreaInput}
					{...this.props.textAreaProps}
				/>
			</InputWrapper>
		);
	}

	public focus = () => {
		if (this.textAreaInput.current) {
			this.textAreaInput.current.focus();
		}
	};

	@action
	private onChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
		if (this.props.textAreaProps && this.props.textAreaProps.onChange) {
			return this.props.textAreaProps.onChange(event);
		}
		this.props.model[this.props.modelProperty] = event.target.value;

		if (this.props.onAfterChange) {
			this.props.onAfterChange(event);
		}
	};

	@action
	private onBlur = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
		if (this.valueWhenFocused !== event.target.value && this.props.onChangeAndBlur) {
			this.props.onChangeAndBlur(event);
		}
	};

	@action
	private onFocus = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
		this.valueWhenFocused = event.target.value;
	};
}
