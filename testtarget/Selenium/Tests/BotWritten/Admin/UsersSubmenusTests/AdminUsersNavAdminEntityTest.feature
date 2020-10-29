
@BotWritten @view
Feature: Admin Submenu AdminEntity Feature
	
	@AdminEntity
	Scenario: I attempt to navigate to the AdminEntity entity page
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Users Nav link on the Admin Nav section
	And I click the Admin admin submenu
	Then I assert that I am on the Admin Entity backend page
