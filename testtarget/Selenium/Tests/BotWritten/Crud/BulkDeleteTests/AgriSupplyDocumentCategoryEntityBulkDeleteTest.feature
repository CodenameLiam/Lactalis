

@BotWritten @bulkDeleteoptions @xunit:collection(BULK_DELETE)
Feature: Bulk Delete AgriSupplyDocumentCategoryEntity entities
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: Bulk Delete AgriSupplyDocumentCategoryEntity entities
	Given I have 10 valid AgriSupplyDocumentCategoryEntity entities
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	When I select all entities on current page
	Then The bulk options bar shows up with correct information
	Then I assert 10 items have been selected
	When I delete the selected items, and Accept to confirm
	Then I assert that the alertbox responds to our deletion request

