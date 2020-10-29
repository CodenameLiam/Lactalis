
@BotWritten @view
Feature: View readonly QualityDocumentEntity
	
	@QualityDocumentEntity
	Scenario: View readonly QualityDocumentEntity
	Given I login to the site as a user
	And I insert a QualityDocumentEntity into the database
	And I navigate to the QualityDocumentEntity backend page
	When I click View on the first row and navigate to the View QualityDocumentEntity
	Then I assert that the entity input fields  are readonly on the QualityDocumentEntity page
