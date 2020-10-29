
@BotWritten @view
Feature: View readonly FarmerEntity
	
	@FarmerEntity
	Scenario: View readonly FarmerEntity
	Given I login to the site as a user
	And I insert a FarmerEntity into the database
	And I navigate to the FarmerEntity backend page
	When I click View on the first row and navigate to the View FarmerEntity
	Then I assert that the entity input fields  are readonly on the FarmerEntity page
