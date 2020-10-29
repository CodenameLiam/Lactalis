import * as React from "react";
import { action } from "mobx";
import { store } from "Models/Store";
import { Route, RouteComponentProps, Switch, Redirect } from "react-router";
import * as Pages from "./Pages";
import Logout from "./Components/Logout/Logout";
import Auth from "./Components/Auth/Auth";
import PageNotFound from "./Pages/PageNotFound";
import Topbar from "./Components/Topbar/Topbar";
import { Home } from "./Pages";
import News from "./Pages/News";
import TradingPost from "./Pages/TradingPost";
import Sustainability from "./Pages/Sustainability";
import Quality from "./Pages/Quality";
import Technical from "./Pages/Technical";
import AgriSupplies from "./Pages/AgriSupplies";
import NewsStory from "./Pages/NewsStory";

export default class Frontend extends React.Component<RouteComponentProps> {
	@action
	private setAppLocation = () => {
		store.appLocation = "frontend";
	};

	public componentDidMount() {
		this.setAppLocation();
	}

	public render() {
		const path = this.props.match.path === "/" ? "" : this.props.match.path;
		return (
			<>
				<div className="body-container">
					{}
					{/* <Topbar currentLocation="frontend" /> */}
					{}
					<div className="frontend">
						{}
						{}
						<>
							<Switch>
								{/* Public routes */}
								{}
								<Route path="/login" component={Pages.Login} />
								<Route path="/register" component={Pages.RegistrationPage} />
								<Route path="/confirm-email" component={Pages.RegistrationConfirmPage} />
								<Route path="/reset-password-request" component={Pages.ResetPasswordRequestPage} />
								<Route path="/reset-password" component={Pages.ResetPasswordPage} />
								<Route path="/logout" component={Logout} />
								{}

								<Route path={`${path}/404`} component={PageNotFound} />

								{}
								{}

								<Auth {...this.props}>
									<Switch>
										{/* These routes require a login to view */}

										{/* Pages from the ui model */}
										{}
										{}
										{}
										<Route exact path="/" component={Home} />
										<Route path="/news/:id" component={NewsStory} />
										<Route path="/news" component={News} />
										<Route path="/trading-post" component={TradingPost} />
										<Route path="/sustainability" component={Sustainability} />
										<Route path="/quality" component={Quality} />
										<Route path="/technical" component={Technical} />
										<Route path="/agri-supplies" component={AgriSupplies} />
										{}

										<Route component={PageNotFound} />
									</Switch>
								</Auth>
							</Switch>
						</>
						{}
						{}
					</div>
				</div>
			</>
		);
	}
}
