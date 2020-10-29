
@BotWritten @view
Feature: View readonly AdminEntity
	
	@AdminEntity
	Scenario: View readonly AdminEntity
	Given I login to the site as a user
	And I insert a AdminEntity into the database
	And I navigate to the AdminEntity backend page
	When I click View on the first row and navigate to the View AdminEntity
	Then I assert that the entity input fields  are readonly on the AdminEntity page
