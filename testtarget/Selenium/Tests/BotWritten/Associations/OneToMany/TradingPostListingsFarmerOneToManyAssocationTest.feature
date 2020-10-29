
@BotWritten @associations
Feature: Reference from TradingPostListingEntity using Trading Post Listings to FarmerEntity using Farmer
	Scenario: Reference from TradingPostListingEntity using Trading Post Listings to FarmerEntity using Farmer
	Given I login to the site as a user
	And I navigate to the TradingPostListingEntity backend page
	And I create 3 TradingPostListingEntity's each associated with 1 Farmer using Farmer
	Then I validate each FarmerEntity has 3 TradingPostListingEntity associations using Trading Post Listings
	Then I validate each TradingPostListingEntity has 1 FarmerEntity associations using Farmer