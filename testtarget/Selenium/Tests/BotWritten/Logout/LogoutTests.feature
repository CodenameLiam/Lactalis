

@logout @BotWritten
Feature: Logout via url

	Scenario: Logout via url
	Given I login to the site as a user
		And I assert that the admin bar is on the Admin
	When I am logged out of the site
	Then I am redirected to the login page

# %  ion % [Add any additional tests here] end