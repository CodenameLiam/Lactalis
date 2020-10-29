
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort TechnicalDocumentEntity
	
	@TechnicalDocumentEntity
	Scenario: Sort TechnicalDocumentEntity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	When I sort TechnicalDocumentEntity by File
	Then I assert that File in TechnicalDocumentEntity of type String is properly sorted in descending
	When I sort TechnicalDocumentEntity by File
	Then I assert that File in TechnicalDocumentEntity of type String is properly sorted in ascending
	When I sort TechnicalDocumentEntity by Name
	Then I assert that Name in TechnicalDocumentEntity of type String is properly sorted in descending
	When I sort TechnicalDocumentEntity by Name
	Then I assert that Name in TechnicalDocumentEntity of type String is properly sorted in ascending

