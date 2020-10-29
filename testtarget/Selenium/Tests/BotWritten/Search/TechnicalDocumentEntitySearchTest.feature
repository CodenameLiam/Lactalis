
@BotWritten @search
Feature: TechnicalDocumentEntity Search

@TechnicalDocumentEntity
Scenario: TechnicalDocumentEntity Search
Given I login to the site as a user
And I navigate to the TechnicalDocumentEntity backend page
When I insert a valid TechnicalDocumentEntity, search for it and delete it