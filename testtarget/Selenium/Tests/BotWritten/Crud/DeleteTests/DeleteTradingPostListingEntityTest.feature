
@BotWritten @delete
Feature: Delete TradingPostListingEntity Feature
	
	@TradingPostListingEntity
	Scenario: I attempt to delete the TradingPostListingEntity entity
	Given I login to the site as a user
	And I navigate to the TradingPostListingEntity backend page
	When I insert a valid TradingPostListingEntity, search for it and delete it
	Then I assert that I am on the TradingPostListingEntity backend page
