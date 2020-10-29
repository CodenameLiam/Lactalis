
@BotWritten @update
Feature: Update AdminEntity Feature
	
	@AdminEntity
	Scenario: Update AdminEntity Feature
	Given I have 1 valid AdminEntity entities
	And I login to the site as a user
	And I navigate to the AdminEntity backend page
	When I edit the first entity row
	And I create a valid AdminEntity
	Then I assert that I can see a popup displays a message: Successfully edited Admin
