
@BotWritten @create
Feature: Create QualityDocumentEntity
	
	@QualityDocumentEntity
	Scenario: Create QualityDocumentEntity
	Given I login to the site as a user
	And I navigate to the QualityDocumentEntity backend page
	And I click to create a QualityDocumentEntity
	When I create a valid QualityDocumentEntity
	Then I assert that I am on the QualityDocumentEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Quality Document
