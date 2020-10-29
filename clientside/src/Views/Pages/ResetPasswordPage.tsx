import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import { Redirect } from "react-router";
import { Button, Display, Sizes } from "../Components/Button/Button";
import { action, observable } from "mobx";
import { store } from "Models/Store";
import { SERVER_URL } from "Constants";
import axios from "axios";
import { ButtonGroup, Alignment } from "Views/Components/Button/ButtonGroup";
import _ from "lodash";
import alert from "../../Util/ToastifyUtils";
import { Password } from "Views/Components/Password/Password";
import * as queryString from "querystring";

interface IResetPasswordState {
	token: string;
	username: string;
	password: string;
	confirmPassword: string;
	errors: { [attr: string]: string };
}

const defaultResetPasswordState: IResetPasswordState = {
	token: "",
	username: "",
	password: "",
	confirmPassword: "",
	errors: {},
};

@observer
export default class ResetPasswordPage extends React.Component<RouteComponentProps> {
	@observable
	private resetPasswordState: IResetPasswordState = defaultResetPasswordState;

	constructor(props: RouteComponentProps, context: any) {
		super(props, context);
		let queryParams = this.props.location.search.substring(1);
		const { token, username } = queryString.parse(queryParams);
		this.resetPasswordState.token = token as string;
		this.resetPasswordState.username = username as string;
	}

	public render() {
		let contents = null;

		if (store.loggedIn) {
			return <Redirect to="/" />;
		}

		contents = (
			<div className="body-content">
				<form className="reset-password" onSubmit={this.onResetPasswordClicked}>
					<h2>Reset Password</h2>
					<Password
						id="new_password"
						className="new-password"
						model={this.resetPasswordState}
						modelProperty="password"
						label="New Password"
						isRequired={true}
						errors={this.resetPasswordState.errors["password"]}
					/>
					<Password
						id="confirm_password"
						className="confirm-password"
						model={this.resetPasswordState}
						modelProperty="confirmPassword"
						label="Confirm Password"
						isRequired={true}
						errors={this.resetPasswordState.errors["confirmPassword"]}
					/>
					<ButtonGroup alignment={Alignment.HORIZONTAL} className="confirm-pwd-buttons">
						<Button
							type="submit"
							className="confirm-reset-password"
							display={Display.Solid}
							sizes={Sizes.Medium}
							buttonProps={{ id: "confirm_reset_password" }}>
							Confirm Password
						</Button>
					</ButtonGroup>
				</form>
			</div>
		);

		return contents;
	}

	@action
	private onResetPasswordClicked = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();

		this.resetPasswordState.errors = {};

		if (!this.resetPasswordState.password) {
			this.resetPasswordState.errors["password"] = "Password is required";
		} else if (this.resetPasswordState["password"].length < 6) {
			this.resetPasswordState.errors["password"] = "The minimum length of password is 6";
		} else if (!this.resetPasswordState["confirmPassword"]) {
			this.resetPasswordState.errors["confirmPassword"] = "Confirm password is required";
		} else if (this.resetPasswordState["confirmPassword"] !== this.resetPasswordState.password) {
			this.resetPasswordState.errors["confirmPassword"] =
				"Password and confirm password does not match";
		}

		if (Object.keys(this.resetPasswordState.errors).length > 0) {
			return;
		} else {
			axios
				.post(`${SERVER_URL}/api/account/reset-password`, {
					token: this.resetPasswordState.token,
					username: this.resetPasswordState.username,
					password: this.resetPasswordState.password,
				})
				.then(({ data }) => {
					this.onConfirmPasswordSent();
				})
				.catch((error) => {
					const errorMsgs = error.response.data.errors.map((error: any) => <p>{error.message}</p>);
					alert(
						<div>
							<h6>Password could not be changed: </h6>
							{errorMsgs}
						</div>,
						"error"
					);
				});
		}
	};

	@action
	private onConfirmPasswordSent = () => {
		alert(`Your password has been reset`, "success");
		store.routerHistory.push(`/login`);
	};
}
