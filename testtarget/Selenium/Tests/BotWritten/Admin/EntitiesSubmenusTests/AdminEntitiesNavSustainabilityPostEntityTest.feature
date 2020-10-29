

@BotWritten @nav @admin
Feature: Admin Submenu SustainabilityPostEntity

	
	@SustainabilityPostEntity
	Scenario: Admin Submenu SustainabilityPostEntity
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Entities Nav link on the Admin Nav section
	And I click the Sustainability Post admin submenu
	Then I assert that I am on the Sustainability Post Entity backend page