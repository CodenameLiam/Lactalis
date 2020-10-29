
@BotWritten @update
Feature: Update PromotedArticlesEntity Feature
	
	@PromotedArticlesEntity
	Scenario: Update PromotedArticlesEntity Feature
	Given I have 1 valid PromotedArticlesEntity entities
	And I login to the site as a user
	And I navigate to the PromotedArticlesEntity backend page
	When I edit the first entity row
	And I create a valid PromotedArticlesEntity
	Then I assert that I can see a popup displays a message: Successfully edited Promoted Articles
