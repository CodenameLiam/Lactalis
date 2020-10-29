import * as React from "react";
import Cookies from "js-cookie";
import { SERVER_URL } from "Constants";
// @ts-ignore
import GraphiQL from "graphiql";

const graphiQLFetcher = (graphQLParams: {}) => {
	const token = Cookies.get("XSRF-TOKEN");
	return fetch(`${SERVER_URL}/api/graphql`, {
		method: "post",
		headers: {
			"Content-Type": "application/json",
			"X-XSRF-TOKEN": token ? token : "",
		},
		body: JSON.stringify(graphQLParams),
	}).then((response) => response.json());
};

export default function GraphiQl() {
	return (
		<div className="graphiql-content-container body-content">
			<GraphiQL fetcher={graphiQLFetcher} />
		</div>
	);
}
