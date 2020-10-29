
@BotWritten @create
Feature: Create MilkTestEntity
	
	@MilkTestEntity
	Scenario: Create MilkTestEntity
	Given I login to the site as a user
	And I navigate to the MilkTestEntity backend page
	And I click to create a MilkTestEntity
	When I create a valid MilkTestEntity
	Then I assert that I am on the MilkTestEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Milk Test
