
@BotWritten @update
Feature: Update QualityDocumentEntity Feature
	
	@QualityDocumentEntity
	Scenario: Update QualityDocumentEntity Feature
	Given I have 1 valid QualityDocumentEntity entities
	And I login to the site as a user
	And I navigate to the QualityDocumentEntity backend page
	When I edit the first entity row
	And I create a valid QualityDocumentEntity
	Then I assert that I can see a popup displays a message: Successfully edited Quality Document
