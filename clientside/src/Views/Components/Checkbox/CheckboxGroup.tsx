import * as React from "react";
import { observer } from "mobx-react";
import { DisplayType } from "../Models/Enums";
import classNames from "classnames";
import * as uuid from "uuid";
import { Tooltip } from "../Tooltip/Tooltip";

export enum CheckboxAlignment {
	VERTICAL = "checkbox-group--vertical",
	HORIZONTAL = "checkbox-group--horizontal",
}

export interface ICheckboxGroupProps {
	id?: string;
	className?: string;
	alignment?: CheckboxAlignment;
	displayType?: DisplayType;
	label?: string;
	tooltip?: string;
	innerProps?: React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement>;
}

@observer
export class CheckboxGroup extends React.Component<ICheckboxGroupProps, any> {
	private uuid = uuid.v4();

	public render() {
		const id = this.props.id || this.uuid.toString();
		const innerProps = this.props.innerProps || {};
		const { displayType, label, tooltip } = this.props;
		const tooltipId = `${id}-tooltip`;
		const labelNode = label ? (
			<p
				className="input-group__checkbox-header"
				aria-describedby={tooltip ? tooltipId : undefined}>
				{label}
			</p>
		) : null;
		const tooltipNode =
			label && tooltip ? <Tooltip id={tooltipId} content={tooltip}></Tooltip> : "";
		const className = `input-group-wrapper__checkbox ${
			this.props.className ? innerProps.className : ""
		}`;
		let classes = classNames(
			className,
			`input-group-${displayType ? displayType : DisplayType.BLOCK}`
		);
		return (
			<div id={id} className={classes} {...this.props.innerProps} aria-live="assertive">
				{labelNode}
				{tooltipNode}
				{this.props.children}
			</div>
		);
	}
}
