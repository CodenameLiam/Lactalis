
@BotWritten @create
Feature: Create SustainabilityPostEntity
	
	@SustainabilityPostEntity
	Scenario: Create SustainabilityPostEntity
	Given I login to the site as a user
	And I navigate to the SustainabilityPostEntity backend page
	And I click to create a SustainabilityPostEntity
	When I create a valid SustainabilityPostEntity
	Then I assert that I am on the SustainabilityPostEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Sustainability Post
