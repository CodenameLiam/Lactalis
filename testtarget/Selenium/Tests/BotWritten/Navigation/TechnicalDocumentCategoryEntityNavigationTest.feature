
@BotWritten @navigation
Feature: TechnicalDocumentCategoryEntity Navigation Feature

@TechnicalDocumentCategoryEntity
Scenario: Navigate to TechnicalDocumentCategoryEntity backend page
Given I login to the site as a user
And I navigate to the TechnicalDocumentCategoryEntity backend page
Then I assert that I am on the TechnicalDocumentCategoryEntity backend page