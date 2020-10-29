
@BotWritten @delete
Feature: Delete NewsArticleEntity Feature
	
	@NewsArticleEntity
	Scenario: I attempt to delete the NewsArticleEntity entity
	Given I login to the site as a user
	And I navigate to the NewsArticleEntity backend page
	When I insert a valid NewsArticleEntity, search for it and delete it
	Then I assert that I am on the NewsArticleEntity backend page
