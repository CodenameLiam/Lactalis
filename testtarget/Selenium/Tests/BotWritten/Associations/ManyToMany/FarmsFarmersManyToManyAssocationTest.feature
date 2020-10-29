
@BotWritten @associations
Feature: Reference from FarmEntity using Farms to FarmerEntity using Farmers
	Scenario: Reference from FarmEntity using Farms to FarmerEntity using Farmers
	Given I login to the site as a user
	And I navigate to the FarmEntity backend page
	And I create 3 FarmEntity's each associated with 3 Farmers using Farmers
	Then I validate each FarmerEntity has 3 FarmEntity associations using Farms
	Then I validate each FarmEntity has 3 FarmerEntity associations using Farmers