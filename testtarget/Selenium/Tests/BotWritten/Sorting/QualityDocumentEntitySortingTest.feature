
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort QualityDocumentEntity
	
	@QualityDocumentEntity
	Scenario: Sort QualityDocumentEntity
	Given I login to the site as a user
	And I navigate to the QualityDocumentEntity backend page
	When I sort QualityDocumentEntity by File
	Then I assert that File in QualityDocumentEntity of type String is properly sorted in descending
	When I sort QualityDocumentEntity by File
	Then I assert that File in QualityDocumentEntity of type String is properly sorted in ascending
	When I sort QualityDocumentEntity by Name
	Then I assert that Name in QualityDocumentEntity of type String is properly sorted in descending
	When I sort QualityDocumentEntity by Name
	Then I assert that Name in QualityDocumentEntity of type String is properly sorted in ascending

