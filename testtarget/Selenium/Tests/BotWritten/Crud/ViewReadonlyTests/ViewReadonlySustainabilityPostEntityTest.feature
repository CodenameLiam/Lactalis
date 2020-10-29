
@BotWritten @view
Feature: View readonly SustainabilityPostEntity
	
	@SustainabilityPostEntity
	Scenario: View readonly SustainabilityPostEntity
	Given I login to the site as a user
	And I insert a SustainabilityPostEntity into the database
	And I navigate to the SustainabilityPostEntity backend page
	When I click View on the first row and navigate to the View SustainabilityPostEntity
	Then I assert that the entity input fields  are readonly on the SustainabilityPostEntity page
