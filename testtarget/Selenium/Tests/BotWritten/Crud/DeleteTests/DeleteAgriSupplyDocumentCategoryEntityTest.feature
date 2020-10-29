
@BotWritten @delete
Feature: Delete AgriSupplyDocumentCategoryEntity Feature
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: I attempt to delete the AgriSupplyDocumentCategoryEntity entity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	When I insert a valid AgriSupplyDocumentCategoryEntity, search for it and delete it
	Then I assert that I am on the AgriSupplyDocumentCategoryEntity backend page
