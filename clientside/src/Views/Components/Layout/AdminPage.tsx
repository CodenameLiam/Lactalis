import React from "react";
import { useLocation } from "react-router";
import AdminHeader from "./AdminHeader";
import AdminNavigation from "./AdminNavigation";

interface IAdminPage {
	children?: any;
	title: any;
}

export function AdminPage(props: IAdminPage) {
	const location = useLocation();
	const bodyStyle = location.pathname == "/admin/graphiql" ? { padding: "0rem" } : {};

	return (
		<div className="page">
			<AdminHeader title={props.title} />
			<div className="content">
				<AdminNavigation />
				<div className="admin-body body" style={bodyStyle}>
					{props.children}
				</div>
			</div>
		</div>
	);
}
