import * as React from "react";
import { observer } from "mobx-react";

interface IIfProps {
	condition: boolean | undefined;
}

@observer
export default class If extends React.Component<IIfProps, any> {
	public render() {
		if (this.props.condition) {
			return this.props.children;
		} else {
			return null;
		}
	}
}
