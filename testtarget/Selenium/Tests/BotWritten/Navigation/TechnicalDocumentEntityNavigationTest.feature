
@BotWritten @navigation
Feature: TechnicalDocumentEntity Navigation Feature

@TechnicalDocumentEntity
Scenario: Navigate to TechnicalDocumentEntity backend page
Given I login to the site as a user
And I navigate to the TechnicalDocumentEntity backend page
Then I assert that I am on the TechnicalDocumentEntity backend page