import * as React from "react";
import { observer } from "mobx-react";
import { Redirect } from "react-router";
import { store } from "Models/Store";

export interface ISecuredPageProps {
	/**
	 * The groups that can access this page.
	 * If this is undefined then the page will be available to all
	 */
	groups?: string[];
}

/**
 * A secured page is a component to manage the page access with respect to security groups
 */
@observer
export default class SecuredPage extends React.Component<ISecuredPageProps, any> {
	public render() {
		if (this.props.groups) {
			const { groups } = this.props;
			if (!groups || !groups.length) {
				return <Redirect to="/404" />;
			}
			if (groups.some((r) => store.userGroups.map((ug) => ug.name).includes(r))) {
				return this.props.children;
			}
			return <Redirect to="/404" />;
		}

		return this.props.children;
	}
}
