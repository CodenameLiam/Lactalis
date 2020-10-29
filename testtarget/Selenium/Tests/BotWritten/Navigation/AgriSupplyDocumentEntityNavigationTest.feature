
@BotWritten @navigation
Feature: AgriSupplyDocumentEntity Navigation Feature

@AgriSupplyDocumentEntity
Scenario: Navigate to AgriSupplyDocumentEntity backend page
Given I login to the site as a user
And I navigate to the AgriSupplyDocumentEntity backend page
Then I assert that I am on the AgriSupplyDocumentEntity backend page