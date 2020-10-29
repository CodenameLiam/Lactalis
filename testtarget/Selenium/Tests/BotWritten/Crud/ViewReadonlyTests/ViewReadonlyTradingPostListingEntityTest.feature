
@BotWritten @view
Feature: View readonly TradingPostListingEntity
	
	@TradingPostListingEntity
	Scenario: View readonly TradingPostListingEntity
	Given I login to the site as a user
	And I insert a TradingPostListingEntity into the database
	And I navigate to the TradingPostListingEntity backend page
	When I click View on the first row and navigate to the View TradingPostListingEntity
	Then I assert that the entity input fields  are readonly on the TradingPostListingEntity page
