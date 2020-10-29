

@BotWritten @nav @admin
Feature: Admin Submenu PromotedArticlesEntity

	
	@PromotedArticlesEntity
	Scenario: Admin Submenu PromotedArticlesEntity
	Given I login to the site as a user
	Then I assert that the admin bar is on the Admin
	When I click on the Topbar Link
	Then I assert that the admin bar is on the Frontend
	When I click on Entities Nav link on the Admin Nav section
	And I click the Promoted Articles admin submenu
	Then I assert that I am on the Promoted Articles Entity backend page