
@BotWritten @navigation
Feature: TradingPostCategoryEntity Navigation Feature

@TradingPostCategoryEntity
Scenario: Navigate to TradingPostCategoryEntity backend page
Given I login to the site as a user
And I navigate to the TradingPostCategoryEntity backend page
Then I assert that I am on the TradingPostCategoryEntity backend page