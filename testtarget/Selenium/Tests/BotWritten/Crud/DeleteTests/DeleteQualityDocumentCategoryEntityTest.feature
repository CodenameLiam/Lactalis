
@BotWritten @delete
Feature: Delete QualityDocumentCategoryEntity Feature
	
	@QualityDocumentCategoryEntity
	Scenario: I attempt to delete the QualityDocumentCategoryEntity entity
	Given I login to the site as a user
	And I navigate to the QualityDocumentCategoryEntity backend page
	When I insert a valid QualityDocumentCategoryEntity, search for it and delete it
	Then I assert that I am on the QualityDocumentCategoryEntity backend page
