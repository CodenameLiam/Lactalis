import * as React from "react";
import { observer } from "mobx-react";
import If from "Views/Components/If/If";
import { ButtonGroup } from "Views/Components/Button/ButtonGroup";
import { Button, Display } from "Views/Components/Button/Button";

export interface IFormProps {
	/** Use the default Submit button or not */
	submitButton?: boolean;
	/** Use the default Cancel button or not */
	cancelButton?: boolean;
	/**
	 * Action Groups
	 * If specified, the default submit and cancel action buttons will not display unconditionally
	 * Then you need to use this actionGroups to specify all the actions button
	 */
	actionGroups?: React.ReactNode[];
	/** The callback function when submit event is triggered */
	onSubmit?: React.FormEventHandler<Element>;
	/** The callback function when cencel button is pressed */
	onCancel?: React.FormEventHandler<Element>;
}

@observer
export class Form extends React.Component<IFormProps> {
	render() {
		let actionGroups: React.ReactNode[] | undefined;
		if (this.props.actionGroups) {
			actionGroups = this.props.actionGroups;
		} else {
			actionGroups = [
				<If condition={this.props.cancelButton}>
					<Button
						className="cancel btn--md"
						type="button"
						display={Display.Outline}
						buttonProps={{ onClick: this.props.onCancel }}>
						Cancel
					</Button>
				</If>,
				<If condition={this.props.submitButton}>
					<Button className="submit btn--md" type="submit" display={Display.Solid}>
						Submit
					</Button>
				</If>,
			];
		}

		return (
			<form onSubmit={this.props.onSubmit}>
				<div className="crud__form-container">{this.props.children}</div>
				<ButtonGroup>
					{actionGroups.map((node, i) => (
						<React.Fragment key={i}>{node}</React.Fragment>
					))}
				</ButtonGroup>
			</form>
		);
	}
}
