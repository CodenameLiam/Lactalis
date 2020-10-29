
@BotWritten @update
Feature: Update ImportantDocumentEntity Feature
	
	@ImportantDocumentEntity
	Scenario: Update ImportantDocumentEntity Feature
	Given I have 1 valid ImportantDocumentEntity entities
	And I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	When I edit the first entity row
	And I create a valid ImportantDocumentEntity
	Then I assert that I can see a popup displays a message: Successfully edited Important Document
