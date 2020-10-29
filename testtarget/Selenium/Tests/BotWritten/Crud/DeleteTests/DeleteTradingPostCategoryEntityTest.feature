
@BotWritten @delete
Feature: Delete TradingPostCategoryEntity Feature
	
	@TradingPostCategoryEntity
	Scenario: I attempt to delete the TradingPostCategoryEntity entity
	Given I login to the site as a user
	And I navigate to the TradingPostCategoryEntity backend page
	When I insert a valid TradingPostCategoryEntity, search for it and delete it
	Then I assert that I am on the TradingPostCategoryEntity backend page
