
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort TradingPostCategoryEntity
	
	@TradingPostCategoryEntity
	Scenario: Sort TradingPostCategoryEntity
	Given I login to the site as a user
	And I navigate to the TradingPostCategoryEntity backend page
	When I sort TradingPostCategoryEntity by Name
	Then I assert that Name in TradingPostCategoryEntity of type String is properly sorted in descending
	When I sort TradingPostCategoryEntity by Name
	Then I assert that Name in TradingPostCategoryEntity of type String is properly sorted in ascending

