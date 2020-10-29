
@BotWritten @associations
Feature: Reference from AgriSupplyDocumentEntity using Agri Supply Documents to AgriSupplyDocumentCategoryEntity using Agri Supply Document Category
	Scenario: Reference from AgriSupplyDocumentEntity using Agri Supply Documents to AgriSupplyDocumentCategoryEntity using Agri Supply Document Category
	Given I login to the site as a user
	And I navigate to the AgriSupplyDocumentEntity backend page
	And I create 3 AgriSupplyDocumentEntity's each associated with 1 AgriSupplyDocumentCategory using Agri Supply Document Category
	Then I validate each AgriSupplyDocumentCategoryEntity has 3 AgriSupplyDocumentEntity associations using Agri Supply Documents
	Then I validate each AgriSupplyDocumentEntity has 1 AgriSupplyDocumentCategoryEntity associations using Agri Supply Document Category