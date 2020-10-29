
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort QualityDocumentCategoryEntity
	
	@QualityDocumentCategoryEntity
	Scenario: Sort QualityDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the QualityDocumentCategoryEntity backend page
	When I sort QualityDocumentCategoryEntity by Name
	Then I assert that Name in QualityDocumentCategoryEntity of type String is properly sorted in descending
	When I sort QualityDocumentCategoryEntity by Name
	Then I assert that Name in QualityDocumentCategoryEntity of type String is properly sorted in ascending

