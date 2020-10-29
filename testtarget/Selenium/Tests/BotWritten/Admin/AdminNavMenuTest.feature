

@BotWritten @admin @nav
Feature: Admin Nav Section

Scenario: Navigate to admin landing page
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	Then The Admin Nav Menu is displayed

Scenario: Navigate to home page
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click the home link of the admin nav section
	Then I assert that the admin bar is on the Frontend

Scenario: Verify the number of Admin Submenus
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Users Nav link on the Admin Nav section
	Then I assert that 3 Nav links are displayed
	When I click on Entities Nav link on the Admin Nav section
	Then I assert that 15 Nav links are displayed

Scenario: Verify the admin submenus
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Users Nav link on the Admin Nav section
	Then I see the Admin Submenus like
	| Users |
	| All Users |
	| Admin |
	| Farmer |
	When I click on Entities Nav link on the Admin Nav section
	Then I see the Admin Submenus like
	| Entities |
	| Trading Post Listing |
	| Trading Post Category |
	| Farm |
	| Milk Test |
	| Important Document Category |
	| Technical Document Category |
	| Quality Document Category |
	| Quality Document |
	| Technical Document |
	| Important Document |
	| News Article |
	| Agri Supply Document Category |
	| Sustainability Post |
	| Agri Supply Document |
	| Promoted Articles |
