
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort AgriSupplyDocumentEntity
	
	@AgriSupplyDocumentEntity
	Scenario: Sort AgriSupplyDocumentEntity
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentEntity backend page
	When I sort AgriSupplyDocumentEntity by File
	Then I assert that File in AgriSupplyDocumentEntity of type String is properly sorted in descending
	When I sort AgriSupplyDocumentEntity by File
	Then I assert that File in AgriSupplyDocumentEntity of type String is properly sorted in ascending
	When I sort AgriSupplyDocumentEntity by Name
	Then I assert that Name in AgriSupplyDocumentEntity of type String is properly sorted in descending
	When I sort AgriSupplyDocumentEntity by Name
	Then I assert that Name in AgriSupplyDocumentEntity of type String is properly sorted in ascending

