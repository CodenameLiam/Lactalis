import * as React from "react";
import { observer } from "mobx-react";
import { store } from "Models/Store";
import { Link } from "react-router-dom";

export interface ISecuredAdminPageProps {
	canDo: boolean;
}

@observer
export default class SecuredAdminPage extends React.Component<ISecuredAdminPageProps, any> {
	public render() {
		const { canDo } = this.props;
		if (!canDo) {
			return (
				<section>
					Access denied. Click <Link to="/admin">Here</Link> to return to the admin main page
				</section>
			);
		} else {
			return this.props.children;
		}
	}
}
