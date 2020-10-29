
@BotWritten @view
Feature: View readonly ImportantDocumentEntity
	
	@ImportantDocumentEntity
	Scenario: View readonly ImportantDocumentEntity
	Given I login to the site as a user
	And I insert a ImportantDocumentEntity into the database
	And I navigate to the ImportantDocumentEntity backend page
	When I click View on the first row and navigate to the View ImportantDocumentEntity
	Then I assert that the entity input fields  are readonly on the ImportantDocumentEntity page
