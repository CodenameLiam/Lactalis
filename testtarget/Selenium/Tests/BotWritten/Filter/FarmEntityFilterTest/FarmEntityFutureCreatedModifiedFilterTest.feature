

@BotWritten @Filter
Feature: FarmEntity filtered by future's created and modifed and search Feature

@FarmEntity
Scenario: FarmEntity filtered by future's created and modifed and search
Given I login to the site as a user
Given I have 10 valid FarmEntity entities
And I have 1 valid FarmEntity entities with fixed string values BlaBla_StringToSearch_BlaBla
And I navigate to the FarmEntity backend page
When I click the filter Button on the FarmEntity page
Then The filter panel shows up with correct information
When I enter the string StringToSearch to search and click filter button
Then The string StringToSearch is in each row of the the collection content
When I enter the created date filter starting in 7 days
And I apply the current filter
Then No row is within the applied current date range filters