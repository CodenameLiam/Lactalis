import React from "react";

interface IDocumenntPage {
	children: any;
}

export default function DocumentPage(props: IDocumenntPage) {
	return (
		<div className="document-page">
			<div className="document-content">{props.children}</div>
		</div>
	);
}
