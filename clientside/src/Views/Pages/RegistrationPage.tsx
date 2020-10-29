import * as React from "react";
import { observer } from "mobx-react";
import { RouteComponentProps } from "react-router";
import { Redirect } from "react-router";
import { Button, Display, Sizes } from "../Components/Button/Button";
import { action, observable, runInAction } from "mobx";
import { store } from "Models/Store";
import axios from "axios";
import * as queryString from "querystring";
import { ButtonGroup, Alignment } from "Views/Components/Button/ButtonGroup";
import If from "Views/Components/If/If";
import { Combobox } from "Views/Components/Combobox/Combobox";
import * as Models from "../../Models/Entities";
import _ from "lodash";
import { Model } from "Models/Model";
import { isRequired } from "Util/EntityUtils";
import { getAttributeComponent } from "Views/Components/CRUD/Attributes/AttributeFactory";
import alert from "../../Util/ToastifyUtils";
import { SERVER_URL } from "Constants";
import { getErrorMessages } from "Util/GraphQLUtils";
import Spinner from "Views/Components/Spinner/Spinner";
import { EntityFormMode } from "Views/Components/Helpers/Common";
import { IUserEntity, UserFields } from "Models/UserEntity";

interface IRegistrationState {
	errors: { [attr: string]: string };
	selectedRegisterType: string;
	modelToRegister: (Model & IUserEntity) | null;
	emailSending: boolean;
	emailSent: boolean;
}

const defaultRegistrationState: IRegistrationState = {
	selectedRegisterType: "",
	modelToRegister: null,
	errors: {},
	emailSending: false,
	emailSent: false,
};

@observer
export default class RegistrationPage extends React.Component<RouteComponentProps> {
	@observable
	private registrationState: IRegistrationState = defaultRegistrationState;

	@observable
	private displayMode: "select-type" | "register" = "register";

	private registerableEntities: { value: string; display: string }[] = [];

	constructor(props: RouteComponentProps, context: any) {
		super(props, context);

		if (this.registerableEntities.length > 1) {
			this.displayMode = "select-type";
		} else {
			this.displayMode = "register";
			if (this.registerableEntities.length === 1) {
				this.registrationState.selectedRegisterType = this.registerableEntities[0].display;
				this.registrationState.modelToRegister = this.userEntityFactory(
					this.registerableEntities[0].value
				);
			}
		}
	}

	public render() {
		let contents = null;

		if (store.loggedIn) {
			return <Redirect to="/" />;
		}

		const selectRegisterType = (
			<div className="body-content">
				<form className="register register-user-type">
					<h2>What type of user are you?</h2>
					<Combobox
						label="User Type"
						model={this.registrationState}
						options={this.registerableEntities}
						modelProperty="selectedRegisterType"
						isRequired={true}
						errors={this.registrationState.errors["selectedRegisterType"]}
					/>
					<ButtonGroup alignment={Alignment.HORIZONTAL} className="select-type-buttons">
						<Button
							className="confirm-type"
							display={Display.Solid}
							sizes={Sizes.Medium}
							buttonProps={{ id: "confirm_type" }}
							onClick={this.onTypeConfirmed}>
							Confirm
						</Button>
						<Button
							className="cancel-register"
							display={Display.Outline}
							sizes={Sizes.Medium}
							buttonProps={{ id: "cancel_register" }}
							onClick={this.onCancelRegisterClicked}>
							Cancel
						</Button>
					</ButtonGroup>
				</form>
			</div>
		);

		const entityAttrs = this.getRegisterEntityAttributes();

		const registerNode = (
			<div className="body-content">
				<form className="register" onSubmit={this.onSubmitRegisterClicked}>
					<If condition={this.registerableEntities.length > 1}>
						<a
							className="change-user-type icon-chevron-left icon-left"
							onClick={this.onChangeUserType}>
							Change User Type
						</a>
					</If>
					<h2>Registration</h2>
					<h5>Registering as a {_.startCase(this.registrationState.selectedRegisterType)}</h5>
					{entityAttrs}
					<ButtonGroup alignment={Alignment.HORIZONTAL} className="register-buttons">
						<Button
							type="submit"
							className="submit-register"
							display={Display.Solid}
							sizes={Sizes.Medium}
							buttonProps={{ id: "submit_register" }}>
							Register
						</Button>
						<Button
							className="cancel-register"
							display={Display.Outline}
							sizes={Sizes.Medium}
							buttonProps={{ id: "cancel_register" }}
							onClick={this.onCancelRegisterClicked}>
							Cancel
						</Button>
					</ButtonGroup>
				</form>
			</div>
		);

		const emailSentMessageNode = (
			<div className="registration registration-success">
				<h2>Registration successful</h2>
				<p>
					Registration for email{" "}
					<span className="bold">
						{this.registrationState.modelToRegister
							? this.registrationState.modelToRegister.email
							: "the user"}
					</span>{" "}
					is successful.
				</p>
				<p>
					{" "}
					We already sent you a confirmation email. You have to confirm your email address before
					you can login
				</p>
				<a className="login-link" onClick={this.onLoginClick}>
					Login
				</a>
			</div>
		);

		contents = (
			<>
				{this.registrationState.emailSending && <Spinner />}
				<If condition={!this.registrationState.emailSent}>
					<If condition={this.displayMode === "select-type"}>{selectRegisterType}</If>
					<If condition={this.displayMode === "register"}>{registerNode}</If>
				</If>
				<If condition={this.registrationState.emailSent}>{emailSentMessageNode}</If>
			</>
		);

		return contents;
	}

	private getRegisterEntityAttributes = () => {
		if (!this.registrationState.modelToRegister) {
			return null;
		} else {
			let attributeOptions = this.registrationState.modelToRegister.getAttributeCRUDOptions();
			const model = this.registrationState.modelToRegister;
			return attributeOptions
				.filter((attributeOption) => {
					return (
						isRequired(model, attributeOption.attributeName) ||
						(UserFields as string[]).indexOf(attributeOption.attributeName) >= 0
					);
				})
				.map((attributeOption) =>
					getAttributeComponent(
						attributeOption,
						model,
						model.getErrorsForAttribute(attributeOption.attributeName),
						EntityFormMode.CREATE,
						isRequired(model, attributeOption.attributeName),
						undefined,
						model ? model.validate.bind(model) : undefined
					)
				);
		}
	};

	@action
	private onSubmitRegisterClicked = async (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();

		this.registrationState.errors = {};
		if (this.registrationState.modelToRegister) {
			await this.registrationState.modelToRegister.validate();
			if (this.registrationState.modelToRegister.hasValidationError) {
				return;
			}
			this.submitRegister();
		}
	};

	@action
	private submitRegister = () => {
		if (this.registrationState.modelToRegister) {
			const userType = this.registrationState.modelToRegister.getModelName();
			const data = this.registrationState.modelToRegister.toJSON({ password: {} });

			this.registrationState.emailSending = true;

			axios
				.post(`${SERVER_URL}/api/register/${_.kebabCase(userType)}`, data)
				.then(({ data }) => {
					this.onRegistrationEmailSentSuccess();
				})
				.catch((response) => {
					runInAction(() => {
						this.registrationState.emailSending = false;
					});
					const errorMessages = getErrorMessages(response).map((error: any) => (
						<p>{error.message}</p>
					));
					alert(
						<div>
							<h6>Registration is not successful</h6>
							{errorMessages}
						</div>,
						"error"
					);
				});
		}
	};

	@action
	private onCancelRegisterClicked = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		event.preventDefault();
		this.resetRegistration();
		const { redirect } = queryString.parse(this.props.location.search.substring(1));
		store.routerHistory.push(`/login?${!!redirect ? `redirect=${redirect}` : ""}`);
	};

	@action
	private onTypeConfirmed = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		event.preventDefault();

		this.registrationState.errors = {};
		if (this.registerableEntities.length > 1 && !this.registrationState.selectedRegisterType) {
			this.registrationState.errors["selectedRegisterType"] =
				"You need to choose the registration type";
		}
		if (Object.keys(this.registrationState.errors).length > 0) {
			return;
		} else {
			let entityToRegister = null;
			if (this.registerableEntities.length > 1 && !!this.registrationState.selectedRegisterType) {
				entityToRegister = this.registrationState.selectedRegisterType;
			} else if (this.registerableEntities.length === 1) {
				entityToRegister = this.registerableEntities[0].value;
			}
			this.displayMode = "register";
			this.registrationState.modelToRegister = this.userEntityFactory(entityToRegister);
		}
	};

	private userEntityFactory = (entityToRegister: string | null): (Model & IUserEntity) | null => {
		switch (entityToRegister) {
			default:
				return null;
		}
	};

	@action
	private onChangeUserType = (e: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => {
		e.preventDefault();
		this.resetRegistration();
		this.displayMode = "select-type";
	};

	private resetRegistration = () => {
		this.registrationState = defaultRegistrationState;
	};

	@action
	private onRegistrationEmailSentSuccess = () => {
		this.registrationState.emailSending = false;
		this.registrationState.emailSent = true;
	};

	private onLoginClick = () => {
		const { redirect } = queryString.parse(this.props.location.search.substring(1));
		store.routerHistory.push(`/login?redirect=${redirect}`);
	};
}
