import { AccountCircle, Lock } from "@material-ui/icons";
import { AppContext } from "NavigationProvider";
import React, { useContext } from "react";
// import { useDispatch, useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router";
// import { returnBackend } from "../../../Store/Actions/navigationAction";

interface IAdminHeader {
	title: string;
}

export default function AdminHeader(props: IAdminHeader) {
	const history = useHistory();
	const location = useLocation();

	const { appState, setAppState } = useContext(AppContext);
	const navigationMargin = appState.navOpen ? { marginLeft: "16rem" } : {};

	return (
		<div className="page-header">
			<div className="left" style={navigationMargin}>
				<div className="title">{props.title}</div>
			</div>
			<div className="right">
				<div className="name">Admin</div>
				<div
					className="backend"
					onClick={() => {
						history.push("/");
					}}>
					<Lock />
				</div>
			</div>
		</div>
	);
}
