
@BotWritten @update
Feature: Update MilkTestEntity Feature
	
	@MilkTestEntity
	Scenario: Update MilkTestEntity Feature
	Given I have 1 valid MilkTestEntity entities
	And I login to the site as a user
	And I navigate to the MilkTestEntity backend page
	When I edit the first entity row
	And I create a valid MilkTestEntity
	Then I assert that I can see a popup displays a message: Successfully edited Milk Test
