
@BotWritten @search
Feature: TradingPostListingEntity Search

@TradingPostListingEntity
Scenario: TradingPostListingEntity Search
Given I login to the site as a user
And I navigate to the TradingPostListingEntity backend page
When I insert a valid TradingPostListingEntity, search for it and delete it