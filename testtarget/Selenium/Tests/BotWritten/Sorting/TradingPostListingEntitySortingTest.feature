###
# @bot-written
# 
# WARNING AND NOTICE
# Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
# Full Software Licence as accepted by you before being granted access to this source code and other materials,
# the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
# commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
# licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
# including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
# access, download, storage, and/or use of this source code.
# 
# BOT WARNING
# This file is bot-written.
# Any changes out side of "protected regions" will be lost next time the bot makes any changes.
###
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

