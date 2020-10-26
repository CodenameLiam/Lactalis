import React from "react";
import { LoginButton, LoginTextField } from "Styles/MaterialStyles";
import { IconButton, InputAdornment } from "@material-ui/core";
import { AccountCircle, Lock, Visibility, VisibilityOff } from "@material-ui/icons";
import { Animated } from "react-animated-css";
import { useEffect, useState } from "react";
import { ReactComponent as ReactLogo } from "./../../Media/LoginGraphic.svg";
import Logo from "./../../Media/LactalisAustraliaLogo.png";
import { IUserResult, store } from "Models/Store";
import Axios from "axios";
import { getErrorMessages } from "Util/GraphQLUtils";
import alert from "Util/ToastifyUtils";
import * as queryString from "querystring";
import { useLocation } from "react-router";

export default function Login() {
	return (
		<div className="login">
			<Form />
			<Banner />
		</div>
	);
}

function Banner() {
	const [banner, setBanner] = useState(checkDisplay());

	// Re-sizes/hides the banner depending on the size of the window
	function checkDisplay() {
		return window.innerHeight / window.innerWidth > 0.8 ? (
			<></>
		) : window.innerWidth < 1500 ? (
			<ReactLogo className="banner" style={{ right: `${window.innerWidth - 1500}px` }} />
		) : (
			<ReactLogo className="banner" />
		);
	}

	// Adds an event listener to re-size the banner depending on the window size
	useEffect(() => {
		window.addEventListener("resize", resize);
		function resize() {
			setBanner(checkDisplay());
		}
		return () => {
			window.removeEventListener("resize", resize);
		};
	});

	return banner;
}

// Displays the form for the login page
function Form() {
	// const authentication = useSelector((state: any) => state.authentication);

	// useEffect(() => {
	// 	if (authentication.user) {
	// 		let path = new URLSearchParams(location.search);
	// 		const redirect = path.get("redirect") ? path.get("redirect") : "/";
	// 		history.push(redirect!);
	// 	}
	// 	if (authentication.error) {
	// 		handleError();
	// 	}
	// }, [authentication]);

	// const history = useHistory();
	const location = useLocation();

	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");

	const [usernameError, setUsernameError] = useState(false);
	const [passwordError, setPasswordError] = useState(false);

	const [passwordView, showPassword] = useState(false);

	const [errorStatus, setErrorStatus] = useState(false);
	const [errorMessage, setErrorMessage] = useState("Username or password is incorrect");

	// const dispatch = useDispatch();

	// Toggles between showing and hiding password
	const handleClickShowPassword = () => {
		showPassword(!passwordView);
	};

	// Prevents default behaviour when hiding/showing password
	const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
		event.preventDefault();
	};

	// Validates the form to ensure all fields have been entered
	const validateForm = (event: any) => {
		event.preventDefault();

		username.length > 0 ? setUsernameError(false) : setUsernameError(true);
		password.length > 0 ? setPasswordError(false) : setPasswordError(true);

		if (username.length > 0 && password.length > 0) {
			onLoginClicked(event);
		}
	};

	const handleError = () => {
		setErrorStatus(true);
		// if (authentication.error.includes("500")) setErrorMessage("Unable to connect to the server");
		// if (authentication.error.includes("401")) setErrorMessage("Username or password is incorrect");
	};

	return (
		<div className="login-form">
			<img src={Logo} />

			<form onSubmit={(e) => validateForm(e)}>
				<div className="username-field">
					<LoginTextField
						className="username"
						placeholder="Username"
						variant="outlined"
						fullWidth
						error={usernameError}
						helperText={usernameError ? "Please enter a valid username" : ""}
						InputProps={{
							startAdornment: (
								<InputAdornment position="start">
									<AccountCircle style={{ color: "#71d2f5" }} />
								</InputAdornment>
							),
						}}
						onChange={(e) => {
							setUsername(e.target.value);
							setErrorStatus(false);
							setUsernameError(false);
						}}
					/>
				</div>

				<div className="password-field">
					<LoginTextField
						className="password"
						placeholder="Password"
						variant="outlined"
						fullWidth
						error={passwordError}
						helperText={passwordError ? "Please enter a valid password" : ""}
						type={passwordView ? "text" : "password"}
						value={password}
						InputProps={{
							startAdornment: (
								<InputAdornment position="start">
									<Lock style={{ color: "#71d2f5" }} />
								</InputAdornment>
							),
							endAdornment: (
								<InputAdornment position="end">
									<IconButton
										aria-label="toggle password visibility"
										onClick={handleClickShowPassword}
										onMouseDown={handleMouseDownPassword}>
										{passwordView ? <Visibility /> : <VisibilityOff />}
									</IconButton>
								</InputAdornment>
							),
						}}
						onChange={(e) => {
							setPassword(e.target.value);
							setErrorStatus(false);
							setPasswordError(false);
						}}
					/>
				</div>

				<Animated
					animationIn="headShake"
					animationOut="fadeOut"
					animationInDuration={1000}
					animationOutDuration={1000}
					isVisible={errorStatus}
					animateOnMount={false}>
					<div className="failed-login">{errorMessage}</div>
				</Animated>

				<div className="submit">
					<div className="forgot-password" onClick={(e: any) => onForgottenPasswordClick(e)}>
						Forgot your password?
					</div>
					<LoginButton className="login-button" type="submit" variant="contained">
						Login
					</LoginButton>
				</div>
			</form>
		</div>
	);

	function onLoginClicked(event: React.FormEvent<HTMLFormElement>) {
		event.preventDefault();

		Axios.post(`/api/authorization/login`, {
			username: username,
			password: password,
		})
			.then(({ data }) => {
				onLoginSuccess(data);
			})
			.catch((response) => {
				const errorMessages = getErrorMessages(response).map((error: any) => {
					const message =
						typeof error.message === "object" ? JSON.stringify(error.message) : error.message;
					return <p>{message}</p>;
				});

				alert(
					<div>
						<h6>Login failed</h6>
						{errorMessages}
					</div>,
					"error"
				);
			});
	}
	// % protected region % [Override onLoginClicked here] end

	// % protected region % [Override onStartRegisterClicked here] off begin

	function onStartRegisterClicked(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
		const { redirect } = queryString.parse(location.search.substring(1));
		store.routerHistory.push(`/register?${!!redirect ? `redirect=${redirect}` : ""}`);
	}
	// % protected region % [Override onStartRegisterClicked here] end

	// % protected region % [Override login success logic here] off begin
	function onLoginSuccess(userResult: IUserResult) {
		store.setLoggedInUser(userResult);

		const { redirect } = queryString.parse(location.search.substring(1));

		if (redirect && !Array.isArray(redirect)) {
			store.routerHistory.push(redirect);
		} else {
			store.routerHistory.push("/");
		}
	}
	// % protected region % [Override login success logic here] end

	// % protected region % [Override onForgottenPasswordClick here] off begin
	function onForgottenPasswordClick(e: React.MouseEvent<HTMLAnchorElement, MouseEvent>) {
		store.routerHistory.push(`/reset-password-request`);
	}
}
