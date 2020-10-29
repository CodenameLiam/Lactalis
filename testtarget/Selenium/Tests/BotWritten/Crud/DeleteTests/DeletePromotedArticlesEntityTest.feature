
@BotWritten @delete
Feature: Delete PromotedArticlesEntity Feature
	
	@PromotedArticlesEntity
	Scenario: I attempt to delete the PromotedArticlesEntity entity
	Given I login to the site as a user
	And I navigate to the PromotedArticlesEntity backend page
	When I insert a valid PromotedArticlesEntity, search for it and delete it
	Then I assert that I am on the PromotedArticlesEntity backend page
