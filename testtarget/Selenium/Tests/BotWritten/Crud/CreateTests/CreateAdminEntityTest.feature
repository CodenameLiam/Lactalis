
@BotWritten @create
Feature: Create AdminEntity
	
	@AdminEntity
	Scenario: Create AdminEntity
	Given I login to the site as a user
	And I navigate to the AdminEntity backend page
	And I click to create a AdminEntity
	When I create a valid AdminEntity
	Then I assert that I am on the AdminEntity backend page
	Then I assert that I can see a popup displays a message: Successfully added Admin
