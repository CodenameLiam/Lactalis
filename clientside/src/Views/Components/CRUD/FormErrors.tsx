import * as React from "react";
import { AttributeCRUDOptions } from "Models/CRUDOptions";
import { observer } from "mobx-react";
import If from "../If/If";
import { action, observable } from "mobx";

interface IFormErrorsProps {
	error: React.ReactNode;
	detailedErrors?: React.ReactNode;
}

@observer
class FormErrors extends React.Component<IFormErrorsProps> {
	@observable
	public showDetailedErrors: boolean;

	public render() {
		const { error } = this.props;

		return (
			<div className="form-errors">
				{error}
				{this.props.detailedErrors ? (
					<If condition={!!this.props.detailedErrors}>
						<div>
							<a onClick={() => this.displayErrors()}>
								{this.showDetailedErrors ? "Hide" : "Show"} Detailed Errors
							</a>
							<If condition={this.showDetailedErrors}>
								<div>{this.props.detailedErrors}</div>
							</If>
						</div>
					</If>
				) : null}
			</div>
		);
	}

	@action
	public displayErrors() {
		this.showDetailedErrors = !this.showDetailedErrors;
	}
}

export default FormErrors;
