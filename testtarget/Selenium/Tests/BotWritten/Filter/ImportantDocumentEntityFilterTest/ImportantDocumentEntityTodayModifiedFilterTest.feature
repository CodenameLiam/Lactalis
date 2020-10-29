

@BotWritten @Filter
Feature: ImportantDocumentEntity filtered by today's modified Feature

@ImportantDocumentEntity
Scenario: ImportantDocumentEntity filtered by today's modified
Given I login to the site as a user
Given I have 10 valid ImportantDocumentEntity entities
And I navigate to the ImportantDocumentEntity backend page
When I click the filter Button on the ImportantDocumentEntity page
Then The filter panel shows up with correct information
When I enter the modified date filter starting from 1 days ago
And I apply the current filter
Then Each row has been modified within the last 1 days
