import * as React from "react";
import { TextField } from "../TextBox/TextBox";
import { Button, Display } from "../Button/Button";
import { observer } from "mobx-react";
import classNames from "classnames";
import { action } from "mobx";

export interface ISearchProps {
	/** A json object that contains the search string */
	model: {
		searchTerm: string;
	};
	/** Class name for the component */
	className?: string;
	/** A suffix to append to the search class name */
	classNameSuffix?: string;
	/** A label to be placed in the aria label element for assistive technology */
	label?: string;
	/** A handler for the submission of a search */
	onSubmit?: () => void;
	/** A handler bound the onAfterChange handler of the inner text field */
	onChange?: (value: string) => void;
	/**
	 * Should the class contain a click to clear button.
	 * Clicking this button is counted as a submission, not a change
	 */
	clickToClear?: boolean;
	/**
	 * Any extra react nodes to be rendered in the form
	 */
	extraNodes?: React.ReactNode;
}

/**
 * A standardised search form
 */
@observer
export default class SearchForm extends React.Component<ISearchProps> {
	static defaultProps: Partial<ISearchProps> = {
		label: "A search form",
		clickToClear: true,
	};

	public render() {
		const className = classNames(
			this.props.className,
			"search",
			this.props.classNameSuffix ? `search__${this.props.classNameSuffix}` : undefined
		);
		return (
			<form
				aria-label={this.props.label}
				onSubmit={this.onSubmit}
				className={className}
				role="search">
				<TextField
					model={this.props.model}
					modelProperty={"searchTerm"}
					label="Search"
					labelVisible={false}
					placeholder="Search"
					onAfterChange={this.onChange}
					clickToClear={this.props.clickToClear}
					onClickToClear={this.onClickToClear}
				/>
				{this.props.extraNodes}
				<Button display={Display.Solid} type="submit">
					Go
				</Button>
			</form>
		);
	}

	/**
	 * Handler for the text box changing
	 * @param event The HTML input event
	 */
	private onChange: React.ChangeEventHandler<HTMLInputElement> = (event) => {
		if (this.props.onChange) {
			this.props.onChange(event.target.value);
		}
	};

	/**
	 * Handler for the form being submitted
	 * @param event The HTML form input event
	 */
	private onSubmit: React.FormEventHandler<HTMLFormElement> = (event) => {
		event.preventDefault();
		if (this.props.onSubmit) {
			this.props.onSubmit();
		}
	};

	/**
	 * Clears the text field and submits the form
	 */
	@action
	private onClickToClear = () => {
		this.props.model.searchTerm = "";
		if (this.props.onSubmit) {
			this.props.onSubmit();
		}
	};
}
