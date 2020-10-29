
@BotWritten @create
Feature: Create TradingPostListingEntity
	
	@TradingPostListingEntity
	Scenario: Create TradingPostListingEntity
	Given I login to the site as a user
	And I navigate to the TradingPostListingEntity backend page
	And I click to create a TradingPostListingEntity
	When I create a valid TradingPostListingEntity
	Then I assert that I am on the TradingPostListingEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Trading Post Listing
