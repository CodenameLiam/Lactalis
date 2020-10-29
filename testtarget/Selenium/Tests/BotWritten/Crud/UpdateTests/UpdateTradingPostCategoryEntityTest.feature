
@BotWritten @update
Feature: Update TradingPostCategoryEntity Feature
	
	@TradingPostCategoryEntity
	Scenario: Update TradingPostCategoryEntity Feature
	Given I have 1 valid TradingPostCategoryEntity entities
	And I login to the site as a user
	And I navigate to the TradingPostCategoryEntity backend page
	When I edit the first entity row
	And I create a valid TradingPostCategoryEntity
	Then I assert that I can see a popup displays a message: Successfully edited Trading Post Category
