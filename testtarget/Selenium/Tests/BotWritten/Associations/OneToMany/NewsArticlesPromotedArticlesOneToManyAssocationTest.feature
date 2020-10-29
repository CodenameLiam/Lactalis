
@BotWritten @associations
Feature: Reference from NewsArticleEntity using News Articles to PromotedArticlesEntity using Promoted Articles
	Scenario: Reference from NewsArticleEntity using News Articles to PromotedArticlesEntity using Promoted Articles
	Given I login to the site as a user
	And I navigate to the NewsArticleEntity backend page
	And I create 3 NewsArticleEntity's each associated with 1 PromotedArticles using Promoted Articles
	Then I validate each PromotedArticlesEntity has 3 NewsArticleEntity associations using News Articles
	Then I validate each NewsArticleEntity has 1 PromotedArticlesEntity associations using Promoted Articles