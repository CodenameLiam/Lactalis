
@BotWritten @create
Feature: Create AgriSupplyDocumentCategoryEntity
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: Create AgriSupplyDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	And I click to create a AgriSupplyDocumentCategoryEntity
	When I create a valid AgriSupplyDocumentCategoryEntity
	Then I assert that I am on the AgriSupplyDocumentCategoryEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Agri Supply Document Category
