

@BotWritten @bulkDeleteoptions @xunit:collection(BULK_DELETE)
Feature: Bulk Delete FarmEntity entities
	
	@FarmEntity
	Scenario: Bulk Delete FarmEntity entities
	Given I have 10 valid FarmEntity entities
	Given I login to the site as a user
	And I navigate to the FarmEntity backend page
	When I select all entities on current page
	Then The bulk options bar shows up with correct information
	Then I assert 10 items have been selected
	When I delete the selected items, and Accept to confirm
	Then I assert that the alertbox responds to our deletion request

