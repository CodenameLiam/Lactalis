
@BotWritten @navigation
Feature: QualityDocumentCategoryEntity Navigation Feature

@QualityDocumentCategoryEntity
Scenario: Navigate to QualityDocumentCategoryEntity backend page
Given I login to the site as a user
And I navigate to the QualityDocumentCategoryEntity backend page
Then I assert that I am on the QualityDocumentCategoryEntity backend page