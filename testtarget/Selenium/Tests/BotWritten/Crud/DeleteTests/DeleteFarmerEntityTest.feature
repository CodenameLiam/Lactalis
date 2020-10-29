
@BotWritten @delete
Feature: Delete FarmerEntity Feature
	
	@FarmerEntity
	Scenario: I attempt to delete the FarmerEntity entity
	Given I login to the site as a user
	And I navigate to the FarmerEntity backend page
	When I insert a valid FarmerEntity, search for it and delete it
	Then I assert that I am on the FarmerEntity backend page
