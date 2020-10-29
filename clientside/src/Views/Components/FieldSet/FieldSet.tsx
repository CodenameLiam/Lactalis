import * as React from "react";
import { observer } from "mobx-react";
import _ from "lodash";

interface IFieldSet {
	/** The unique id for FieldSet component which will be used as key */
	id: string;
	/** The displayed content of the fildset legend */
	name: string;
	/** The extra class for fieldSet */
	className?: string;
	/** Whether to display the name of the group */
	showName?: boolean;
}

@observer
export class FieldSet extends React.Component<IFieldSet> {
	render() {
		return (
			<fieldset key={this.props.id} className={this.props.className}>
				{this.props.name.length > 0 ? <legend>{this.props.name}</legend> : <></>}
				<div>{this.props.children}</div>
			</fieldset>
		);
	}
}
