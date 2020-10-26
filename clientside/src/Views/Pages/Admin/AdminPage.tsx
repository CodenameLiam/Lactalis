/*
 * @bot-written
 *
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 *
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from "react";
import { observer } from "mobx-react";
import { Redirect, RouteComponentProps } from "react-router";
import { PageWrapper } from "Views/Components/PageWrapper/PageWrapper";
import { store } from "../../../Models/Store";
import { Button, withStyles } from "@material-ui/core";
import { ChromeReaderMode, Person, PersonAdd } from "@material-ui/icons";

// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

@observer
export default class AdminPage extends React.Component<RouteComponentProps> {
	public render() {
		// % protected region % [Override contents here] on begin
		return (
			<PageWrapper {...this.props}>
				<div className="admin-dashboard">
					{/* <h2>Administration</h2> */}
					<p>Welcome to the Administration Dashboard!</p>
					<p>Use this section to manage all data within the Lactalis Application.</p>
					<div className="quick-links">
						<QuickLinkButton startIcon={<PersonAdd />}>Create a new farmer</QuickLinkButton>
						<QuickLinkButton startIcon={<ChromeReaderMode />}>Add a news article</QuickLinkButton>
					</div>
				</div>
			</PageWrapper>
		);
		// % protected region % [Override contents here] end
	}
}

const QuickLinkButton = withStyles({
	root: {
		background: "#1c1e26",
		borderRadius: "1rem",
		height: "10rem",
		width: "15rem",
		margin: "1rem",
		color: "white",
		fontSize: "1rem",
		fontFamily: "'Poppins', sans-serif",
		boxShadow: "2px 4px 4px -2px #dddddd",
		transition: "all 0.3s",
		"&:hover": {
			background: "#1c1e26",
			boxShadow: "2px 5px 8px -1px #dddddd",
			transform: "scale(1.05)",
		},
	},
})(Button);
