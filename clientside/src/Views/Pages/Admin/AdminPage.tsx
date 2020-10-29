import * as React from "react";
import { observer } from "mobx-react";
import { Redirect, RouteComponentProps } from "react-router";
import { PageWrapper } from "Views/Components/PageWrapper/PageWrapper";
import { store } from "../../../Models/Store";
import { ChromeReaderMode, PersonAdd } from "@material-ui/icons";
import { QuickLinkButton } from "Views/Components/MaterialComponents/MaterialStyles";

@observer
export default class AdminPage extends React.Component<RouteComponentProps> {
	public render() {
		return (
			<PageWrapper {...this.props}>
				<div className="admin-dashboard">
					{/* <h2>Administration</h2> */}
					<p>Welcome to the Administration Dashboard!</p>
					<p>Use this section to manage all data within the Lactalis Application.</p>
					<div className="quick-links">
						<QuickLinkButton
							onClick={() => store.routerHistory.push("/admin/FarmerEntity")}
							startIcon={<PersonAdd />}>
							Create a new farmer
						</QuickLinkButton>
						<QuickLinkButton
							onClick={() => store.routerHistory.push("/admin/FarmEntity")}
							startIcon={<PersonAdd />}>
							Add a new farm
						</QuickLinkButton>
						<QuickLinkButton
							onClick={() => store.routerHistory.push("/admin/NewsArticleEntity")}
							startIcon={<ChromeReaderMode />}>
							Publish a news article
						</QuickLinkButton>
					</div>
				</div>
			</PageWrapper>
		);
	}
}
