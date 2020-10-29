
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort AgriSupplyDocumentCategoryEntity
	
	@AgriSupplyDocumentCategoryEntity
	Scenario: Sort AgriSupplyDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentCategoryEntity backend page
	When I sort AgriSupplyDocumentCategoryEntity by Name
	Then I assert that Name in AgriSupplyDocumentCategoryEntity of type String is properly sorted in descending
	When I sort AgriSupplyDocumentCategoryEntity by Name
	Then I assert that Name in AgriSupplyDocumentCategoryEntity of type String is properly sorted in ascending

