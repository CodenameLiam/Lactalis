
@BotWritten @update
Feature: Update AgriSupplyDocumentEntity Feature
	
	@AgriSupplyDocumentEntity
	Scenario: Update AgriSupplyDocumentEntity Feature
	Given I have 1 valid AgriSupplyDocumentEntity entities
	And I login to the site as a user
	And I navigate to the AgriSupplyDocumentEntity backend page
	When I edit the first entity row
	And I create a valid AgriSupplyDocumentEntity
	Then I assert that I can see a popup displays a message: Successfully edited Agri Supply Document
