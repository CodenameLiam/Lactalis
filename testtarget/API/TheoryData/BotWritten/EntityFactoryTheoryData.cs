
using APITests.Factories;
using Xunit;




namespace APITests.TheoryData.BotWritten
{
	public class UserEntityFactorySingleTheoryData : TheoryData<UserEntityFactory>
	{
		public UserEntityFactorySingleTheoryData()
		{
			Add(new UserEntityFactory("FarmerEntity"));
			Add(new UserEntityFactory("AdminEntity"));
		}
	}

	public class EntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactorySingleTheoryData()
		{
			Add(new EntityFactory("TradingPostListingEntity"), 1);
			Add(new EntityFactory("TradingPostCategoryEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("FarmEntity"), 1);
			Add(new EntityFactory("MilkTestEntity"), 1);
			Add(new EntityFactory("FarmerEntity"), 1);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentEntity"), 1);
			Add(new EntityFactory("ImportantDocumentEntity"), 1);
			Add(new EntityFactory("NewsArticleEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), 1);
			Add(new EntityFactory("SustainabilityPostEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), 1);
			Add(new EntityFactory("PromotedArticlesEntity"), 1);
		}
	}

	public class NonUserEntityFactorySingleTheoryData : TheoryData<EntityFactory, int>
	{
		public NonUserEntityFactorySingleTheoryData()
		{
			Add(new EntityFactory("TradingPostListingEntity"), 1);
			Add(new EntityFactory("TradingPostCategoryEntity"), 1);
			Add(new EntityFactory("AdminEntity"), 1);
			Add(new EntityFactory("FarmEntity"), 1);
			Add(new EntityFactory("MilkTestEntity"), 1);
			Add(new EntityFactory("FarmerEntity"), 1);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), 1);
			Add(new EntityFactory("QualityDocumentEntity"), 1);
			Add(new EntityFactory("TechnicalDocumentEntity"), 1);
			Add(new EntityFactory("ImportantDocumentEntity"), 1);
			Add(new EntityFactory("NewsArticleEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), 1);
			Add(new EntityFactory("SustainabilityPostEntity"), 1);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), 1);
			Add(new EntityFactory("PromotedArticlesEntity"), 1);
		}
	}

	public class EntityFactoryTheoryData : TheoryData<EntityFactory>
	{
		public EntityFactoryTheoryData()
		{
			Add(new EntityFactory("TradingPostListingEntity"));
			Add(new EntityFactory("TradingPostCategoryEntity"));
			Add(new EntityFactory("AdminEntity"));
			Add(new EntityFactory("FarmEntity"));
			Add(new EntityFactory("MilkTestEntity"));
			Add(new EntityFactory("FarmerEntity"));
			Add(new EntityFactory("ImportantDocumentCategoryEntity"));
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"));
			Add(new EntityFactory("QualityDocumentCategoryEntity"));
			Add(new EntityFactory("QualityDocumentEntity"));
			Add(new EntityFactory("TechnicalDocumentEntity"));
			Add(new EntityFactory("ImportantDocumentEntity"));
			Add(new EntityFactory("NewsArticleEntity"));
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"));
			Add(new EntityFactory("SustainabilityPostEntity"));
			Add(new EntityFactory("AgriSupplyDocumentEntity"));
			Add(new EntityFactory("PromotedArticlesEntity"));
		}
	}

	public class EntityFactoryMultipleTheoryData : TheoryData<EntityFactory, int>
	{
		public EntityFactoryMultipleTheoryData()
		{
			var numEntities = 3;
			Add(new EntityFactory("TradingPostListingEntity"), numEntities);
			Add(new EntityFactory("TradingPostCategoryEntity"), numEntities);
			Add(new EntityFactory("FarmEntity"), numEntities);
			Add(new EntityFactory("MilkTestEntity"), numEntities);
			Add(new EntityFactory("ImportantDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("TechnicalDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("QualityDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("QualityDocumentEntity"), numEntities);
			Add(new EntityFactory("TechnicalDocumentEntity"), numEntities);
			Add(new EntityFactory("ImportantDocumentEntity"), numEntities);
			Add(new EntityFactory("NewsArticleEntity"), numEntities);
			Add(new EntityFactory("AgriSupplyDocumentCategoryEntity"), numEntities);
			Add(new EntityFactory("SustainabilityPostEntity"), numEntities);
			Add(new EntityFactory("AgriSupplyDocumentEntity"), numEntities);
			Add(new EntityFactory("PromotedArticlesEntity"), numEntities);
		}
	}


}