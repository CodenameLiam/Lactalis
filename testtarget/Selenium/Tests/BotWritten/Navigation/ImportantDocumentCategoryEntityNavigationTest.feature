
@BotWritten @navigation
Feature: ImportantDocumentCategoryEntity Navigation Feature

@ImportantDocumentCategoryEntity
Scenario: Navigate to ImportantDocumentCategoryEntity backend page
Given I login to the site as a user
And I navigate to the ImportantDocumentCategoryEntity backend page
Then I assert that I am on the ImportantDocumentCategoryEntity backend page