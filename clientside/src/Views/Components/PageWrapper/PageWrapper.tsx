import * as React from "react";
import { RouteComponentProps } from "react-router";

export class PageWrapper extends React.Component<RouteComponentProps> {
	public render() {
		return <>{this.props.children}</>;
	}
}
