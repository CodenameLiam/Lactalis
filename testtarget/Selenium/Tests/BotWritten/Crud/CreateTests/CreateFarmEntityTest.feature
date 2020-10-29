
@BotWritten @create
Feature: Create FarmEntity
	
	@FarmEntity
	Scenario: Create FarmEntity
	Given I login to the site as a user
	And I navigate to the FarmEntity backend page
	And I click to create a FarmEntity
	When I create a valid FarmEntity
	Then I assert that I am on the FarmEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Farm
