
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort MilkTestEntity
	
	@MilkTestEntity
	Scenario: Sort MilkTestEntity
	Given I login to the site as a user
	And I navigate to the MilkTestEntity backend page
	When I sort MilkTestEntity by Time
	Then I assert that Time in MilkTestEntity of type DateTime is properly sorted in descending
	When I sort MilkTestEntity by Time
	Then I assert that Time in MilkTestEntity of type DateTime is properly sorted in ascending
	When I sort MilkTestEntity by Volume
	Then I assert that Volume in MilkTestEntity of type int is properly sorted in descending
	When I sort MilkTestEntity by Volume
	Then I assert that Volume in MilkTestEntity of type int is properly sorted in ascending
	When I sort MilkTestEntity by Temperature
	Then I assert that Temperature in MilkTestEntity of type double is properly sorted in descending
	When I sort MilkTestEntity by Temperature
	Then I assert that Temperature in MilkTestEntity of type double is properly sorted in ascending
	When I sort MilkTestEntity by Milk Fat
	Then I assert that Milk Fat in MilkTestEntity of type double is properly sorted in descending
	When I sort MilkTestEntity by Milk Fat
	Then I assert that Milk Fat in MilkTestEntity of type double is properly sorted in ascending
	When I sort MilkTestEntity by Protein
	Then I assert that Protein in MilkTestEntity of type double is properly sorted in descending
	When I sort MilkTestEntity by Protein
	Then I assert that Protein in MilkTestEntity of type double is properly sorted in ascending

