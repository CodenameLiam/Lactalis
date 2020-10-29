import * as React from "react";
import { action } from "mobx";
import * as AdminPages from "./Pages/Admin/Entity";
import Auth from "./Components/Auth/Auth";
import AllUsersPage from "./Pages/Admin/AllUsersPage";
import AdminPage from "./Pages/Admin/AdminPage";
import Topbar from "./Components/Topbar/Topbar";
import PageLinks from "./Pages/Admin/PageLinks";
import Spinner from "Views/Components/Spinner/Spinner";
import { Redirect, Route, RouteComponentProps, Switch } from "react-router";
import { store } from "Models/Store";
import { AdminPage as AdminLayout } from "./Components/Layout/AdminPage";

const GraphiQlLazy = React.lazy(() => import("./Pages/Admin/Graphiql"));

export default class Admin extends React.Component<RouteComponentProps> {
	@action
	private setAppLocation = () => {
		store.appLocation = "admin";
	};

	public componentDidMount() {
		this.setAppLocation();
	}

	public render() {
		return (
			<>
				<div className="body-container">
					{}
					{/* <Topbar currentLocation="admin" /> */}
					{}
					<div className="admin">
						<Auth {...this.props}>
							<this.adminSwitch />
						</Auth>
					</div>
				</div>
			</>
		);
	}

	private adminSwitch = () => {
		if (!store.userGroups.some((ug) => ug.hasBackendAccess)) {
			return <Redirect to="/404" />;
		}

		const path = this.props.match.path === "/" ? "" : this.props.match.path;

		return (
			<>
				{}
				<AdminLayout title="Administration">
					{/* <PageLinks {...this.props} /> */}
					{}
					<div className="body-content">
						<Switch>
							{/* These routes require a login to view */}

							{/* Admin entity pages */}
							<Route exact={true} path={`${path}`} component={AdminPage} />
							<Route path={`${path}/User`} component={AllUsersPage} />
							<Route
								path={`${path}/TradingPostListingEntity`}
								component={AdminPages.TradingPostListingEntityPage}
							/>
							<Route
								path={`${path}/TradingPostCategoryEntity`}
								component={AdminPages.TradingPostCategoryEntityPage}
							/>
							<Route path={`${path}/AdminEntity`} component={AdminPages.AdminEntityPage} />
							<Route path={`${path}/FarmEntity`} component={AdminPages.FarmEntityPage} />
							<Route path={`${path}/MilkTestEntity`} component={AdminPages.MilkTestEntityPage} />
							<Route path={`${path}/FarmerEntity`} component={AdminPages.FarmerEntityPage} />
							<Route
								path={`${path}/ImportantDocumentCategoryEntity`}
								component={AdminPages.ImportantDocumentCategoryEntityPage}
							/>
							<Route
								path={`${path}/TechnicalDocumentCategoryEntity`}
								component={AdminPages.TechnicalDocumentCategoryEntityPage}
							/>
							<Route
								path={`${path}/QualityDocumentCategoryEntity`}
								component={AdminPages.QualityDocumentCategoryEntityPage}
							/>
							<Route
								path={`${path}/QualityDocumentEntity`}
								component={AdminPages.QualityDocumentEntityPage}
							/>
							<Route
								path={`${path}/TechnicalDocumentEntity`}
								component={AdminPages.TechnicalDocumentEntityPage}
							/>
							<Route
								path={`${path}/ImportantDocumentEntity`}
								component={AdminPages.ImportantDocumentEntityPage}
							/>
							<Route
								path={`${path}/NewsArticleEntity`}
								component={AdminPages.NewsArticleEntityPage}
							/>
							<Route
								path={`${path}/AgriSupplyDocumentCategoryEntity`}
								component={AdminPages.AgriSupplyDocumentCategoryEntityPage}
							/>
							<Route
								path={`${path}/SustainabilityPostEntity`}
								component={AdminPages.SustainabilityPostEntityPage}
							/>
							<Route
								path={`${path}/AgriSupplyDocumentEntity`}
								component={AdminPages.AgriSupplyDocumentEntityPage}
							/>
							<Route
								path={`${path}/PromotedArticlesEntity`}
								component={AdminPages.PromotedArticlesEntityPage}
							/>

							{}
							<Route path={`${path}/graphiql`}>
								<React.Suspense fallback={<Spinner />}>
									<GraphiQlLazy />
								</React.Suspense>
							</Route>
							{}
						</Switch>
					</div>
					{}
				</AdminLayout>
				{}

				{}
				<Switch>
					<Route path={`${path}/graphiql`}>
						<React.Suspense fallback={<Spinner />}>
							<GraphiQlLazy />
						</React.Suspense>
					</Route>
				</Switch>
				{}
			</>
		);
	};
}
