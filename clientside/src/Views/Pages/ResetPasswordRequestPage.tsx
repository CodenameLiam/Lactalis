import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import { Redirect } from "react-router";
import { Button, Display, Sizes, Colors } from "../Components/Button/Button";
import { action, observable } from "mobx";
import { TextField } from "../Components/TextBox/TextBox";
import { store } from "Models/Store";
import { SERVER_URL } from "Constants";
import axios from "axios";
import { ButtonGroup, Alignment } from "Views/Components/Button/ButtonGroup";
import _ from "lodash";
import { isEmail } from "Validators/Functions/Email";
import alert from "../../Util/ToastifyUtils";
import { getErrorMessages } from "Util/GraphQLUtils";

interface IResetRequestState {
	username: string;
	errors: { [attr: string]: string };
}

const defaultResetRequestState: IResetRequestState = {
	username: "",
	errors: {},
};

@observer
export default class ResetPasswordRequestPage extends React.Component<RouteComponentProps> {
	@observable
	private ResetRequestState: IResetRequestState = defaultResetRequestState;

	public render() {
		let contents = null;

		if (store.loggedIn) {
			return <Redirect to="/" />;
		}

		contents = (
			<div className="body-content">
				<form className="reset-password" onSubmit={this.onResetPasswordClicked}>
					<h2>Reset Password</h2>
					<TextField
						id="username"
						className="username"
						model={this.ResetRequestState}
						modelProperty="username"
						label="Email Address"
						inputProps={{ autoComplete: "username" }}
						isRequired={true}
						errors={this.ResetRequestState.errors["username"]}
					/>
					<ButtonGroup alignment={Alignment.HORIZONTAL} className="reset-pwd-buttons">
						<Button
							type="submit"
							display={Display.Solid}
							sizes={Sizes.Medium}
							buttonProps={{ id: "reset_password" }}>
							Reset Password
						</Button>
						<Button
							className="cancel-reset-pwd"
							display={Display.Outline}
							sizes={Sizes.Medium}
							colors={Colors.Secondary}
							buttonProps={{ id: "cancel_reset_pwd" }}
							onClick={this.onCancelResetClicked}>
							Cancel
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

		this.ResetRequestState.errors = {};

		if (!this.ResetRequestState.username) {
			this.ResetRequestState.errors["username"] = "Email Address is required";
		} else if (!isEmail(this.ResetRequestState.username)) {
			this.ResetRequestState.errors["username"] = "This is not a valid email address";
		}

		if (Object.keys(this.ResetRequestState.errors).length > 0) {
			return;
		} else {
			axios
				.post(`${SERVER_URL}/api/account/reset-password-request`, {
					username: this.ResetRequestState.username,
				})
				.then(({ data }) => {
					this.onResetPasswordSent();
				})
				.catch((response) => {
					const errorMessages = getErrorMessages(response).map((error: any) => (
						<p>{error.message}</p>
					));
					alert(
						<div>
							<h6>Sending request failed</h6>
							{errorMessages}
						</div>,
						"error"
					);
				});
		}
	};

	@action
	private onCancelResetClicked = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		store.routerHistory.push(`/login`);
	};

	@action
	private onResetPasswordSent = () => {
		alert(`Reset Password Email has been sent to ${this.ResetRequestState.username}`, "success");
		store.routerHistory.push(`/login`);
	};
}
