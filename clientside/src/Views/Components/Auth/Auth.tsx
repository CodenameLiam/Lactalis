import * as React from "react";
import { observer } from "mobx-react";
import { IUserResult, store } from "Models/Store";
import axios from "axios";
import { action, observable } from "mobx";
import Spinner from "../Spinner/Spinner";
import { Redirect, RouteComponentProps } from "react-router";
import { SERVER_URL } from "Constants";

export interface AuthProps extends Partial<RouteComponentProps> {
	/** If true then don't redirect on login check failure. */
	checkOnly?: boolean;
	/** The location that the component will redirect to on login check failure. */
	redirectLocation?: string;
	/** Callback on login check success. */
	onAfterSuccess?: () => void;
	/** Callback on login check failure. Note that if checkOnly is not set to true the user will still be redirected. */
	onAfterError?: () => void;
}

/**
 * This component handles the requirement for a user being logged in before they can access the child components.
 *
 * If the user is logged in then the this component will just render the child props.
 *
 * If the user is not flagged as logged in with Javascript then it will send a request to the server to see if they are
 * actually authenticated. If they are then the user is set as logged in, otherwise they are redirected to the login
 * page.
 */
@observer
export default class Auth extends React.Component<AuthProps> {
	@observable
	private requestState: "pending" | "error" | "success" = "pending";

	@action
	private onSuccess = (userResult?: IUserResult) => {
		if (userResult) {
			store.setLoggedInUser(userResult);
		}
		this.requestState = "success";
		this.props.onAfterSuccess?.();
	};

	@action
	private onError = () => {
		store.clearLoggedInUser();
		this.requestState = "error";
		this.props.onAfterError?.();
	};

	public componentDidMount() {
		// If we are already logged in then we don't need to check again
		if (store.loggedIn) {
			this.onSuccess();
			return;
		}

		// Otherwise send a request to the server to see if the token is valid
		axios
			.get(`${SERVER_URL}/api/account/me`)
			.then(({ data }) => this.onSuccess(data))
			.catch(this.onError);
	}

	public render() {
		const { location, children, redirectLocation, checkOnly } = this.props;
		const redirect =
			redirectLocation ?? location?.pathname ?? store.routerHistory.location.pathname;

		switch (this.requestState) {
			case "pending":
				return <Spinner />;
			case "success":
				return this.props.children;
			case "error":
				if (checkOnly) {
					return children;
				}
				return <Redirect to={{ pathname: "/login", search: `?redirect=${redirect}` }} />;
		}
	}
}
