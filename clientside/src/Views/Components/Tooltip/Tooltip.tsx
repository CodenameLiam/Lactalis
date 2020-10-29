import * as React from "react";

export interface ITooltipProps<T> {
	id: string;
	content: React.ReactNode;
}

export class Tooltip<T> extends React.Component<ITooltipProps<T>, any> {
	public render() {
		const { id, content } = this.props;
		return (
			<div className="tooltip icon-information icon-right" id={id}>
				<span className="tooltip__content">{content}</span>
			</div>
		);
	}
}
