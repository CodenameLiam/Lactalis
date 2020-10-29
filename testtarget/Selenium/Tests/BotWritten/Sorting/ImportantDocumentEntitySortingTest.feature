
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort ImportantDocumentEntity
	
	@ImportantDocumentEntity
	Scenario: Sort ImportantDocumentEntity
	Given I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	When I sort ImportantDocumentEntity by File
	Then I assert that File in ImportantDocumentEntity of type String is properly sorted in descending
	When I sort ImportantDocumentEntity by File
	Then I assert that File in ImportantDocumentEntity of type String is properly sorted in ascending
	When I sort ImportantDocumentEntity by Name
	Then I assert that Name in ImportantDocumentEntity of type String is properly sorted in descending
	When I sort ImportantDocumentEntity by Name
	Then I assert that Name in ImportantDocumentEntity of type String is properly sorted in ascending
	When I sort ImportantDocumentEntity by QLD
	Then I assert that QLD in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by QLD
	Then I assert that QLD in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by NSW
	Then I assert that NSW in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by NSW
	Then I assert that NSW in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by VIC
	Then I assert that VIC in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by VIC
	Then I assert that VIC in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by TAS
	Then I assert that TAS in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by TAS
	Then I assert that TAS in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by WA
	Then I assert that WA in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by WA
	Then I assert that WA in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by SA
	Then I assert that SA in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by SA
	Then I assert that SA in ImportantDocumentEntity of type bool is properly sorted in ascending
	When I sort ImportantDocumentEntity by NT
	Then I assert that NT in ImportantDocumentEntity of type bool is properly sorted in descending
	When I sort ImportantDocumentEntity by NT
	Then I assert that NT in ImportantDocumentEntity of type bool is properly sorted in ascending

