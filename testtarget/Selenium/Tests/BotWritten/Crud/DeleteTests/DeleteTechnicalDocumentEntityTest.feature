
@BotWritten @delete
Feature: Delete TechnicalDocumentEntity Feature
	
	@TechnicalDocumentEntity
	Scenario: I attempt to delete the TechnicalDocumentEntity entity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	When I insert a valid TechnicalDocumentEntity, search for it and delete it
	Then I assert that I am on the TechnicalDocumentEntity backend page
