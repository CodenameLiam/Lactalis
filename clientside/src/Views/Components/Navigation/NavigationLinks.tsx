import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { filter } from "lodash";
import NavigationLink from "./NavigationLink";
import { ILink } from "./Navigation";
import { observer } from "mobx-react";
import { action } from "mobx";

export interface INavigationLinksProps<T extends ILink> extends RouteComponentProps {
	className?: string;
	links: Array<T>;
	filter?: (link: T) => boolean;
}

@observer
class NavigationLinks<T extends ILink> extends React.Component<INavigationLinksProps<T>> {
	public render() {
		const { className, links, ...routerProps } = this.props;
		const htmlLinks = filter(links, this.props.filter)
			.filter((link) => (link.shouldDisplay ? link.shouldDisplay() : true))
			.map((link) => (
				<NavigationLink
					{...link}
					{...routerProps}
					path={link.path}
					label={link.label}
					icon={link.icon}
					iconPos={link.iconPos}
					key={link.path}
					isParent={!!link.subLinks}
					onClick={() => this.onClick(link)}
					isDisabled={link.isDisabled}
					subLinksFilter={link.subLinksFilter}
					className={link.className}
				/>
			));

		let content = <ul className={className}>{htmlLinks}</ul>;

		return content;
	}

	@action
	private onClick = (link: ILink) => {
		if (!!link.onClick) {
			link.onClick();
		}
	};
}

export default NavigationLinks;
