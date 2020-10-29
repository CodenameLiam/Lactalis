
@BotWritten @view
Feature: View readonly AgriSupplyDocumentEntity
	
	@AgriSupplyDocumentEntity
	Scenario: View readonly AgriSupplyDocumentEntity
	Given I login to the site as a user
	And I insert a AgriSupplyDocumentEntity into the database
	And I navigate to the AgriSupplyDocumentEntity backend page
	When I click View on the first row and navigate to the View AgriSupplyDocumentEntity
	Then I assert that the entity input fields  are readonly on the AgriSupplyDocumentEntity page
