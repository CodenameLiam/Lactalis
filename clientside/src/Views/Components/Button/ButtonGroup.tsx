import * as React from "react";
import { observer } from "mobx-react";
import classNames from "classnames";

export enum Alignment {
	VERTICAL = "btn-group--vertical",
	HORIZONTAL = "btn-group--horizontal",
}

export enum Sizing {
	GROW = "btn-group--grow-elements",
	STATIC = "btn-group--static-elements",
}

export interface IButtonGroupProps {
	className?: string;
	alignment?: Alignment;
	sizing?: Sizing;
	innerProps?: React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement>;
}

@observer
export class ButtonGroup extends React.Component<IButtonGroupProps, any> {
	public render() {
		const innerProps = this.props.innerProps || {};
		const className = `btn-group ${innerProps.className ? innerProps.className : ""}`;
		let classes = className;

		const { alignment, sizing } = this.props;
		classes = classNames(classes, alignment ? alignment : "");
		classes = classNames(classes, sizing ? sizing : "");
		classes = classNames(classes, this.props.className);

		return (
			<section {...this.props.innerProps} className={classes}>
				{this.props.children}
			</section>
		);
	}
}
