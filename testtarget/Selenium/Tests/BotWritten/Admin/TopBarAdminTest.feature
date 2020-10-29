
@BotWritten @admin
Feature: Admin Topbar

Scenario: Admin Topbar
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Admin