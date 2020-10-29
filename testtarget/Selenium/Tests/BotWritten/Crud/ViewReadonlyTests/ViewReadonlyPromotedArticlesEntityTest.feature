
@BotWritten @view
Feature: View readonly PromotedArticlesEntity
	
	@PromotedArticlesEntity
	Scenario: View readonly PromotedArticlesEntity
	Given I login to the site as a user
	And I insert a PromotedArticlesEntity into the database
	And I navigate to the PromotedArticlesEntity backend page
	When I click View on the first row and navigate to the View PromotedArticlesEntity
	Then I assert that the entity input fields  are readonly on the PromotedArticlesEntity page
