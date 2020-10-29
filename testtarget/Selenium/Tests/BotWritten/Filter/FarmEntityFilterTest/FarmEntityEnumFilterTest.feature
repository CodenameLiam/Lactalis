

@BotWritten @Filter
Feature: FarmEntity filtered by enum values

@FarmEntity
Scenario: FarmEntity filtered by enum values
Given I login to the site as a user
Given I have 10 valid FarmEntity entities
And I have 1 valid FarmEntity entities with fixed string values BlaBla_StringToSearch_BlaBla
And I navigate to the FarmEntity backend page
When I click the filter Button on the FarmEntity page
Then The filter panel shows up with correct information
When I enter the enum filter state with the same value in the entity just created and click
Then The enum value created for State is in each row of the the collection content
