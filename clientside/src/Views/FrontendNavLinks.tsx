import { ILink } from "Views/Components/Navigation/Navigation";
import { RouteComponentProps } from "react-router";
import { store } from "Models/Store";

export function getFrontendNavLinks(pageProps: RouteComponentProps): ILink[][] {
	return [
		[{ label: "Home", path: "/", icon: "home", iconPos: "icon-left" }],
		[],
		[
			{
				label: "Login",
				path: "/login",
				icon: "login",
				iconPos: "icon-left",
				shouldDisplay: () => !store.loggedIn,
			},
			{
				label: "Logout",
				path: "/logout",
				icon: "logout",
				iconPos: "icon-left",
				shouldDisplay: () => store.loggedIn,
			},
		],
	];
}
