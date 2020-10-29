
@BotWritten @update
Feature: Update TradingPostListingEntity Feature
	
	@TradingPostListingEntity
	Scenario: Update TradingPostListingEntity Feature
	Given I have 1 valid TradingPostListingEntity entities
	And I login to the site as a user
	And I navigate to the TradingPostListingEntity backend page
	When I edit the first entity row
	And I create a valid TradingPostListingEntity
	Then I assert that I can see a popup displays a message: Successfully edited Trading Post Listing
