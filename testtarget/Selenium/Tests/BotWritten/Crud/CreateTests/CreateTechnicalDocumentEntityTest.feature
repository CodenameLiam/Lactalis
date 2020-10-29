
@BotWritten @create
Feature: Create TechnicalDocumentEntity
	
	@TechnicalDocumentEntity
	Scenario: Create TechnicalDocumentEntity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	And I click to create a TechnicalDocumentEntity
	When I create a valid TechnicalDocumentEntity
	Then I assert that I am on the TechnicalDocumentEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Technical Document
