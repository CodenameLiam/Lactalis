import React from "react";
import Navigation from "../Components/Navigation/Navigation";
import { useHistory } from "react-router";
import Page from "Views/Components/Layout/Page";

export default function Quality() {
	const history = useHistory();

	return <Page title="Quality">Quality</Page>;
}
