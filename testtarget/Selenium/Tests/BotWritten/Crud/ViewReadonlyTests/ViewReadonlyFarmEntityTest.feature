
@BotWritten @view
Feature: View readonly FarmEntity
	
	@FarmEntity
	Scenario: View readonly FarmEntity
	Given I login to the site as a user
	And I insert a FarmEntity into the database
	And I navigate to the FarmEntity backend page
	When I click View on the first row and navigate to the View FarmEntity
	Then I assert that the entity input fields  are readonly on the FarmEntity page
