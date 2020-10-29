
@BotWritten @create
Feature: Create QualityDocumentCategoryEntity
	
	@QualityDocumentCategoryEntity
	Scenario: Create QualityDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the QualityDocumentCategoryEntity backend page
	And I click to create a QualityDocumentCategoryEntity
	When I create a valid QualityDocumentCategoryEntity
	Then I assert that I am on the QualityDocumentCategoryEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Quality Document Category
