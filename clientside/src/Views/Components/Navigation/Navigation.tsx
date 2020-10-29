import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { IIconProps } from "../Helpers/Common";
import { observer } from "mobx-react";
import { observable, action, computed } from "mobx";
import classNames from "classnames";
import NavigationLinks from "./NavigationLinks";

export enum Orientation {
	VERTICAL,
	HORIZONTAL,
}

export interface ILink extends IIconProps {
	shouldDisplay?: () => boolean;
	path: string;
	label: React.ReactNode;
	onClick?: (event?: any) => void;
	subLinks?: ILink[];
	subLinksFilter?: (link: ILink) => boolean;
	isDisabled?: boolean;
	className?: string;
}

export interface INavigationProps<T extends ILink> extends RouteComponentProps {
	className?: string;
	orientation: Orientation;
	linkGroups: Array<Array<T>>;
	filter?: (link: T) => boolean;
	alwaysExpanded?: boolean;
}

@observer
class Navigation<T extends ILink> extends React.Component<INavigationProps<T>> {
	@computed
	private get alwaysExpanded() {
		const { alwaysExpanded, orientation } = this.props;
		if (orientation === Orientation.HORIZONTAL && alwaysExpanded === undefined) {
			return true;
		}
		return alwaysExpanded;
	}

	@observable
	private navCollapsed: boolean = true;

	public render() {
		const { className, linkGroups, ...routerProps } = this.props;

		let expandButton = null;
		let navClassName = classNames(className, "nav", this.getOrientationClassName());

		if (!this.alwaysExpanded) {
			navClassName = classNames(
				navClassName,
				this.navCollapsed ? "nav--collapsed" : "nav--expanded"
			);
			expandButton = (
				<a
					className={classNames(
						"link-rm-txt-dec expand-icon",
						this.navCollapsed ? "icon-menu" : "icon-menu",
						"icon-left"
					)}
					onClick={this.onClickNavCollapse}
				/>
			);
		}

		return (
			<nav className={navClassName}>
				{linkGroups.map((links, index) => (
					<NavigationLinks key={index} {...routerProps} links={links} />
				))}
				{expandButton}
			</nav>
		);
	}

	private getOrientationClassName = () => {
		const { orientation } = this.props;
		switch (orientation) {
			case Orientation.HORIZONTAL:
				return "nav--horizontal";
			case Orientation.VERTICAL:
				return "nav--vertical";
			default:
				break;
		}
		return "";
	};

	@action
	private onClickNavCollapse = () => {
		this.navCollapsed = !this.navCollapsed;
	};
}

export default Navigation;
