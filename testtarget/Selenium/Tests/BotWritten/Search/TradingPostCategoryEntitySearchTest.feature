
@BotWritten @search
Feature: TradingPostCategoryEntity Search

@TradingPostCategoryEntity
Scenario: TradingPostCategoryEntity Search
Given I login to the site as a user
And I navigate to the TradingPostCategoryEntity backend page
When I insert a valid TradingPostCategoryEntity, search for it and delete it