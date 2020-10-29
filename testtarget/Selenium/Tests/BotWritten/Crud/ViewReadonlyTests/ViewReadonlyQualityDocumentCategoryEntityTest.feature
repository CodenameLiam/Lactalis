
@BotWritten @view
Feature: View readonly QualityDocumentCategoryEntity
	
	@QualityDocumentCategoryEntity
	Scenario: View readonly QualityDocumentCategoryEntity
	Given I login to the site as a user
	And I insert a QualityDocumentCategoryEntity into the database
	And I navigate to the QualityDocumentCategoryEntity backend page
	When I click View on the first row and navigate to the View QualityDocumentCategoryEntity
	Then I assert that the entity input fields  are readonly on the QualityDocumentCategoryEntity page
