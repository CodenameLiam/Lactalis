
@BotWritten @create
Feature: Create ImportantDocumentCategoryEntity
	
	@ImportantDocumentCategoryEntity
	Scenario: Create ImportantDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentCategoryEntity backend page
	And I click to create a ImportantDocumentCategoryEntity
	When I create a valid ImportantDocumentCategoryEntity
	Then I assert that I am on the ImportantDocumentCategoryEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Important Document Category
