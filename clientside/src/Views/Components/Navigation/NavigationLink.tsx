import { RouteComponentProps, matchPath } from "react-router";
import * as React from "react";
import classNames from "classnames";
import { Link } from "react-router-dom";
import { observer } from "mobx-react";
import { computed, action, observable } from "mobx";
import { IIconProps } from "../Helpers/Common";
import { ILink } from "./Navigation";
import NavigationLinks from "./NavigationLinks";
import If from "../If/If";

interface INavigationLinkProps extends RouteComponentProps, IIconProps {
	path: string;
	className?: string;
	label: React.ReactNode;
	isParent?: boolean;
	subLinks?: ILink[];
	subLinksFilter?: (link: ILink) => boolean;
	onClick?: (event: React.MouseEvent<HTMLLIElement, MouseEvent>) => void;
	isActive?: boolean;
	isDisabled?: boolean;
}

@observer
class NavigationLink extends React.Component<INavigationLinkProps> {
	static defaultProps = {
		iconPos: "left",
	};

	private liRef: HTMLLIElement | null = null;

	componentDidMount() {
		document.addEventListener("click", this.handleDocumentClick);
	}
	componentWillMount() {
		document.removeEventListener("click", this.handleDocumentClick);
	}
	componentWillUnmount() {
		document.removeEventListener("click", this.handleDocumentClick);
	}

	@observable
	private subLinksExpanded: boolean = false;

	@computed
	private get icon() {
		if (this.props.icon) {
			return `icon-${this.props.icon}`;
		}
		return undefined;
	}

	@computed
	private get iconPos() {
		if (this.icon) {
			return this.props.iconPos;
		}
		return undefined;
	}

	public render() {
		const { path, label, subLinks, subLinksFilter, ...routerProps } = this.props;
		let subLinksNode = null;
		if (this.props.isParent && !!this.props.subLinks) {
			const ulClazz = classNames(
				"nav__sublinks",
				this.subLinksExpanded ? "nav__sublinks--visible" : ""
			);
			subLinksNode = (
				<NavigationLinks
					{...routerProps}
					className={ulClazz}
					links={subLinks || []}
					filter={subLinksFilter}
				/>
			);
		}

		const isActive = this.props.isActive ? this.props.isActive : this.isActive(path);

		let textNode = !!this.icon ? <span>{label}</span> : label;

		return (
			<li
				ref={(ref: any) => {
					if (!!ref) {
						this.liRef = ref;
					}
				}}
				className={classNames(
					{
						"nav__parent-link--active": this.subLinksExpanded && this.props.isParent,
						active: isActive && !this.props.isParent,
					},
					this.props.className
				)}
				key={path}
				onClick={this.onClick}>
				<If condition={this.props.isParent || this.props.isDisabled}>
					<a
						className={classNames(this.icon, this.iconPos)}
						aria-label={typeof label === "string" ? label : undefined}>
						{textNode}
					</a>
				</If>
				<If condition={!(this.props.isParent || this.props.isDisabled)}>
					<Link
						to={path}
						className={classNames(this.icon, this.iconPos)}
						aria-label={typeof label === "string" ? label : undefined}>
						{textNode}
					</Link>
				</If>
				{subLinksNode}
			</li>
		);
	}

	@action
	private onClick = (event: React.MouseEvent<HTMLLIElement, MouseEvent>) => {
		if (this.props.isParent) {
			this.subLinksExpanded = !this.subLinksExpanded;
		}
		if (this.props.onClick) {
			this.props.onClick(event);
		}
	};

	private isActive = (path: string) => {
		return !!matchPath(this.props.location.pathname, { path, exact: true });
	};

	@action
	private handleDocumentClick = (e: any) => {
		if (
			this.subLinksExpanded &&
			!!this.liRef &&
			(!this.liRef.contains(e.target) ||
				(!!this.liRef.lastElementChild && this.liRef.lastElementChild.contains(e.target)))
		) {
			this.subLinksExpanded = !this.subLinksExpanded;
		}
	};
}

export default NavigationLink;
