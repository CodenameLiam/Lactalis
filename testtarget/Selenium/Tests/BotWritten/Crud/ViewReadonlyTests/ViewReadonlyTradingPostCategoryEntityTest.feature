
@BotWritten @view
Feature: View readonly TradingPostCategoryEntity
	
	@TradingPostCategoryEntity
	Scenario: View readonly TradingPostCategoryEntity
	Given I login to the site as a user
	And I insert a TradingPostCategoryEntity into the database
	And I navigate to the TradingPostCategoryEntity backend page
	When I click View on the first row and navigate to the View TradingPostCategoryEntity
	Then I assert that the entity input fields  are readonly on the TradingPostCategoryEntity page
