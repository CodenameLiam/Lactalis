

@BotWritten @Filter
Feature: TradingPostListingEntity filtered by enum values

@TradingPostListingEntity
Scenario: TradingPostListingEntity filtered by enum values
Given I login to the site as a user
Given I have 10 valid TradingPostListingEntity entities
And I have 1 valid TradingPostListingEntity entities with fixed string values BlaBla_StringToSearch_BlaBla
And I navigate to the TradingPostListingEntity backend page
When I click the filter Button on the TradingPostListingEntity page
Then The filter panel shows up with correct information
When I enter the enum filter priceType with the same value in the entity just created and click
Then The enum value created for Price Type is in each row of the the collection content
