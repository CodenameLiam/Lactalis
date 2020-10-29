
@BotWritten @delete
Feature: Delete TechnicalDocumentCategoryEntity Feature
	
	@TechnicalDocumentCategoryEntity
	Scenario: I attempt to delete the TechnicalDocumentCategoryEntity entity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentCategoryEntity backend page
	When I insert a valid TechnicalDocumentCategoryEntity, search for it and delete it
	Then I assert that I am on the TechnicalDocumentCategoryEntity backend page
