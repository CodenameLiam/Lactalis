import React, { useContext, useEffect, useState } from "react";
import clsx from "clsx";
import { useHistory, useLocation } from "react-router";
import { Drawer, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import {
	Home,
	ChromeReaderMode,
	Menu,
	ExitToApp,
	Eco,
	Equalizer,
	Settings,
	Spa,
	Store,
} from "@material-ui/icons";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import Logo from "./../../../Media/LactalisAustraliaLogo.png";
import If from "../If/If";
import { store } from "Models/Store";
import { FloatingNavigation } from "../MaterialComponents/MaterialStyles";
import { AppContext } from "NavigationProvider";
// import { FloatingNavigation } from "../../Styles/MaterialStyles";

export interface LinkInterface {
	path: string;
	label: React.ReactNode;
	icon: React.ReactNode;
}

// Links for the navigation drawer
function NavigationLinks(): LinkInterface[] {
	return [
		{ label: "Home", path: "/", icon: <Home /> },
		{ label: "News", path: "/news", icon: <ChromeReaderMode /> },
		{ label: "Agri-Supplies", path: "/agri-supplies", icon: <Spa /> },
		{ label: "Sustainability", path: "/sustainability", icon: <Eco /> },
		{ label: "Trading Post", path: "/trading-post", icon: <Store /> },
		{ label: "Quality", path: "/quality", icon: <Equalizer /> },
		{ label: "Technical", path: "/technical", icon: <Settings /> },

		// Pages no longer in use
		// { label: "Our Farmers", path: "/our-farmers", icon: <Group /> },
		// { label: "What's On", path: "/whats-on", icon: <Event /> },
	];
}

// Styles for the navigation drawer
const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		root: {
			display: "flex",
		},
		appBar: {
			zIndex: theme.zIndex.drawer + 1,
		},
		drawer: {
			width: "20rem",
			flexShrink: 0,
			whiteSpace: "nowrap",
			overflow: "hidden",
		},
		drawerOpen: {
			width: "20rem",
			boxShadow:
				"0px 4px 50px -2px rgba(200, 230, 255, 0.3), -1px 2px 10px 0px rgba(200,200,200, 0.14), 0px 1px 5px 0px rgba(200,200,200,0.12) !important",
			borderRight: "none",
			transition: theme.transitions.create("width", {
				easing: theme.transitions.easing.sharp,
				duration: theme.transitions.duration.enteringScreen,
			}),
			overflow: "hidden",
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
			position: "relative",
			height: "calc(100vh - 4rem)",
		},
		drawerNotVisible: {
			width: "0rem",
		},
		list: { margin: "0.5rem", height: "100%", overflow: "hidden" },
		listBottom: { position: "absolute", bottom: "10px", width: "100%" },
		menuButtom: {
			borderRadius: "1rem",
			marginBottom: "0.5rem",
			"& span": { fontFamily: "'Poppins', sans-serif" },
		},
		menuButtonActive: {
			background:
				"linear-gradient(90deg, rgba(113,210,245,1) 0%, rgba(131,228,237,1) 80%, rgba(146,243,230,1) 100%)",
			color: "white",
		},
		menuButtonInactive: {
			transition: "background-color 0.3s",
			"&:hover": { backgroundColor: "rgb(235, 235, 235)" },
		},
		menuButtonExpand: { height: "48px" },
		menuIconActive: { color: "white" },
		menuIconInactive: {},
	})
);

// The navigation drawer
export default function Navigation() {
	const history: any = useHistory();
	const location: any = useLocation();
	const { appState, setAppState } = useContext(AppContext);

	const navigationLinks = NavigationLinks();
	const classes = useStyles();

	// Close the drawer if open, open the drawer if closed
	const handleDrawer = () => {
		setAppState({ navOpen: !appState.navOpen, navVisible: checkNavVisible() });
	};

	// Checks if the navigation link matches the current route (will be rendered as active)
	function checkActive(locationPath: string, linkPath: string) {
		return locationPath.split("/")[1] === linkPath.split("/")[1];
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
								<ExitToApp />
							</ListItemIcon>
							<ListItemText primary="Logout" />
						</ListItem>
						<ListItem
							button
							onClick={handleDrawer}
							classes={{ root: clsx(classes.menuButtom, classes.menuButtonExpand) }}>
							<ListItemIcon>
								<Menu />
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
