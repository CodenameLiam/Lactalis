
@BotWritten @create
Feature: Create TechnicalDocumentCategoryEntity
	
	@TechnicalDocumentCategoryEntity
	Scenario: Create TechnicalDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentCategoryEntity backend page
	And I click to create a TechnicalDocumentCategoryEntity
	When I create a valid TechnicalDocumentCategoryEntity
	Then I assert that I am on the TechnicalDocumentCategoryEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Technical Document Category
