import { Drawer, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import {
	ChromeReaderMode,
	ExitToApp,
	Home,
	HomeWork,
	LocalDrink,
	Menu,
	People,
} from "@material-ui/icons";
import React, { useContext, useEffect } from "react";
// import { useDispatch, useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router";
// import { logout } from "../../../Store/Actions/loginActions";
// import { openNav, showNav } from "./../../../Store/Actions/navigationAction";
import { LinkInterface } from "./Navigation";
import clsx from "clsx";
import If from "./../If/If";
import Logo from "./../../../Media/LactalisAustraliaLogo.png";
import { FloatingNavigation } from "../../../Styles/MaterialStyles";
import { AppContext } from "NavigationProvider";
import { store } from "Models/Store";

function NavigationLinks(): LinkInterface[] {
	return [
		{ label: "Home", path: "/admin", icon: <Home /> },
		{ label: "Users", path: "/admin/User", icon: <People /> },
		{ label: "News Articles", path: "/admin/NewsArticleEntity", icon: <ChromeReaderMode /> },
		{ label: "Farms", path: "/admin/FarmEntity", icon: <HomeWork /> },
		{ label: "Milk Tests", path: "/admin/MilkTestEntity", icon: <LocalDrink /> },
	];
}

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		root: {
			display: "flex",
		},
		appBar: {
			zIndex: theme.zIndex.drawer + 1,
		},
		drawer: {
			width: "15rem",
			flexShrink: 0,
			whiteSpace: "nowrap",
			overflow: "hidden",
		},
		drawerOpen: {
			width: "15rem",
			boxShadow:
				"0px 4px 50px -2px rgba(200, 230, 255, 0.3), -1px 2px 10px 0px rgba(200,200,200, 0.14), 0px 1px 5px 0px rgba(200,200,200,0.12) !important",
			borderRight: "none",
			transition: theme.transitions.create("width", {
				easing: theme.transitions.easing.sharp,
				duration: theme.transitions.duration.enteringScreen,
			}),
			overflow: "hidden",
			background: "#1c1e26",
			"& ul": {
				paddingLeft: "0rem",
			},
			position: "relative",
			height: "calc(100vh - 4rem)",
		},
		drawerClose: {
			transition: theme.transitions.create("width", {
				easing: theme.transitions.easing.sharp,
				duration: theme.transitions.duration.leavingScreen,
			}),
			boxShadow:
				"0px 4px 50px -2px rgba(200, 230, 255, 0.3), -1px 2px 10px 0px rgba(200,200,200, 0.14), 0px 1px 5px 0px rgba(200,200,200,0.12) !important",
			borderRight: "none",
			overflowX: "hidden",
			width: "70px",
			[theme.breakpoints.up("sm")]: {
				width: theme.spacing(9) + 1,
			},
			overflow: "hidden",
			background: "#1c1e26",
			"& ul": {
				paddingLeft: "0rem",
			},
			position: "relative",
			height: "calc(100vh - 4rem)",
		},
		drawerNotVisible: {
			width: "0rem",
		},
		list: { color: "#ffffff", margin: "0.5rem", height: "100%", overflow: "hidden" },
		listBottom: { position: "absolute", bottom: "10px", width: "100%" },
		menuButtom: {
			borderRadius: "1rem",
			marginBottom: "0.5rem",
			"& span": { fontFamily: "'Poppins', sans-serif" },
			"&:hover": { backgroundColor: "rgb(152, 152, 152)" },
		},
		menuButtonActive: {
			backgroundColor: "#ffffff !important",
			color: "#1c1e26",
			// "&:hover": { background: "#fffff" },
		},
		menuButtonInactive: {
			transition: "background-color 0.3s",
			"&:hover": { backgroundColor: "rgb(152, 152, 152)" },
		},
		menuButtonExpand: { height: "48px" },
		menuIconActive: { color: "#1c1e26" },
		menuIconInactive: { color: "#ffffff" },
	})
);

// The navigation drawer
export default function AdminNavigation() {
	const history: any = useHistory();
	const location: any = useLocation();
	const { appState, setAppState } = useContext(AppContext);

	const navigationLinks = NavigationLinks();
	const classes = useStyles();

	// State to track visibility and size
	// const navigation = useSelector((state: any) => state.navigation);
	// const dispatch = useDispatch();

	// Close the drawer if open, open the drawer if closed
	const handleDrawer = () => {
		setAppState({ navOpen: !appState.navOpen, navVisible: checkNavVisible() });
	};

	// Checks if the navigation link matches the current route (will be rendered as active)
	function checkActive(locationPath: string, linkPath: string) {
		return locationPath.split("/")[2] === linkPath.split("/")[2];
	}

	// Event listeners to re-size/hide navbar on window change
	useEffect(() => {
		window.addEventListener("resize", resize);
		function resize() {
			setAppState({
				navOpen: appState.navOpen ? checkNavOpen() : false,
				navVisible: checkNavVisible(),
			});
		}
		return () => {
			window.removeEventListener("resize", resize);
		};
	}, [appState.navOpen, appState.navVisible]);

	const handleLogOut = () => {
		store.routerHistory.push("/logout");
	};

	return (
		<div className="navigation" style={{ position: checkNavVisible() ? undefined : "absolute" }}>
			<Drawer
				variant="permanent"
				className={clsx(classes.drawer, {
					[classes.drawerOpen]: appState.navOpen,
					[classes.drawerClose]: !appState.navOpen,
					[classes.drawerNotVisible]: !appState.navVisible,
				})}
				classes={{
					paper: clsx({
						[classes.drawerOpen]: appState.navOpen,
						[classes.drawerClose]: !appState.navOpen,
						[classes.drawerNotVisible]: !appState.navVisible,
					}),
				}}>
				<div className="image-container">
					<If condition={appState.navOpen}>
						<img src={Logo} />
					</If>
				</div>

				<List classes={{ root: classes.list }}>
					{navigationLinks.map((link, index) => {
						const active = checkActive(location.pathname, link.path);
						return (
							<ListItem
								key={index}
								onClick={() => {
									setAppState({ ...appState, navVisible: checkNavVisible() });
									history.push(link.path);
								}}
								button
								className={clsx(classes.menuButtom, {
									[classes.menuButtonActive]: active,
									[classes.menuButtonInactive]: !active,
								})}
								classes={{
									root: clsx({
										[classes.menuButtonActive]: active,
										[classes.menuButtonInactive]: !active,
									}),
								}}>
								<ListItemIcon
									classes={{
										root: clsx({
											[classes.menuIconActive]: active,
											[classes.menuIconInactive]: !active,
										}),
									}}>
									{link.icon}
								</ListItemIcon>
								<ListItemText primary={link.label} />
							</ListItem>
						);
					})}
					<List classes={{ root: classes.listBottom }}>
						<ListItem
							button
							onClick={handleLogOut}
							classes={{ root: clsx(classes.menuButtom, classes.menuButtonExpand) }}>
							<ListItemIcon>
								<ExitToApp classes={{ root: classes.menuIconInactive }} />
							</ListItemIcon>
							<ListItemText primary="Logout" />
						</ListItem>
						<ListItem
							button
							onClick={handleDrawer}
							classes={{ root: clsx(classes.menuButtom, classes.menuButtonExpand) }}>
							<ListItemIcon>
								<Menu classes={{ root: classes.menuIconInactive }} />
							</ListItemIcon>
						</ListItem>
					</List>
				</List>
			</Drawer>

			<If condition={!appState.navVisible}>
				<FloatingNavigation
					onClick={() => {
						setAppState({
							navOpen: false,
							navVisible: false,
						});
					}}>
					<Menu />
				</FloatingNavigation>
			</If>
		</div>
	);
}

// Check if the navbar should be closed
function checkNavOpen() {
	return window.innerWidth <= 1024 ? false : true;
}

// Check if the navbar should be visible
function checkNavVisible() {
	return window.innerWidth <= 800 ? false : true;
}
