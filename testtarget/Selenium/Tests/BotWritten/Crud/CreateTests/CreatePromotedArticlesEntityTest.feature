
@BotWritten @create
Feature: Create PromotedArticlesEntity
	
	@PromotedArticlesEntity
	Scenario: Create PromotedArticlesEntity
	Given I login to the site as a user
	And I navigate to the PromotedArticlesEntity backend page
	And I click to create a PromotedArticlesEntity
	When I create a valid PromotedArticlesEntity
	Then I assert that I am on the PromotedArticlesEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Promoted Articles
