
@BotWritten @search
Feature: QualityDocumentEntity Search

@QualityDocumentEntity
Scenario: QualityDocumentEntity Search
Given I login to the site as a user
And I navigate to the QualityDocumentEntity backend page
When I insert a valid QualityDocumentEntity, search for it and delete it