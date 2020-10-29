
@BotWritten @view
Feature: View readonly MilkTestEntity
	
	@MilkTestEntity
	Scenario: View readonly MilkTestEntity
	Given I login to the site as a user
	And I insert a MilkTestEntity into the database
	And I navigate to the MilkTestEntity backend page
	When I click View on the first row and navigate to the View MilkTestEntity
	Then I assert that the entity input fields  are readonly on the MilkTestEntity page
