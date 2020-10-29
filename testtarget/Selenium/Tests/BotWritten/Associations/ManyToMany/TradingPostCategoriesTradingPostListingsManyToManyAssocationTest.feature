
@BotWritten @associations
Feature: Reference from TradingPostCategoryEntity using Trading Post Categories to TradingPostListingEntity using Trading Post Listings
	Scenario: Reference from TradingPostCategoryEntity using Trading Post Categories to TradingPostListingEntity using Trading Post Listings
	Given I login to the site as a user
	And I navigate to the TradingPostCategoryEntity backend page
	And I create 3 TradingPostCategoryEntity's each associated with 3 TradingPostListings using Trading Post Listings
	Then I validate each TradingPostListingEntity has 3 TradingPostCategoryEntity associations using Trading Post Categories
	Then I validate each TradingPostCategoryEntity has 3 TradingPostListingEntity associations using Trading Post Listings