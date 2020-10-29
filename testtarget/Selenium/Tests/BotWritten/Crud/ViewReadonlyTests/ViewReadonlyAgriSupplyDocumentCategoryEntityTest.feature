
@BotWritten @view
Feature: View readonly AgriSupplyDocumentCategoryEntity
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: View readonly AgriSupplyDocumentCategoryEntity
	Given I login to the site as a user
	And I insert a AgriSupplyDocumentCategoryEntity into the database
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	When I click View on the first row and navigate to the View AgriSupplyDocumentCategoryEntity
	Then I assert that the entity input fields  are readonly on the AgriSupplyDocumentCategoryEntity page
