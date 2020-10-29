
@BotWritten @associations
Feature: Reference from ImportantDocumentEntity using Important Documents to ImportantDocumentCategoryEntity using Document Category
	Scenario: Reference from ImportantDocumentEntity using Important Documents to ImportantDocumentCategoryEntity using Document Category
	Given I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	And I create 3 ImportantDocumentEntity's each associated with 1 DocumentCategory using Document Category
	Then I validate each ImportantDocumentCategoryEntity has 3 ImportantDocumentEntity associations using Important Documents
	Then I validate each ImportantDocumentEntity has 1 ImportantDocumentCategoryEntity associations using Document Category