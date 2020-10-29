
@BotWritten @update
Feature: Update FarmEntity Feature
	
	@FarmEntity
	Scenario: Update FarmEntity Feature
	Given I have 1 valid FarmEntity entities
	And I login to the site as a user
	And I navigate to the FarmEntity backend page
	When I edit the first entity row
	And I create a valid FarmEntity
	Then I assert that I can see a popup displays a message: Successfully edited Farm
