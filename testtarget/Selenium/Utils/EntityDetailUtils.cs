

using System;
using SeleniumTests.PageObjects.BotWritten;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Utils
{
	internal static class EntityDetailUtils
	{
		public static IDetailSection GetEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				case "TradingPostListingEntity":
					return new TradingPostListingEntityDetailSection(contextConfiguration);
				case "TradingPostCategoryEntity":
					return new TradingPostCategoryEntityDetailSection(contextConfiguration);
				case "AdminEntity":
					return new AdminEntityDetailSection(contextConfiguration);
				case "FarmEntity":
					return new FarmEntityDetailSection(contextConfiguration);
				case "MilkTestEntity":
					return new MilkTestEntityDetailSection(contextConfiguration);
				case "FarmerEntity":
					return new FarmerEntityDetailSection(contextConfiguration);
				case "ImportantDocumentCategoryEntity":
					return new ImportantDocumentCategoryEntityDetailSection(contextConfiguration);
				case "TechnicalDocumentCategoryEntity":
					return new TechnicalDocumentCategoryEntityDetailSection(contextConfiguration);
				case "QualityDocumentCategoryEntity":
					return new QualityDocumentCategoryEntityDetailSection(contextConfiguration);
				case "QualityDocumentEntity":
					return new QualityDocumentEntityDetailSection(contextConfiguration);
				case "TechnicalDocumentEntity":
					return new TechnicalDocumentEntityDetailSection(contextConfiguration);
				case "ImportantDocumentEntity":
					return new ImportantDocumentEntityDetailSection(contextConfiguration);
				case "NewsArticleEntity":
					return new NewsArticleEntityDetailSection(contextConfiguration);
				case "AgriSupplyDocumentCategoryEntity":
					return new AgriSupplyDocumentCategoryEntityDetailSection(contextConfiguration);
				case "SustainabilityPostEntity":
					return new SustainabilityPostEntityDetailSection(contextConfiguration);
				case "AgriSupplyDocumentEntity":
					return new AgriSupplyDocumentEntityDetailSection(contextConfiguration);
				case "PromotedArticlesEntity":
					return new PromotedArticlesEntityDetailSection(contextConfiguration);
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}

		public static WorkflowPage GetWorkflowEntityDetailsSection(string entityName, ContextConfiguration contextConfiguration)
		{
			switch (entityName)
			{
				default:
					throw new Exception($"Cannot find detail section for type {entityName}");
			}
		}
	}
}