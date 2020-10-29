

@BotWritten @Filter
Feature: AgriSupplyDocumentEntity filtered by today's created Feature

@AgriSupplyDocumentEntity
Scenario: AgriSupplyDocumentEntity filtered by today's created
Given I login to the site as a user
Given I have 10 valid AgriSupplyDocumentEntity entities
And I navigate to the AgriSupplyDocumentEntity backend page
When I click the filter Button on the AgriSupplyDocumentEntity page
Then The filter panel shows up with correct information
When I enter the created date filter starting from 1 days ago
And I apply the current filter
Then Each row has been created within the last 1 days