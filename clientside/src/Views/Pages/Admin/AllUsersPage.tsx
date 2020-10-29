import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import UserList from "Views/Components/CRUD/UserList";
import { PageWrapper } from "Views/Components/PageWrapper/PageWrapper";
import SecuredPage from "Views/Components/Security/SecuredPage";

@observer
export default class AllUsersPage extends React.Component<RouteComponentProps> {
	public render() {
		return (
			<SecuredPage groups={["Admin", "Farmer"]}>
				<PageWrapper {...this.props}>
					<UserList {...this.props} />
				</PageWrapper>
			</SecuredPage>
		);
	}
}
