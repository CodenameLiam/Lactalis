import * as React from "react";
import { observer } from "mobx-react";
import { Link } from "react-router-dom";
import { Checkbox } from "../Checkbox/Checkbox";
import { store } from "../../../Models/Store";
import If from "../If/If";

export type uiLocation = "frontend" | "admin";
export interface ITopBarProps {
	/** The current location of the application */
	currentLocation: uiLocation;
}

/**
 * The Topbar component displays the topbar for admins to toggle between the frontend and the backend
 */
@observer
export default class Topbar extends React.Component<ITopBarProps> {
	private buttonLink = ({ location }: { location: uiLocation }) => {
		if (location === "admin") {
			return (
				<Link to="/" className="icon-right icon-arrow-right-up link-rm-txt-dec active">
					Frontend
				</Link>
			);
		}
		return (
			<Link to="/admin" className="icon-right icon-arrow-right-up link-rm-txt-dec active">
				Admin
			</Link>
		);
	};

	public render() {
		return (
			<If condition={store.hasBackendAccess}>
				<div className="admin__top-bar">
					<ul>
						<If condition={false}>
							<li>
								<Checkbox
									model={store}
									modelProperty="frontendEditMode"
									label="Edit Mode"
									labelVisible={false}
									className="input-group__toggle-switch"
								/>
							</li>
						</If>
						<li>
							<this.buttonLink location={this.props.currentLocation} />
						</li>
					</ul>
				</div>
			</If>
		);
	}
}
