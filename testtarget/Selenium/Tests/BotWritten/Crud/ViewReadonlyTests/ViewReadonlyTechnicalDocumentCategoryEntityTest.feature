
@BotWritten @view
Feature: View readonly TechnicalDocumentCategoryEntity
	
	@TechnicalDocumentCategoryEntity
	Scenario: View readonly TechnicalDocumentCategoryEntity
	Given I login to the site as a user
	And I insert a TechnicalDocumentCategoryEntity into the database
	And I navigate to the TechnicalDocumentCategoryEntity backend page
	When I click View on the first row and navigate to the View TechnicalDocumentCategoryEntity
	Then I assert that the entity input fields  are readonly on the TechnicalDocumentCategoryEntity page
