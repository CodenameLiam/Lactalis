
@BotWritten @navigation
Feature: QualityDocumentEntity Navigation Feature

@QualityDocumentEntity
Scenario: Navigate to QualityDocumentEntity backend page
Given I login to the site as a user
And I navigate to the QualityDocumentEntity backend page
Then I assert that I am on the QualityDocumentEntity backend page