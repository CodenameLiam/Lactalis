import * as React from "react";
import { observer } from "mobx-react";
import { action, computed, observable } from "mobx";
import { Button } from "Views/Components/Button/Button";

export interface IAccordionConfig {
	name: string;
	component: React.ReactNode;
	key: string;
	afterTitleContent?: React.ReactNode;
	disabled?: boolean;
}

export interface IAccordionGroupProps {
	accordions: Array<IAccordionConfig>;
}

@observer
export class AccordionSection extends React.Component<IAccordionConfig> {
	@observable
	private showContents = false;

	@computed
	private get isOpen() {
		return this.showContents && !this.props.disabled;
	}

	@action
	public toggleContents = (isOpen?: boolean) => {
		if (isOpen === undefined) {
			this.showContents = !this.showContents;
		} else {
			this.showContents = isOpen;
		}
	};

	private get contentClassName() {
		return this.isOpen
			? "accordion__info accordion__info--expanded"
			: "accordion__info accordion__info--collapsed";
	}

	public render() {
		return (
			<section className={this.isOpen ? "accordion active" : "accordion"}>
				<Button
					icon={{ icon: "chevron-up", iconPos: "icon-right" }}
					onClick={() => this.toggleContents()}>
					{this.props.name}
				</Button>
				{this.props.afterTitleContent}
				<div className={this.contentClassName}>{this.isOpen ? this.props.component : null}</div>
			</section>
		);
	}
}

export default class AccordionGroup extends React.Component<IAccordionGroupProps> {
	public render() {
		return (
			<>
				{this.props.accordions.map((accordion) => (
					<AccordionSection {...accordion} />
				))}
			</>
		);
	}
}
