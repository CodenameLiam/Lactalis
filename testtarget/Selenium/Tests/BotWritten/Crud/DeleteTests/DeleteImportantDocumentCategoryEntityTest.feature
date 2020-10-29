
@BotWritten @delete
Feature: Delete ImportantDocumentCategoryEntity Feature
	
	@ImportantDocumentCategoryEntity
	Scenario: I attempt to delete the ImportantDocumentCategoryEntity entity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentCategoryEntity backend page
	When I insert a valid ImportantDocumentCategoryEntity, search for it and delete it
	Then I assert that I am on the ImportantDocumentCategoryEntity backend page
