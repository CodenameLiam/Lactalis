import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import { Link } from "react-router-dom";

@observer
export default class PageNotFound extends React.Component<RouteComponentProps> {
	public render() {
		let contents = null;

		contents = (
			<div className="body-content">
				<div>
					The page could not be found. Click <Link to="/">Here</Link> to return to the home page
				</div>
			</div>
		);

		return contents;
	}
}
