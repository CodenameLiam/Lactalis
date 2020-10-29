
@BotWritten @delete
Feature: Delete FarmEntity Feature
	
	@FarmEntity
	Scenario: I attempt to delete the FarmEntity entity
	Given I login to the site as a user
	And I navigate to the FarmEntity backend page
	When I insert a valid FarmEntity, search for it and delete it
	Then I assert that I am on the FarmEntity backend page
