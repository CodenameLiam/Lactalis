
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort ImportantDocumentCategoryEntity
	
	@ImportantDocumentCategoryEntity
	Scenario: Sort ImportantDocumentCategoryEntity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentCategoryEntity backend page
	When I sort ImportantDocumentCategoryEntity by Name
	Then I assert that Name in ImportantDocumentCategoryEntity of type String is properly sorted in descending
	When I sort ImportantDocumentCategoryEntity by Name
	Then I assert that Name in ImportantDocumentCategoryEntity of type String is properly sorted in ascending

