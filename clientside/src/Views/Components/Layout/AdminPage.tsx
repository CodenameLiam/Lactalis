import React from "react";
import AdminHeader from "./AdminHeader";
import AdminNavigation from "./AdminNavigation";

interface IAdminPage {
	children?: any;
	title: any;
}

export function AdminPage(props: IAdminPage) {
	return (
		<div className="page">
			<AdminHeader title={props.title} />
			<div className="content">
				<AdminNavigation />
				<div className="admin-body body">{props.children}</div>
			</div>
		</div>
	);
}
