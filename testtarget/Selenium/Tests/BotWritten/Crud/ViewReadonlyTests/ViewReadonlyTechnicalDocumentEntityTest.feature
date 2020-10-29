
@BotWritten @view
Feature: View readonly TechnicalDocumentEntity
	
	@TechnicalDocumentEntity
	Scenario: View readonly TechnicalDocumentEntity
	Given I login to the site as a user
	And I insert a TechnicalDocumentEntity into the database
	And I navigate to the TechnicalDocumentEntity backend page
	When I click View on the first row and navigate to the View TechnicalDocumentEntity
	Then I assert that the entity input fields  are readonly on the TechnicalDocumentEntity page
