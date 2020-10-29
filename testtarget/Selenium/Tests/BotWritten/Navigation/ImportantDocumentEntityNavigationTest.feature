
@BotWritten @navigation
Feature: ImportantDocumentEntity Navigation Feature

@ImportantDocumentEntity
Scenario: Navigate to ImportantDocumentEntity backend page
Given I login to the site as a user
And I navigate to the ImportantDocumentEntity backend page
Then I assert that I am on the ImportantDocumentEntity backend page