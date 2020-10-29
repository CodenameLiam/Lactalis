

using System;
using APITests.EntityObjects.Models;
using APITests.Factories;
using SeleniumTests.PageObjects.CRUDPageObject.PageDetails;
using SeleniumTests.Setup;

namespace SeleniumTests.Factories
{
	internal class EntityDetailFactory
	{
		private readonly ContextConfiguration _contextConfiguration;

		public EntityDetailFactory(ContextConfiguration contextConfiguration)
		{
			_contextConfiguration = contextConfiguration;
		}

		public BaseEntity ApplyDetails(string entityName, bool isValid)
		{
			var entityFactory = new EntityFactory(entityName);
			var entity = entityFactory.Construct(isValid);
			entity.Configure(BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES);
			CreateDetailSection(entityName, entity).Apply();
			return entity;
		}

		public IEntityDetailSection CreateDetailSection(string entityName, BaseEntity entity = null)
		{
			return entityName switch
			{
				"TradingPostListingEntity" => new TradingPostListingEntityDetailSection(_contextConfiguration, (TradingPostListingEntity) entity),
				"TradingPostCategoryEntity" => new TradingPostCategoryEntityDetailSection(_contextConfiguration, (TradingPostCategoryEntity) entity),
				"AdminEntity" => new AdminEntityDetailSection(_contextConfiguration, (AdminEntity) entity),
				"FarmEntity" => new FarmEntityDetailSection(_contextConfiguration, (FarmEntity) entity),
				"MilkTestEntity" => new MilkTestEntityDetailSection(_contextConfiguration, (MilkTestEntity) entity),
				"FarmerEntity" => new FarmerEntityDetailSection(_contextConfiguration, (FarmerEntity) entity),
				"ImportantDocumentCategoryEntity" => new ImportantDocumentCategoryEntityDetailSection(_contextConfiguration, (ImportantDocumentCategoryEntity) entity),
				"TechnicalDocumentCategoryEntity" => new TechnicalDocumentCategoryEntityDetailSection(_contextConfiguration, (TechnicalDocumentCategoryEntity) entity),
				"QualityDocumentCategoryEntity" => new QualityDocumentCategoryEntityDetailSection(_contextConfiguration, (QualityDocumentCategoryEntity) entity),
				"QualityDocumentEntity" => new QualityDocumentEntityDetailSection(_contextConfiguration, (QualityDocumentEntity) entity),
				"TechnicalDocumentEntity" => new TechnicalDocumentEntityDetailSection(_contextConfiguration, (TechnicalDocumentEntity) entity),
				"ImportantDocumentEntity" => new ImportantDocumentEntityDetailSection(_contextConfiguration, (ImportantDocumentEntity) entity),
				"NewsArticleEntity" => new NewsArticleEntityDetailSection(_contextConfiguration, (NewsArticleEntity) entity),
				"AgriSupplyDocumentCategoryEntity" => new AgriSupplyDocumentCategoryEntityDetailSection(_contextConfiguration, (AgriSupplyDocumentCategoryEntity) entity),
				"SustainabilityPostEntity" => new SustainabilityPostEntityDetailSection(_contextConfiguration, (SustainabilityPostEntity) entity),
				"AgriSupplyDocumentEntity" => new AgriSupplyDocumentEntityDetailSection(_contextConfiguration, (AgriSupplyDocumentEntity) entity),
				"PromotedArticlesEntity" => new PromotedArticlesEntityDetailSection(_contextConfiguration, (PromotedArticlesEntity) entity),
				_ => throw new Exception($"Cannot find entity type {entityName}"),
			};
		}
	}
}
