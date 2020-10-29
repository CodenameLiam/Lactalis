
@BotWritten @delete
Feature: Delete AdminEntity Feature
	
	@AdminEntity
	Scenario: I attempt to delete the AdminEntity entity
	Given I login to the site as a user
	And I navigate to the AdminEntity backend page
	When I insert a valid AdminEntity, search for it and delete it
	Then I assert that I am on the AdminEntity backend page
