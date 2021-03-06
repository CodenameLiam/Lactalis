import * as React from "react";
import { observer } from "mobx-react";
import { SERVER_URL } from "../../Constants";
import { RouteComponentProps } from "react-router";
import queryString from "querystring";
import Spinner from "Views/Components/Spinner/Spinner";
import If from "Views/Components/If/If";
import axios from "axios";
import { getErrorMessages } from "Util/GraphQLUtils";
import { observable, runInAction, action } from "mobx";
import alert from "../../Util/ToastifyUtils";
import { store } from "Models/Store";
import { ButtonGroup, Alignment } from "Views/Components/Button/ButtonGroup";
import { Button, Display, Sizes } from "Views/Components/Button/Button";

/**
 * Logs the user out of the application
 *
 * Can take a location query param that will redirect after the logout occurs
 */

@observer
export default class RegistrationConfirmPage extends React.Component<RouteComponentProps> {
	@observable
	private loading: boolean = false;
	@observable
	private confirmed: boolean = false;
	private confirmEmailData: { token: string; email: string };

	public componentDidMount() {
		const { token, username } =
			queryString.parse(this.props.location.search.substring(1)) || undefined;
		this.confirmEmailData = {
			token: token as string,
			email: username as string,
		};
	}

	public render() {
		let pageContent = (
			<div className="registration registration-confirm">
				<h2>Confirm your registration</h2>
				<p>Please click the button below to finish your registration</p>
				<ButtonGroup alignment={Alignment.HORIZONTAL} className="confirm-reg-buttons">
					<Button
						type="submit"
						className="confirm-registration"
						display={Display.Solid}
						sizes={Sizes.Medium}
						buttonProps={{ id: "confirm_registration" }}
						onClick={this.onClickConfirm}>
						Confirm Registration
					</Button>
				</ButtonGroup>
			</div>
		);

		if (this.confirmed) {
			pageContent = (
				<div className="body-content">
					{this.loading && <Spinner />}
					<If condition={!this.loading}>
						<div className="registration registration-success">
							<h2>Registration successful</h2>
							<p>Your email has been confirmed successfully</p>
							<a className="login-link" onClick={this.onLoginClick}>
								Login
							</a>
						</div>
					</If>
				</div>
			);
		}

		return pageContent;
	}

	private onLoginClick = () => {
		const { redirect } = queryString.parse(this.props.location.search.substring(1));
		if (redirect) {
			store.routerHistory.push(`/login?redirect=${redirect}`);
		}
		store.routerHistory.push("/login");
	};

	@action
	private onClickConfirm = () => {
		this.loading = true;
		axios
			.post(`${SERVER_URL}/api/register/confirm-email`, this.confirmEmailData)
			.then(({ data }) => {
				runInAction(() => {
					this.loading = false;
					this.confirmed = true;
				});
			})
			.catch((response) => {
				runInAction(() => {
					this.loading = false;
				});
				const errorMessages = getErrorMessages(response).map((error: any) => (
					<p>{error.message}</p>
				));
				alert(
					<div>
						<h6>Email Confirmation was not successful</h6>
						{errorMessages}
					</div>,
					"error"
				);
			});
	};
}
