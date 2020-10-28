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
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace APITests.TheoryData.BotWritten
{
	public class EntityNamePluralizedTheoryData : TheoryData<string>
	{
		public EntityNamePluralizedTheoryData()
		{
			// % protected region % [Modify EntityNamePluralizedTheoryData entities here] off begin
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
			// % protected region % [Modify EntityNamePluralizedTheoryData entities here] end
		}

	}

	public class EntityNameTheoryData : TheoryData<string>
	{
		public EntityNameTheoryData()
		{
			// % protected region % [Modify EntityNameTheoryData entities here] off begin
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
			// % protected region % [Modify EntityNameTheoryData entities here] end
		}

	}
}