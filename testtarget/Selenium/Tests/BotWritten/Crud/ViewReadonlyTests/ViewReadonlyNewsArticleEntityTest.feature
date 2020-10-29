
@BotWritten @view
Feature: View readonly NewsArticleEntity
	
	@NewsArticleEntity
	Scenario: View readonly NewsArticleEntity
	Given I login to the site as a user
	And I insert a NewsArticleEntity into the database
	And I navigate to the NewsArticleEntity backend page
	When I click View on the first row and navigate to the View NewsArticleEntity
	Then I assert that the entity input fields  are readonly on the NewsArticleEntity page
