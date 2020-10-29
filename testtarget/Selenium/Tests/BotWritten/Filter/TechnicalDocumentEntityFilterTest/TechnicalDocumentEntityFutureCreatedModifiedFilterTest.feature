

@BotWritten @Filter
Feature: TechnicalDocumentEntity filtered by future's created and modifed and search Feature

@TechnicalDocumentEntity
Scenario: TechnicalDocumentEntity filtered by future's created and modifed and search
Given I login to the site as a user
Given I have 10 valid TechnicalDocumentEntity entities
And I have 1 valid TechnicalDocumentEntity entities with fixed string values BlaBla_StringToSearch_BlaBla
And I navigate to the TechnicalDocumentEntity backend page
When I click the filter Button on the TechnicalDocumentEntity page
Then The filter panel shows up with correct information
When I enter the string StringToSearch to search and click filter button
Then The string StringToSearch is in each row of the the collection content
When I enter the created date filter starting in 7 days
And I apply the current filter
Then No row is within the applied current date range filters