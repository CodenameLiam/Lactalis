
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort TradingPostListingEntity
	
	@TradingPostListingEntity
	Scenario: Sort TradingPostListingEntity
	Given I login to the site as a user
	And I navigate to the TradingPostListingEntity backend page
	When I sort TradingPostListingEntity by Title
	Then I assert that Title in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Title
	Then I assert that Title in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Email
	Then I assert that Email in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Email
	Then I assert that Email in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Phone
	Then I assert that Phone in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Phone
	Then I assert that Phone in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Additional Info
	Then I assert that Additional Info in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Additional Info
	Then I assert that Additional Info in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Address Line 1
	Then I assert that Address Line 1 in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Address Line 1
	Then I assert that Address Line 1 in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Address Line 2
	Then I assert that Address Line 2 in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Address Line 2
	Then I assert that Address Line 2 in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Postal Code
	Then I assert that Postal Code in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Postal Code
	Then I assert that Postal Code in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Product Image
	Then I assert that Product Image in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Product Image
	Then I assert that Product Image in TradingPostListingEntity of type String is properly sorted in ascending
	When I sort TradingPostListingEntity by Price
	Then I assert that Price in TradingPostListingEntity of type int is properly sorted in descending
	When I sort TradingPostListingEntity by Price
	Then I assert that Price in TradingPostListingEntity of type int is properly sorted in ascending
	When I sort TradingPostListingEntity by Price Type
	Then I assert that Price Type in TradingPostListingEntity of type String is properly sorted in descending
	When I sort TradingPostListingEntity by Price Type
	Then I assert that Price Type in TradingPostListingEntity of type String is properly sorted in ascending

