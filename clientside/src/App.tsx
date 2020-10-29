import * as React from "react";
import Cookies from "js-cookie";
import Admin from "./Views/Admin";
import Frontend from "./Views/Frontend";
import { Route, Router, Switch } from "react-router";
import { Provider } from "mobx-react";
import { store } from "./Models/Store";
import { ApolloProvider } from "react-apollo";
import { default as ApolloClient, Operation } from "apollo-boost";
import { SERVER_URL } from "Constants";
import { isServerError } from "./Util/GraphQLUtils";
import { configure, runInAction } from "mobx";
import { createBrowserHistory } from "history";
import { ErrorResponse } from "apollo-link-error";
import { ToastContainer } from "react-toastify";
import GlobalModal from "./Views/Components/Modal/GlobalModal";
import "./Styles/main.scss";
import "reactjs-popup/dist/index.css";
import NavigationProvider from "NavigationProvider";

export default class App extends React.Component {
	constructor(props: any, context: any) {
		super(props, context);
		store.routerHistory = createBrowserHistory();

		store.apolloClient = new ApolloClient({
			uri: `${SERVER_URL}/api/graphql`,
			request: this.onApolloRequest,
			onError: this.onApolloError,
		});

		// All state changes should be run in an action so enforce that
		configure({ enforceActions: "observed" });
	}

	public render() {
		return (
			<ApolloProvider client={store.apolloClient}>
				<Provider store={store}>
					<NavigationProvider>
						<Router history={store.routerHistory}>
							<>
								<ToastContainer className="frontend" />
							</>
							<Switch>
								<Route path="/admin" component={Admin} />
								<Route path="/" component={Frontend} />
							</Switch>
						</Router>
						<GlobalModal ref={(ref) => (ref ? (store.modal = ref) : undefined)} />
					</NavigationProvider>
				</Provider>
			</ApolloProvider>
		);
	}

	/**
	 * Request handler for the apollo client
	 * @param operation
	 */
	private onApolloRequest = async (operation: Operation) => {
		operation.setContext({
			headers: {
				"X-XSRF-TOKEN": Cookies.get("XSRF-TOKEN"),
			},
		});
	};

	/**
	 * Error Handler for the apollo client
	 * @param error
	 */
	private onApolloError = (error: ErrorResponse) => {
		if (isServerError(error.networkError) && error.networkError.statusCode === 401) {
			runInAction(() => {
				store.clearLoggedInUser();
				store.routerHistory.push(`/login?redirect=${store.routerHistory.location.pathname}`);
			});
		}
	};
}
