
@BotWritten @create
Feature: Create NewsArticleEntity
	
	@NewsArticleEntity
	Scenario: Create NewsArticleEntity
	Given I login to the site as a user
	And I navigate to the NewsArticleEntity backend page
	And I click to create a NewsArticleEntity
	When I create a valid NewsArticleEntity
	Then I assert that I am on the NewsArticleEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added News Article
