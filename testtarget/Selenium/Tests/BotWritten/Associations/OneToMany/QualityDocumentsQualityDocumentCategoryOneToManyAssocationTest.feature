
@BotWritten @associations
Feature: Reference from QualityDocumentEntity using Quality Documents to QualityDocumentCategoryEntity using Quality Document Category
	Scenario: Reference from QualityDocumentEntity using Quality Documents to QualityDocumentCategoryEntity using Quality Document Category
	Given I login to the site as a user
	And I navigate to the QualityDocumentEntity backend page
	And I create 3 QualityDocumentEntity's each associated with 1 QualityDocumentCategory using Quality Document Category
	Then I validate each QualityDocumentCategoryEntity has 3 QualityDocumentEntity associations using Quality Documents
	Then I validate each QualityDocumentEntity has 1 QualityDocumentCategoryEntity associations using Quality Document Category