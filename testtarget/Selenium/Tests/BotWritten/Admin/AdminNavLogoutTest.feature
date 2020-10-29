

@BotWritten @logout @admin
Feature: Admin Nav Logout

Scenario: Admin Nav Logout
Given I login to the site as a user
	And I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I am logged out of the site via admin nav link
	Then I am redirected to the login page