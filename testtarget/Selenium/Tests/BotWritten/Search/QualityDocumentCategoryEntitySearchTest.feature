
@BotWritten @search
Feature: QualityDocumentCategoryEntity Search

@QualityDocumentCategoryEntity
Scenario: QualityDocumentCategoryEntity Search
Given I login to the site as a user
And I navigate to the QualityDocumentCategoryEntity backend page
When I insert a valid QualityDocumentCategoryEntity, search for it and delete it