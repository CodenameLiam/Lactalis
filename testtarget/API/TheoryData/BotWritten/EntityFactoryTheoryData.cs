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
using APITests.Factories;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace APITests.TheoryData.BotWritten
{
	public class UserEntityFactorySingleTheoryData : TheoryData<UserEntityFactory>
	{
		public UserEntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new UserEntityFactory("FarmerEntity"));
			Add(new UserEntityFactory("AdminEntity"));
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("TradingPostListingEntity"), 1);
			Add(new EntityFactory("TradingPostCategoryEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("FarmEntity"), 1);
			Add(new EntityFactory("MilkTestEntity"), 1);
			Add(new EntityFactory("FarmerEntity"), 1);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentEntity"), 1);
			Add(new EntityFactory("ImportantDocumentEntity"), 1);
			Add(new EntityFactory("NewsArticleEntity"), 1);
			Add(new EntityFactory("PromotedArticlesEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), 1);
			Add(new EntityFactory("SustainabilityPostEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), 1);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class NonUserEntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public NonUserEntityFactorySingleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("TradingPostListingEntity"), 1);
			Add(new EntityFactory("TradingPostCategoryEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("FarmEntity"), 1);
			Add(new EntityFactory("MilkTestEntity"), 1);
			Add(new EntityFactory("FarmerEntity"), 1);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentEntity"), 1);
			Add(new EntityFactory("ImportantDocumentEntity"), 1);
			Add(new EntityFactory("NewsArticleEntity"), 1);
			Add(new EntityFactory("PromotedArticlesEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), 1);
			Add(new EntityFactory("SustainabilityPostEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), 1);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactoryTheoryData : TheoryData<EntityFactory>
	{
		public EntityFactoryTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			Add(new EntityFactory("TradingPostListingEntity"));
			Add(new EntityFactory("TradingPostCategoryEntity"));
			Add(new EntityFactory("AdminEntity"));
			Add(new EntityFactory("FarmEntity"));
			Add(new EntityFactory("MilkTestEntity"));
			Add(new EntityFactory("FarmerEntity"));
			Add(new EntityFactory("ImportantDocumentCategoryEntity"));
			Add(new EntityFactory("QualityDocumentCategoryEntity"));
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"));
			Add(new EntityFactory("QualityDocumentEntity"));
			Add(new EntityFactory("TechnicalDocumentEntity"));
			Add(new EntityFactory("ImportantDocumentEntity"));
			Add(new EntityFactory("NewsArticleEntity"));
			Add(new EntityFactory("PromotedArticlesEntity"));
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"));
			Add(new EntityFactory("SustainabilityPostEntity"));
			Add(new EntityFactory("AgriSupplyDocumentEntity"));
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	public class EntityFactoryMultipleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactoryMultipleTheoryData()
		{
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] off begin
			var numEntities = 3;
			Add(new EntityFactory("TradingPostListingEntity"), numEntities);
			Add(new EntityFactory("TradingPostCategoryEntity"), numEntities);
			Add(new EntityFactory("FarmEntity"), numEntities);
			Add(new EntityFactory("MilkTestEntity"), numEntities);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("QualityDocumentEntity"), numEntities);
			Add(new EntityFactory("TechnicalDocumentEntity"), numEntities);
			Add(new EntityFactory("ImportantDocumentEntity"), numEntities);
			Add(new EntityFactory("NewsArticleEntity"), numEntities);
			Add(new EntityFactory("PromotedArticlesEntity"), numEntities);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("SustainabilityPostEntity"), numEntities);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), numEntities);
			// % protected region % [Modify UserEntityFactorySingleTheoryData entities here] end
		}
	}

	// % protected region % [Add any further custom EntityFactoryTheoryData here] off begin
	// % protected region % [Add any further custom EntityFactoryTheoryData here] end

}