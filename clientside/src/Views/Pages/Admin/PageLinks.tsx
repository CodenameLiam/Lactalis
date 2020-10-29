import * as React from "react";
import Navigation, { Orientation, ILink } from "Views/Components/Navigation/Navigation";
import { RouteComponentProps } from "react-router";
import * as Models from "Models/Entities";
import { Model } from "Models/Model";
import { IIconProps } from "Views/Components/Helpers/Common";
import { SecurityService } from "Services/SecurityService";
import { store } from "Models/Store";
import { getModelDisplayName } from "Util/EntityUtils";

interface AdminLink extends IIconProps {
	path: string;
	label: string;
	entity: { new (args: any): Model };
	isMember?: boolean;
}

const getPageLinks = (): AdminLink[] => [
	{
		path: "/admin/tradingpostlistingentity",
		label: getModelDisplayName(Models.TradingPostListingEntity),
		entity: Models.TradingPostListingEntity,
		isMember: false,
	},
	{
		path: "/admin/tradingpostcategoryentity",
		label: getModelDisplayName(Models.TradingPostCategoryEntity),
		entity: Models.TradingPostCategoryEntity,
		isMember: false,
	},
	{
		path: "/admin/adminentity",
		label: getModelDisplayName(Models.AdminEntity),
		entity: Models.AdminEntity,
		isMember: true,
	},
	{
		path: "/admin/farmentity",
		label: getModelDisplayName(Models.FarmEntity),
		entity: Models.FarmEntity,
		isMember: false,
	},
	{
		path: "/admin/milktestentity",
		label: getModelDisplayName(Models.MilkTestEntity),
		entity: Models.MilkTestEntity,
		isMember: false,
	},
	{
		path: "/admin/farmerentity",
		label: getModelDisplayName(Models.FarmerEntity),
		entity: Models.FarmerEntity,
		isMember: true,
	},
	{
		path: "/admin/importantdocumentcategoryentity",
		label: getModelDisplayName(Models.ImportantDocumentCategoryEntity),
		entity: Models.ImportantDocumentCategoryEntity,
		isMember: false,
	},
	{
		path: "/admin/technicaldocumentcategoryentity",
		label: getModelDisplayName(Models.TechnicalDocumentCategoryEntity),
		entity: Models.TechnicalDocumentCategoryEntity,
		isMember: false,
	},
	{
		path: "/admin/qualitydocumentcategoryentity",
		label: getModelDisplayName(Models.QualityDocumentCategoryEntity),
		entity: Models.QualityDocumentCategoryEntity,
		isMember: false,
	},
	{
		path: "/admin/qualitydocumententity",
		label: getModelDisplayName(Models.QualityDocumentEntity),
		entity: Models.QualityDocumentEntity,
		isMember: false,
	},
	{
		path: "/admin/technicaldocumententity",
		label: getModelDisplayName(Models.TechnicalDocumentEntity),
		entity: Models.TechnicalDocumentEntity,
		isMember: false,
	},
	{
		path: "/admin/importantdocumententity",
		label: getModelDisplayName(Models.ImportantDocumentEntity),
		entity: Models.ImportantDocumentEntity,
		isMember: false,
	},
	{
		path: "/admin/newsarticleentity",
		label: getModelDisplayName(Models.NewsArticleEntity),
		entity: Models.NewsArticleEntity,
		isMember: false,
	},
	{
		path: "/admin/agrisupplydocumentcategoryentity",
		label: getModelDisplayName(Models.AgriSupplyDocumentCategoryEntity),
		entity: Models.AgriSupplyDocumentCategoryEntity,
		isMember: false,
	},
	{
		path: "/admin/sustainabilitypostentity",
		label: getModelDisplayName(Models.SustainabilityPostEntity),
		entity: Models.SustainabilityPostEntity,
		isMember: false,
	},
	{
		path: "/admin/agrisupplydocumententity",
		label: getModelDisplayName(Models.AgriSupplyDocumentEntity),
		entity: Models.AgriSupplyDocumentEntity,
		isMember: false,
	},
	{
		path: "/admin/promotedarticlesentity",
		label: getModelDisplayName(Models.PromotedArticlesEntity),
		entity: Models.PromotedArticlesEntity,
		isMember: false,
	},
];

export default class PageLinks extends React.Component<RouteComponentProps> {
	private filter = (link: AdminLink) => {
		return SecurityService.canRead(link.entity);
	};

	public render() {
		return (
			<Navigation
				className="nav__admin"
				orientation={Orientation.VERTICAL}
				linkGroups={this.getAdminNavLinks()}
				{...this.props}
			/>
		);
	}

	private getAdminNavLinks = (): ILink[][] => {
		const links = getPageLinks();
		let userLinks = links.filter((link) => link.isMember).filter(this.filter);
		let entityLinks = links.filter((link) => !link.isMember).filter(this.filter);

		let linkGroups: ILink[][] = [];

		const homeLinkGroup: ILink[] = [
			{ path: "/admin", label: "Home", icon: "home", iconPos: "icon-left" },
			// { path: '/admin/settings', label: 'Settings', icon: 'settings', iconPos: 'icon-left', isDisabled: true }
		];
		linkGroups.push(homeLinkGroup);

		const entityLinkGroup: ILink[] = [];
		if (userLinks.length > 0) {
			entityLinkGroup.push({
				path: "/admin/users",
				label: "Users",
				icon: "person-group",
				iconPos: "icon-left",
				subLinks: [
					{ path: "/admin/user", label: "All Users" },
					...userLinks.map((link) => ({ path: link.path, label: link.label })),
				],
			});
		}
		if (entityLinks.length > 0) {
			entityLinkGroup.push({
				path: "/admin/entities",
				label: "Entities",
				icon: "list",
				iconPos: "icon-left",
				subLinks: entityLinks.map((link) => {
					return {
						path: link.path,
						label: link.label,
					};
				}),
			});
		}
		linkGroups.push(entityLinkGroup);

		// Removed these links until these behaviours are activated in the future
		const otherlinkGroup: ILink[] = [];
		//otherlinkGroup.push({ path: '/admin/dashboards', label: 'Dashboards', icon: 'dashboard', iconPos: 'icon-left', isDisabled: true });
		//otherlinkGroup.push({ path: '/admin/timelines', label: 'Timelines', icon: 'timeline', iconPos: 'icon-left', isDisabled: true });
		if (otherlinkGroup.length > 0) {
			linkGroups.push(otherlinkGroup);
		}

		const bottomlinkGroup: ILink[] = [];
		// bottomlinkGroup.push({ path: '/admin/documentation', label: 'Documentation', icon: 'help', iconPos: 'icon-left', isDisabled: true });
		bottomlinkGroup.push({
			path: "/logout",
			label: "Logout",
			icon: "logout",
			iconPos: "icon-left",
		});
		linkGroups.push(bottomlinkGroup);

		return linkGroups;
	};
}
