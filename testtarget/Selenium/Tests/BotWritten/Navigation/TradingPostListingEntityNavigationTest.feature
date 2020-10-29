
@BotWritten @navigation
Feature: TradingPostListingEntity Navigation Feature

@TradingPostListingEntity
Scenario: Navigate to TradingPostListingEntity backend page
Given I login to the site as a user
And I navigate to the TradingPostListingEntity backend page
Then I assert that I am on the TradingPostListingEntity backend page