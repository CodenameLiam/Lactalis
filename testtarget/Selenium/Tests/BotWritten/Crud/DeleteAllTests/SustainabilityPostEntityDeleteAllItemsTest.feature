
@BotWritten @bulkDeleteoptions @xunit:collection(DELETE_ALL)
Feature: Delete all SustainabilityPostEntity on all pages
	
	@SustainabilityPostEntity
	Scenario: Delete all SustainabilityPostEntity on all pages
	Given I have 10 valid SustainabilityPostEntity entities
	Given I login to the site as a user
	And I navigate to the SustainabilityPostEntity backend page
	When I select all entities on current page
	Then The bulk options bar shows up with correct information
	When I select all entities on all pages
	And I delete the selected items, and Accept to confirm
	Then I assert that the alertbox responds to our deletion request
