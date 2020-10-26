import React, { useContext } from "react";
import { AccountCircle, LockOpen, Security } from "@material-ui/icons";
import If from "../If/If";
import { useHistory, useLocation } from "react-router";
import { store } from "Models/Store";
import { AppContext } from "NavigationProvider";

interface IHeader {
	title: string;
}

export default function Header(props: IHeader) {
	const history = useHistory();
	// const location = useLocation();
	const { appState, setAppState } = useContext(AppContext);
	const navigationMargin = appState.navOpen ? { marginLeft: "16rem" } : {};

	return (
		<div className="page-header">
			<div className="left" style={navigationMargin}>
				<div className="title">{props.title}</div>
			</div>
			<div className="right">
				<div className="name">
					{/* {authentication.user.firstName} {authentication.user.lastName} */}
				</div>
				<div className="account">
					<AccountCircle />
				</div>
				<If condition={store.userGroups[0].hasBackendAccess}>
					<div
						className="backend"
						onClick={() => {
							// dispatch(returnFrontend(location.pathname));
							history.push("/admin");
						}}>
						<LockOpen />
					</div>
				</If>
			</div>
		</div>
	);
}
