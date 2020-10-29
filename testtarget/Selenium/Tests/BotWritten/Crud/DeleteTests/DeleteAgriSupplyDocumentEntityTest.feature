
@BotWritten @delete
Feature: Delete AgriSupplyDocumentEntity Feature
	
	@AgriSupplyDocumentEntity
	Scenario: I attempt to delete the AgriSupplyDocumentEntity entity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentEntity backend page
	When I insert a valid AgriSupplyDocumentEntity, search for it and delete it
	Then I assert that I am on the AgriSupplyDocumentEntity backend page
