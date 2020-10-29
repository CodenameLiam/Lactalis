
@BotWritten @delete
Feature: Delete SustainabilityPostEntity Feature
	
	@SustainabilityPostEntity
	Scenario: I attempt to delete the SustainabilityPostEntity entity
	Given I login to the site as a user
	And I navigate to the SustainabilityPostEntity backend page
	When I insert a valid SustainabilityPostEntity, search for it and delete it
	Then I assert that I am on the SustainabilityPostEntity backend page
