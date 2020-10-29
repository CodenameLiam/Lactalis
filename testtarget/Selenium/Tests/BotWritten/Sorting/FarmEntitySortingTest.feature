
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort FarmEntity
	
	@FarmEntity
	Scenario: Sort FarmEntity
	Given I login to the site as a user
	And I navigate to the FarmEntity backend page
	When I sort FarmEntity by Code
	Then I assert that Code in FarmEntity of type String is properly sorted in descending
	When I sort FarmEntity by Code
	Then I assert that Code in FarmEntity of type String is properly sorted in ascending
	When I sort FarmEntity by Name
	Then I assert that Name in FarmEntity of type String is properly sorted in descending
	When I sort FarmEntity by Name
	Then I assert that Name in FarmEntity of type String is properly sorted in ascending
	When I sort FarmEntity by State
	Then I assert that State in FarmEntity of type String is properly sorted in descending
	When I sort FarmEntity by State
	Then I assert that State in FarmEntity of type String is properly sorted in ascending

