
@BotWritten @update
Feature: Update SustainabilityPostEntity Feature
	
	@SustainabilityPostEntity
	Scenario: Update SustainabilityPostEntity Feature
	Given I have 1 valid SustainabilityPostEntity entities
	And I login to the site as a user
	And I navigate to the SustainabilityPostEntity backend page
	When I edit the first entity row
	And I create a valid SustainabilityPostEntity
	Then I assert that I can see a popup displays a message: Successfully edited Sustainability Post
