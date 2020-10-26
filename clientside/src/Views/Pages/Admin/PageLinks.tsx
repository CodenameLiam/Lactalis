/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
import * as React from 'react';
import Navigation, { Orientation, ILink } from 'Views/Components/Navigation/Navigation';
import { RouteComponentProps } from 'react-router';
import * as Models from 'Models/Entities';
import { Model } from 'Models/Model';
import { IIconProps } from "Views/Components/Helpers/Common";
import { SecurityService } from "Services/SecurityService";
import { store } from "Models/Store";
import { getModelDisplayName } from 'Util/EntityUtils';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

interface AdminLink extends IIconProps {
	path: string;
	label: string;
	entity: {new (args: any): Model};
	isMember?: boolean;
	// % protected region % [Add extra AdminLink fields here] off begin
	// % protected region % [Add extra AdminLink fields here] end
}

const getPageLinks = (): AdminLink[] => [
	{
		// % protected region % [Override navigation link for TradingPostListingEntity here] off begin
		path: '/admin/tradingpostlistingentity',
		label: getModelDisplayName(Models.TradingPostListingEntity),
		entity: Models.TradingPostListingEntity,
		isMember: false
		// % protected region % [Override navigation link for TradingPostListingEntity here] end
	},
	{
		// % protected region % [Override navigation link for TradingPostCategoryEntity here] off begin
		path: '/admin/tradingpostcategoryentity',
		label: getModelDisplayName(Models.TradingPostCategoryEntity),
		entity: Models.TradingPostCategoryEntity,
		isMember: false
		// % protected region % [Override navigation link for TradingPostCategoryEntity here] end
	},
	{
		// % protected region % [Override navigation link for AdminEntity here] off begin
		path: '/admin/adminentity',
		label: getModelDisplayName(Models.AdminEntity),
		entity: Models.AdminEntity,
		isMember: true
		// % protected region % [Override navigation link for AdminEntity here] end
	},
	{
		// % protected region % [Override navigation link for FarmEntity here] off begin
		path: '/admin/farmentity',
		label: getModelDisplayName(Models.FarmEntity),
		entity: Models.FarmEntity,
		isMember: false
		// % protected region % [Override navigation link for FarmEntity here] end
	},
	{
		// % protected region % [Override navigation link for MilkTestEntity here] off begin
		path: '/admin/milktestentity',
		label: getModelDisplayName(Models.MilkTestEntity),
		entity: Models.MilkTestEntity,
		isMember: false
		// % protected region % [Override navigation link for MilkTestEntity here] end
	},
	{
		// % protected region % [Override navigation link for FarmerEntity here] off begin
		path: '/admin/farmerentity',
		label: getModelDisplayName(Models.FarmerEntity),
		entity: Models.FarmerEntity,
		isMember: true
		// % protected region % [Override navigation link for FarmerEntity here] end
	},
	{
		// % protected region % [Override navigation link for ImportantDocumentCategoryEntity here] off begin
		path: '/admin/importantdocumentcategoryentity',
		label: getModelDisplayName(Models.ImportantDocumentCategoryEntity),
		entity: Models.ImportantDocumentCategoryEntity,
		isMember: false
		// % protected region % [Override navigation link for ImportantDocumentCategoryEntity here] end
	},
	{
		// % protected region % [Override navigation link for QualityDocumentCategoryEntity here] off begin
		path: '/admin/qualitydocumentcategoryentity',
		label: getModelDisplayName(Models.QualityDocumentCategoryEntity),
		entity: Models.QualityDocumentCategoryEntity,
		isMember: false
		// % protected region % [Override navigation link for QualityDocumentCategoryEntity here] end
	},
	{
		// % protected region % [Override navigation link for TechnicalDocumentCategoryEntity here] off begin
		path: '/admin/technicaldocumentcategoryentity',
		label: getModelDisplayName(Models.TechnicalDocumentCategoryEntity),
		entity: Models.TechnicalDocumentCategoryEntity,
		isMember: false
		// % protected region % [Override navigation link for TechnicalDocumentCategoryEntity here] end
	},
	{
		// % protected region % [Override navigation link for QualityDocumentEntity here] off begin
		path: '/admin/qualitydocumententity',
		label: getModelDisplayName(Models.QualityDocumentEntity),
		entity: Models.QualityDocumentEntity,
		isMember: false
		// % protected region % [Override navigation link for QualityDocumentEntity here] end
	},
	{
		// % protected region % [Override navigation link for TechnicalDocumentEntity here] off begin
		path: '/admin/technicaldocumententity',
		label: getModelDisplayName(Models.TechnicalDocumentEntity),
		entity: Models.TechnicalDocumentEntity,
		isMember: false
		// % protected region % [Override navigation link for TechnicalDocumentEntity here] end
	},
	{
		// % protected region % [Override navigation link for ImportantDocumentEntity here] off begin
		path: '/admin/importantdocumententity',
		label: getModelDisplayName(Models.ImportantDocumentEntity),
		entity: Models.ImportantDocumentEntity,
		isMember: false
		// % protected region % [Override navigation link for ImportantDocumentEntity here] end
	},
	{
		// % protected region % [Override navigation link for NewsArticleEntity here] off begin
		path: '/admin/newsarticleentity',
		label: getModelDisplayName(Models.NewsArticleEntity),
		entity: Models.NewsArticleEntity,
		isMember: false
		// % protected region % [Override navigation link for NewsArticleEntity here] end
	},
	{
		// % protected region % [Override navigation link for PromotedArticlesEntity here] off begin
		path: '/admin/promotedarticlesentity',
		label: getModelDisplayName(Models.PromotedArticlesEntity),
		entity: Models.PromotedArticlesEntity,
		isMember: false
		// % protected region % [Override navigation link for PromotedArticlesEntity here] end
	},
	{
		// % protected region % [Override navigation link for AgriSupplyDocumentCategoryEntity here] off begin
		path: '/admin/agrisupplydocumentcategoryentity',
		label: getModelDisplayName(Models.AgriSupplyDocumentCategoryEntity),
		entity: Models.AgriSupplyDocumentCategoryEntity,
		isMember: false
		// % protected region % [Override navigation link for AgriSupplyDocumentCategoryEntity here] end
	},
	{
		// % protected region % [Override navigation link for SustainabilityPostEntity here] off begin
		path: '/admin/sustainabilitypostentity',
		label: getModelDisplayName(Models.SustainabilityPostEntity),
		entity: Models.SustainabilityPostEntity,
		isMember: false
		// % protected region % [Override navigation link for SustainabilityPostEntity here] end
	},
	{
		// % protected region % [Override navigation link for AgriSupplyDocumentEntity here] off begin
		path: '/admin/agrisupplydocumententity',
		label: getModelDisplayName(Models.AgriSupplyDocumentEntity),
		entity: Models.AgriSupplyDocumentEntity,
		isMember: false
		// % protected region % [Override navigation link for AgriSupplyDocumentEntity here] end
	},
	// % protected region % [Add any extra page links here] off begin
	// % protected region % [Add any extra page links here] end
];

export default class PageLinks extends React.Component<RouteComponentProps> {
	private filter = (link: AdminLink) => {
		return SecurityService.canRead(link.entity);
	}

	public render() {
		return <Navigation
			className='nav__admin'
			orientation={Orientation.VERTICAL}
			linkGroups={this.getAdminNavLinks()}
			{...this.props} />;
	}

	private getAdminNavLinks = () : ILink[][] => {
		// % protected region % [Add custom logic before all here] off begin
		// % protected region % [Add custom logic before all here] end

		const links = getPageLinks();
		let userLinks = links.filter(link => link.isMember).filter(this.filter);
		let entityLinks = links.filter(link => ! link.isMember).filter(this.filter);

		let linkGroups: ILink[][] = [];

		// % protected region % [Add any custom logic here before groups are made] off begin
		// % protected region % [Add any custom logic here before groups are made] end

		const homeLinkGroup: ILink[] = [
			{ path: '/admin', label: 'Home', icon: 'home', iconPos: 'icon-left' },
			// { path: '/admin/settings', label: 'Settings', icon: 'settings', iconPos: 'icon-left', isDisabled: true }

			// % protected region % [Updated your home link group here] off begin
			// % protected region % [Updated your home link group here] end
		];
		linkGroups.push(homeLinkGroup);

		const entityLinkGroup: ILink[] = [];
		if (userLinks.length > 0) {
			entityLinkGroup.push(
				{
					path: '/admin/users',
					label: 'Users',
					icon: 'person-group',
					iconPos: 'icon-left',
					subLinks: [
						{path: "/admin/user", label: "All Users"},
						...userLinks.map(link => ({path: link.path, label: link.label}))
					]
				}
			);
		}
		if (entityLinks.length > 0) {
			entityLinkGroup.push(
				{
					path: '/admin/entities',
					label: 'Entities',
					icon: 'list',
					iconPos: 'icon-left',
					subLinks: entityLinks.map(link => {
						return {
							path: link.path,
							label: link.label,
						}
					})
				}
			);
		}
		linkGroups.push(entityLinkGroup);

		// % protected region % [Add any new link groups here before other and bottom] off begin
		// % protected region % [Add any new link groups here before other and bottom] end

		// Removed these links until these behaviours are activated in the future
		const otherlinkGroup: ILink[] = [];
		//otherlinkGroup.push({ path: '/admin/dashboards', label: 'Dashboards', icon: 'dashboard', iconPos: 'icon-left', isDisabled: true });
		//otherlinkGroup.push({ path: '/admin/timelines', label: 'Timelines', icon: 'timeline', iconPos: 'icon-left', isDisabled: true });
		if (otherlinkGroup.length > 0) {
			linkGroups.push(otherlinkGroup);
		}

		const bottomlinkGroup: ILink[] = [];
		// bottomlinkGroup.push({ path: '/admin/documentation', label: 'Documentation', icon: 'help', iconPos: 'icon-left', isDisabled: true });
		bottomlinkGroup.push({ path: '/logout', label: 'Logout', icon: 'logout', iconPos: 'icon-left' });
		linkGroups.push(bottomlinkGroup);

		// % protected region % [Modify your link groups here before returning] off begin
		// % protected region % [Modify your link groups here before returning] end

		return linkGroups;
	}

	// % protected region % [Add custom methods here] off begin
	// % protected region % [Add custom methods here] end
}