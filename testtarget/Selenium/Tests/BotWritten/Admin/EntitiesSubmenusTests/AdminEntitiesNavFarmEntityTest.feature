

@BotWritten @nav @admin
Feature: Admin Submenu FarmEntity

	
	@FarmEntity
	Scenario: Admin Submenu FarmEntity
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Entities Nav link on the Admin Nav section
	And I click the Farm admin submenu
	Then I assert that I am on the Farm Entity backend page