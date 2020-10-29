
@BotWritten @update
Feature: Update TechnicalDocumentCategoryEntity Feature
	
	@TechnicalDocumentCategoryEntity
	Scenario: Update TechnicalDocumentCategoryEntity Feature
	Given I have 1 valid TechnicalDocumentCategoryEntity entities
	And I login to the site as a user
	And I navigate to the TechnicalDocumentCategoryEntity backend page
	When I edit the first entity row
	And I create a valid TechnicalDocumentCategoryEntity
	Then I assert that I can see a popup displays a message: Successfully edited Technical Document Category
