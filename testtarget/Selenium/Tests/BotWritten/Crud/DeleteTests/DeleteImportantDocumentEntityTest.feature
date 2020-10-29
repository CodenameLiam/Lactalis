
@BotWritten @delete
Feature: Delete ImportantDocumentEntity Feature
	
	@ImportantDocumentEntity
	Scenario: I attempt to delete the ImportantDocumentEntity entity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	When I insert a valid ImportantDocumentEntity, search for it and delete it
	Then I assert that I am on the ImportantDocumentEntity backend page
