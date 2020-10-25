import React from "react";
import Header from "./Header";
import Navigation from "./Navigation";

interface IPage {
	children?: any;
	title: string;
}

export default function Page(props: IPage) {
	return (
		<div className="page">
			<Header title={props.title} />
			<div className="content">
				<Navigation />
				<div className="body">{props.children}</div>
			</div>
		</div>
	);
}
