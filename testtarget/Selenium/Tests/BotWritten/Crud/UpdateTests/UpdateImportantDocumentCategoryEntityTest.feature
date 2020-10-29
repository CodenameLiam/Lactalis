
@BotWritten @update
Feature: Update ImportantDocumentCategoryEntity Feature
	
	@ImportantDocumentCategoryEntity
	Scenario: Update ImportantDocumentCategoryEntity Feature
	Given I have 1 valid ImportantDocumentCategoryEntity entities
	And I login to the site as a user
	And I navigate to the ImportantDocumentCategoryEntity backend page
	When I edit the first entity row
	And I create a valid ImportantDocumentCategoryEntity
	Then I assert that I can see a popup displays a message: Successfully edited Important Document Category
