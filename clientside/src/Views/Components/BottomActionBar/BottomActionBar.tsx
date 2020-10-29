import * as React from "react";
import { observer } from "mobx-react";
import { ButtonGroup, Alignment } from "../Button/ButtonGroup";

export interface IBottomActionBarProps {
	groups: React.ReactNode[];
}

@observer
export class BottomActionBar<T> extends React.Component<IBottomActionBarProps> {
	public render() {
		return (
			<section aria-label="action-bar" className="action-bar">
				{this.props.groups.map((e, i) => (
					<ButtonGroup alignment={Alignment.HORIZONTAL} key={i}>
						{e}
					</ButtonGroup>
				))}
			</section>
		);
	}
}
