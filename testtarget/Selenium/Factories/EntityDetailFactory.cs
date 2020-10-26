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
				"QualityDocumentCategoryEntity" => new QualityDocumentCategoryEntityDetailSection(_contextConfiguration, (QualityDocumentCategoryEntity) entity),
				"TechnicalDocumentCategoryEntity" => new TechnicalDocumentCategoryEntityDetailSection(_contextConfiguration, (TechnicalDocumentCategoryEntity) entity),
				"QualityDocumentEntity" => new QualityDocumentEntityDetailSection(_contextConfiguration, (QualityDocumentEntity) entity),
				"TechnicalDocumentEntity" => new TechnicalDocumentEntityDetailSection(_contextConfiguration, (TechnicalDocumentEntity) entity),
				"ImportantDocumentEntity" => new ImportantDocumentEntityDetailSection(_contextConfiguration, (ImportantDocumentEntity) entity),
				"NewsArticleEntity" => new NewsArticleEntityDetailSection(_contextConfiguration, (NewsArticleEntity) entity),
				"PromotedArticlesEntity" => new PromotedArticlesEntityDetailSection(_contextConfiguration, (PromotedArticlesEntity) entity),
				"AgriSupplyDocumentCategoryEntity" => new AgriSupplyDocumentCategoryEntityDetailSection(_contextConfiguration, (AgriSupplyDocumentCategoryEntity) entity),
				"SustainabilityPostEntity" => new SustainabilityPostEntityDetailSection(_contextConfiguration, (SustainabilityPostEntity) entity),
				"AgriSupplyDocumentEntity" => new AgriSupplyDocumentEntityDetailSection(_contextConfiguration, (AgriSupplyDocumentEntity) entity),
				_ => throw new Exception($"Cannot find entity type {entityName}"),
			};
		}
	}
}
