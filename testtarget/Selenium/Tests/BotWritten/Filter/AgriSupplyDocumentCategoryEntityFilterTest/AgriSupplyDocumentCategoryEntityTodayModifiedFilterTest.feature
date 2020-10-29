

@BotWritten @Filter
Feature: AgriSupplyDocumentCategoryEntity filtered by today's modified Feature

@AgriSupplyDocumentCategoryEntity
Scenario: AgriSupplyDocumentCategoryEntity filtered by today's modified
Given I login to the site as a user
Given I have 10 valid AgriSupplyDocumentCategoryEntity entities
And I navigate to the AgriSupplyDocumentCategoryEntity backend page
When I click the filter Button on the AgriSupplyDocumentCategoryEntity page
Then The filter panel shows up with correct information
When I enter the modified date filter starting from 1 days ago
And I apply the current filter
Then Each row has been modified within the last 1 days
