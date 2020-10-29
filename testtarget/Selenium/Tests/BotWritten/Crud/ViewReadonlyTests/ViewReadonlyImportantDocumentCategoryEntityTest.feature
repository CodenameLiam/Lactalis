
@BotWritten @view
Feature: View readonly ImportantDocumentCategoryEntity
	
	@ImportantDocumentCategoryEntity
	Scenario: View readonly ImportantDocumentCategoryEntity
	Given I login to the site as a user
	And I insert a ImportantDocumentCategoryEntity into the database
	And I navigate to the ImportantDocumentCategoryEntity backend page
	When I click View on the first row and navigate to the View ImportantDocumentCategoryEntity
	Then I assert that the entity input fields  are readonly on the ImportantDocumentCategoryEntity page
