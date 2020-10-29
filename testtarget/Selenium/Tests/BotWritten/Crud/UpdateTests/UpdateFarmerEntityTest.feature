
@BotWritten @update
Feature: Update FarmerEntity Feature
	
	@FarmerEntity
	Scenario: Update FarmerEntity Feature
	Given I have 1 valid FarmerEntity entities
	And I login to the site as a user
	And I navigate to the FarmerEntity backend page
	When I edit the first entity row
	And I create a valid FarmerEntity
	Then I assert that I can see a popup displays a message: Successfully edited Farmer
