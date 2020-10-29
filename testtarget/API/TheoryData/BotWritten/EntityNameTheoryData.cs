
using Xunit;


namespace APITests.TheoryData.BotWritten
{
	public class EntityNamePluralizedTheoryData : TheoryData<string>
	{
		public EntityNamePluralizedTheoryData()
		{
			Add("tradingPostListingEntitys");
			Add("tradingPostCategoryEntitys");
			Add("adminEntitys");
			Add("farmEntitys");
			Add("milkTestEntitys");
			Add("farmerEntitys");
			Add("importantDocumentCategoryEntitys");
			Add("technicalDocumentCategoryEntitys");
			Add("qualityDocumentCategoryEntitys");
			Add("qualityDocumentEntitys");
			Add("technicalDocumentEntitys");
			Add("importantDocumentEntitys");
			Add("newsArticleEntitys");
			Add("agriSupplyDocumentCategoryEntitys");
			Add("sustainabilityPostEntitys");
			Add("agriSupplyDocumentEntitys");
			Add("promotedArticlesEntitys");
		}

	}

	public class EntityNameTheoryData : TheoryData<string>
	{
		public EntityNameTheoryData()
		{
			Add("tradingPostListingEntity");
			Add("tradingPostCategoryEntity");
			Add("adminEntity");
			Add("farmEntity");
			Add("milkTestEntity");
			Add("farmerEntity");
			Add("importantDocumentCategoryEntity");
			Add("technicalDocumentCategoryEntity");
			Add("qualityDocumentCategoryEntity");
			Add("qualityDocumentEntity");
			Add("technicalDocumentEntity");
			Add("importantDocumentEntity");
			Add("newsArticleEntity");
			Add("agriSupplyDocumentCategoryEntity");
			Add("sustainabilityPostEntity");
			Add("agriSupplyDocumentEntity");
			Add("promotedArticlesEntity");
		}

	}
}