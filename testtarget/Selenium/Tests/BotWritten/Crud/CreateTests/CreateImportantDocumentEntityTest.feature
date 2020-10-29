
@BotWritten @create
Feature: Create ImportantDocumentEntity
	
	@ImportantDocumentEntity
	Scenario: Create ImportantDocumentEntity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	And I click to create a ImportantDocumentEntity
	When I create a valid ImportantDocumentEntity
	Then I assert that I am on the ImportantDocumentEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Important Document
