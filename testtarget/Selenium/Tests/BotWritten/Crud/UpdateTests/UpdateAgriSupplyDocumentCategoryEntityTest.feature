
@BotWritten @update
Feature: Update AgriSupplyDocumentCategoryEntity Feature
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: Update AgriSupplyDocumentCategoryEntity Feature
	Given I have 1 valid AgriSupplyDocumentCategoryEntity entities
	And I login to the site as a user
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	When I edit the first entity row
	And I create a valid AgriSupplyDocumentCategoryEntity
	Then I assert that I can see a popup displays a message: Successfully edited Agri Supply Document Category
