
@BotWritten @update
Feature: Update NewsArticleEntity Feature
	
	@NewsArticleEntity
	Scenario: Update NewsArticleEntity Feature
	Given I have 1 valid NewsArticleEntity entities
	And I login to the site as a user
	And I navigate to the NewsArticleEntity backend page
	When I edit the first entity row
	And I create a valid NewsArticleEntity
	Then I assert that I can see a popup displays a message: Successfully edited News Article
