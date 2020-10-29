
@BotWritten @create
Feature: Create AgriSupplyDocumentEntity
	
	@AgriSupplyDocumentEntity
	Scenario: Create AgriSupplyDocumentEntity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentEntity backend page
	And I click to create a AgriSupplyDocumentEntity
	When I create a valid AgriSupplyDocumentEntity
	Then I assert that I am on the AgriSupplyDocumentEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Agri Supply Document
