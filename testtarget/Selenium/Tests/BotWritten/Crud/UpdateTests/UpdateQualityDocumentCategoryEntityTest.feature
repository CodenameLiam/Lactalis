
@BotWritten @update
Feature: Update QualityDocumentCategoryEntity Feature
	
	@QualityDocumentCategoryEntity
	Scenario: Update QualityDocumentCategoryEntity Feature
	Given I have 1 valid QualityDocumentCategoryEntity entities
	And I login to the site as a user
	And I navigate to the QualityDocumentCategoryEntity backend page
	When I edit the first entity row
	And I create a valid QualityDocumentCategoryEntity
	Then I assert that I can see a popup displays a message: Successfully edited Quality Document Category
