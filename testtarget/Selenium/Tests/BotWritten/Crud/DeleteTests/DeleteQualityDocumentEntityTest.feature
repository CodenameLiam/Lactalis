
@BotWritten @delete
Feature: Delete QualityDocumentEntity Feature
	
	@QualityDocumentEntity
	Scenario: I attempt to delete the QualityDocumentEntity entity
	Given I login to the site as a user
	And I navigate to the QualityDocumentEntity backend page
	When I insert a valid QualityDocumentEntity, search for it and delete it
	Then I assert that I am on the QualityDocumentEntity backend page
