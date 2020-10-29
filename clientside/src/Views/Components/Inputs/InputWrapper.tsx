import * as React from "react";
import { observer } from "mobx-react";
import classNames from "classnames";
import { action, observable } from "mobx";
import { Tooltip } from "../Tooltip/Tooltip";
import If from "../If/If";
import { DisplayType } from "../Models/Enums";

export enum InputType {
	INPUT = "input",
	TEXTAREA = "textarea",
	CHECKBOX = "checkbox",
	RADIO = "radio",
	DATE = "date",
	TIME = "time",
	PASSWORD = "password",
}

interface IInputWrapperProps {
	isInputGroup?: boolean;
	wrapperId?: string;
	id?: string;
	inputType?: InputType;
	inputName?: string;
	inputId?: string;
	label?: ILabelOptions | string;
	labelVisible?: boolean;
	className?: string;
	displayType?: DisplayType;
	isRequired?: boolean;
	staticInput?: boolean;
	tooltip?: string;
	subDescription?: string;
	errors?: string | string[];
	innerProps?: React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement>;
}

export enum LabelPositions {
	BEFORE,
	AFTER,
}

export interface ILabelOptions {
	text: string;
	position?: LabelPositions;
}

@observer
class InputWrapper extends React.Component<IInputWrapperProps, any> {
	@observable
	private focused: boolean;

	@observable
	private isHovered: boolean;

	public render() {
		const {
			isInputGroup,
			wrapperId,
			inputType,
			id,
			label,
			children,
			className,
			displayType,
			inputId,
			isRequired,
			staticInput,
			tooltip,
			subDescription,
			errors,
		} = this.props;
		const tooltipId = `${id}-tooltip`;
		const subDescriptionId = `${id}-sub-description`;
		const labelPostion = typeof label === "object" ? label.position : LabelPositions.BEFORE;
		const requiredMark = label && isRequired ? <span className="required">*</span> : undefined;
		const labelNode = label ? (
			<label htmlFor={inputId}>
				{typeof label === "object" ? label.text : label}
				{requiredMark}
			</label>
		) : (
			""
		);
		const labelVisible = this.props.labelVisible === undefined ? true : this.props.labelVisible;

		let classes = isRequired ? classNames("input-group--is-required", className) : className;
		classes = this.setTooltipClass(classes, tooltip);
		classes = classNames(classes, !isInputGroup ? "input-group" : null);
		classes = classNames(classes, inputType ? `input-group__${inputType}` : null);
		classes = classNames(classes, `input-group-${displayType ? displayType : DisplayType.BLOCK}`);
		classes = classNames(
			classes,
			errors && (typeof errors === "string" || errors.some((e) => !!e))
				? "input-group--error"
				: null
		);
		classes = classNames(classes, staticInput ? "input-group--static" : null);

		const Errors = (props: { errors?: string | string[] }) => {
			if (!props.errors) {
				return null;
			} else if (typeof props.errors === "string") {
				return (
					<div className="input-group__error-text">
						<p>{errors}</p>
					</div>
				);
			}
			return (
				<div className="input-group__error-text">
					{props.errors.map((error, idx) => {
						return <p key={idx}>{error}</p>;
					})}
				</div>
			);
		};

		const tooltipNode = tooltip ? <Tooltip id={tooltipId} content={tooltip} /> : "";
		const subDescriptionNode =
			!tooltip && subDescription ? <p id={subDescriptionId}>{subDescription}</p> : "";

		let labelText: string | undefined;
		if (typeof label === "string") {
			labelText = label;
		} else if (label === undefined) {
			labelText = undefined;
		} else {
			labelText = label.text;
		}

		return (
			<div
				id={wrapperId}
				className={classes}
				onFocus={this.onFocus}
				onBlur={this.onBlue}
				onMouseEnter={this.handleHover}
				onMouseLeave={this.handleHover}
				aria-label={labelText}
				{...this.props.innerProps}>
				<If condition={labelVisible && labelPostion === LabelPositions.BEFORE}>{labelNode}</If>
				{children}
				<If condition={labelVisible && labelPostion === LabelPositions.AFTER}>{labelNode}</If>
				{tooltipNode}
				{subDescriptionNode}
				<Errors errors={errors} />
			</div>
		);
	}

	@action
	private onFocus = (event: React.ChangeEvent<HTMLDivElement>): void => {
		this.focused = true;
	};

	@action
	private onBlue = (event: React.ChangeEvent<HTMLDivElement>): void => {
		this.focused = false;
	};

	@action
	private handleHover = (): void => {
		this.isHovered = !this.isHovered;
	};

	private setFocusClass = (classes?: string) => {
		if (this.focused) {
			return classNames("input-group--focus", classes);
		}
		return classes;
	};

	private setHoverClass = (classes?: string) => {
		if (this.isHovered) {
			return classNames("input-group--hover", classes);
		}
		return classes;
	};

	private setTooltipClass = (classes?: string, tooltip?: React.ReactNode) => {
		if (tooltip) {
			return classNames("input-group--tooltip", classes);
		}
		return classes;
	};
}
export default InputWrapper;
