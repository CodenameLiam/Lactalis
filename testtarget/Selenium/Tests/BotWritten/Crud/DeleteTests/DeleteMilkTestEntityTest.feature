
@BotWritten @delete
Feature: Delete MilkTestEntity Feature
	
	@MilkTestEntity
	Scenario: I attempt to delete the MilkTestEntity entity
	Given I login to the site as a user
	And I navigate to the MilkTestEntity backend page
	When I insert a valid MilkTestEntity, search for it and delete it
	Then I assert that I am on the MilkTestEntity backend page
