import * as React from "react";
import { observer } from "mobx-react";
import { SERVER_URL } from "../../../Constants";
import { RouteComponentProps } from "react-router";
import queryString from "querystring";

/**
 * Logs the user out of the application
 *
 * Can take a location query param that will redirect after the logout occurs
 */
@observer
export default class Logout extends React.Component<RouteComponentProps> {
	public componentDidMount() {
		const location =
			queryString.parse(this.props.location.search.substring(1))["redirect"] || undefined;
		let qry = `?redirect=${location ? `${location}` : "/"}`;
		window.location.href = `${SERVER_URL}/api/authorization/logout${qry}`;
	}

	public render() {
		return null;
	}
}
