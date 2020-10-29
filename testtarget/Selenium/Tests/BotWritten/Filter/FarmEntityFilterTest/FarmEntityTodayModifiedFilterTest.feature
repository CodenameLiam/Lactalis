

@BotWritten @Filter
Feature: FarmEntity filtered by today's modified Feature

@FarmEntity
Scenario: FarmEntity filtered by today's modified
Given I login to the site as a user
Given I have 10 valid FarmEntity entities
And I navigate to the FarmEntity backend page
When I click the filter Button on the FarmEntity page
Then The filter panel shows up with correct information
When I enter the modified date filter starting from 1 days ago
And I apply the current filter
Then Each row has been modified within the last 1 days
