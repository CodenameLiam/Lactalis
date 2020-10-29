

@BotWritten @Filter
Feature: TradingPostCategoryEntity filtered by today's created Feature

@TradingPostCategoryEntity
Scenario: TradingPostCategoryEntity filtered by today's created
Given I login to the site as a user
Given I have 10 valid TradingPostCategoryEntity entities
And I navigate to the TradingPostCategoryEntity backend page
When I click the filter Button on the TradingPostCategoryEntity page
Then The filter panel shows up with correct information
When I enter the created date filter starting from 1 days ago
And I apply the current filter
Then Each row has been created within the last 1 days