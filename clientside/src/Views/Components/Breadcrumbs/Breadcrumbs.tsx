import * as React from "react";
import { observer } from "mobx-react";
import { Link } from "react-router-dom";
import classNames from "classnames";
import { IIconProps } from "../Helpers/Common";
import If from "../If/If";

export interface IBreadCrumbsTag {
	label: string;
	link?: string;
	className?: string;
	isHomeTag?: boolean;
	isCurrentTag?: boolean;
}

export interface IBreadcrumbsProps {
	tags: IBreadCrumbsTag[];
	className?: string;
	icon?: IIconProps;
}

@observer
export class Breadcrumbs extends React.Component<IBreadcrumbsProps, any> {
	constructor(props: IBreadcrumbsProps, context: any) {
		super(props, context);
	}

	private getTagNode = (tag: IBreadCrumbsTag) => {
		let className = classNames(tag.className);

		if (!tag.isHomeTag) {
			className = classNames(
				className,
				`icon-${this.props.icon && this.props.icon ? this.props.icon.icon : "chevron-right"}`,
				this.props.icon && this.props.icon ? this.props.icon.iconPos : "icon-left"
			);
		}

		if (tag.isCurrentTag) {
			return <li className={className}>{tag.label}</li>;
		} else {
			return (
				<li className={className}>
					<Link to={tag.link ? tag.link : "#"}>{tag.label}</Link>
				</li>
			);
		}
	};

	render() {
		const className = classNames("breadcrumbs", this.props.className);
		return (
			<ul className={className}>
				{this.props.tags.map((tag, index) => {
					return this.getTagNode({
						...tag,
						isHomeTag: index === 0,
						isCurrentTag: this.props.tags.length === index + 1,
					});
				})}
			</ul>
		);
	}
}
