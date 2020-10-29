
@BotWritten @update
Feature: Update TechnicalDocumentEntity Feature
	
	@TechnicalDocumentEntity
	Scenario: Update TechnicalDocumentEntity Feature
	Given I have 1 valid TechnicalDocumentEntity entities
	And I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	When I edit the first entity row
	And I create a valid TechnicalDocumentEntity
	Then I assert that I can see a popup displays a message: Successfully edited Technical Document
