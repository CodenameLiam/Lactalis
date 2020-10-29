
@BotWritten @create
Feature: Create TradingPostCategoryEntity
	
	@TradingPostCategoryEntity
	Scenario: Create TradingPostCategoryEntity
	Given I login to the site as a user
	And I navigate to the TradingPostCategoryEntity backend page
	And I click to create a TradingPostCategoryEntity
	When I create a valid TradingPostCategoryEntity
	Then I assert that I am on the TradingPostCategoryEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Trading Post Category
