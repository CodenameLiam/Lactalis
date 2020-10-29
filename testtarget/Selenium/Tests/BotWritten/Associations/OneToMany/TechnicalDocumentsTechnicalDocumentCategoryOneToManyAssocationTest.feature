
@BotWritten @associations
Feature: Reference from TechnicalDocumentEntity using Technical Documents to TechnicalDocumentCategoryEntity using Technical Document Category
	Scenario: Reference from TechnicalDocumentEntity using Technical Documents to TechnicalDocumentCategoryEntity using Technical Document Category
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	And I create 3 TechnicalDocumentEntity's each associated with 1 TechnicalDocumentCategory using Technical Document Category
	Then I validate each TechnicalDocumentCategoryEntity has 3 TechnicalDocumentEntity associations using Technical Documents
	Then I validate each TechnicalDocumentEntity has 1 TechnicalDocumentCategoryEntity associations using Technical Document Category