
@BotWritten @create
Feature: Create FarmerEntity
	
	@FarmerEntity
	Scenario: Create FarmerEntity
	Given I login to the site as a user
	And I navigate to the FarmerEntity backend page
	And I click to create a FarmerEntity
	When I create a valid FarmerEntity
	Then I assert that I am on the FarmerEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Farmer
