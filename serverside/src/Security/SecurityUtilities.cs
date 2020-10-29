
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