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
using System.Collections.Generic;
using Lactalis.Security.Acl;

namespace Lactalis.Security
{
	public static class SecurityUtilities
	{
		public static IEnumerable<IAcl> GetAllAcls()
		{
			return new List<IAcl>
			{
				new AdminMilkTestEntity(),
				new AdminNewsArticleEntity(),
				new AdminFarmsEntity(),
				new AdminFarmerEntity(),
				new AdminFarmEntity(),
				new AdminAdminEntity(),
				new FarmerMilkTestEntity(),
				new FarmerNewsArticleEntity(),
				new FarmerFarmsEntity(),
				new FarmerFarmerEntity(),
				new FarmerFarmEntity(),
				new AdminImportantDocumentEntity(),
				new FarmerImportantDocumentEntity(),
				new AdminTradingPostListingEntity(),
				new AdminTradingPostCategoryEntity(),
				new AdminTradingPostCategoriesEntity(),
				new AdminTechnicalDocumentCategoryEntity(),
				new AdminTechnicalDocumentEntity(),
				new AdminSustainabilityPostEntity(),
				new AdminQualityDocumentCategoryEntity(),
				new AdminQualityDocumentEntity(),
				new AdminPromotedArticlesEntity(),
				new AdminImportantDocumentCategoryEntity(),
				new AdminAgriSupplyDocumentCategoryEntity(),
				new AdminAgriSupplyDocumentEntity(),
				new FarmerAgriSupplyDocumentEntity(),
				new FarmerAgriSupplyDocumentCategoryEntity(),
				new FarmerImportantDocumentCategoryEntity(),
				new FarmerQualityDocumentEntity(),
				new FarmerPromotedArticlesEntity(),
				new FarmerSustainabilityPostEntity(),
				new FarmerQualityDocumentCategoryEntity(),
				new FarmerTechnicalDocumentCategoryEntity(),
				new FarmerTechnicalDocumentEntity(),
				new FarmerTradingPostCategoriesEntity(),
				new FarmerTradingPostCategoryEntity(),
				new FarmerTradingPostListingEntity(),
			};
		}
	}
}