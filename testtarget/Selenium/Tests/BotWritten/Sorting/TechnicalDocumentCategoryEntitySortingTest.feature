
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort TechnicalDocumentCategoryEntity
	
	@TechnicalDocumentCategoryEntity
	Scenario: Sort TechnicalDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentCategoryEntity backend page
	When I sort TechnicalDocumentCategoryEntity by Name
	Then I assert that Name in TechnicalDocumentCategoryEntity of type String is properly sorted in descending
	When I sort TechnicalDocumentCategoryEntity by Name
	Then I assert that Name in TechnicalDocumentCategoryEntity of type String is properly sorted in ascending

