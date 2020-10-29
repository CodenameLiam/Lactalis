
@BotWritten @allUserList
Feature: All User List Test
	Scenario: Verify contents of all user page
	Given I login to the site as a user
	And I navigate to the all user page
	Then I verify the contents of the All User page