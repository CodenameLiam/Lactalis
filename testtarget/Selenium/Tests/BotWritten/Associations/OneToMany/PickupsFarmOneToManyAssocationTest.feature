
@BotWritten @associations
Feature: Reference from MilkTestEntity using Pickups to FarmEntity using Farm
	Scenario: Reference from MilkTestEntity using Pickups to FarmEntity using Farm
	Given I login to the site as a user
	And I navigate to the MilkTestEntity backend page
	And I create 3 MilkTestEntity's each associated with 1 Farm using Farm
	Then I validate each FarmEntity has 3 MilkTestEntity associations using Pickups
	Then I validate each MilkTestEntity has 1 FarmEntity associations using Farm