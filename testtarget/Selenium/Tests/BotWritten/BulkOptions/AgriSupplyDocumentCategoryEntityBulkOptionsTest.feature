
@BotWritten @bulkoptions @ignore
Feature: AgriSupplyDocumentCategoryEntity Bulk Option Selection
# WARNING: These Tests have been flagged as needing web fixes and are currently ignored

@AgriSupplyDocumentCategoryEntity
Scenario: Select all AgriSupplyDocumentCategoryEntitys on current page
Given I login to the site as a user
And I navigate to the AgriSupplyDocumentCategoryEntity backend page
When I select all entities on current page
Then I click the bulk bar cancel button
Then 0 entities on current page should be selected

@AgriSupplyDocumentCategoryEntity
Scenario: I attempt to select all AgriSupplyDocumentCategoryEntitys on all pages
Given I login to the site as a user
And I navigate to the AgriSupplyDocumentCategoryEntity backend page
When I select all entities on current page
Then The bulk options bar shows up with correct information
And I select all items in the collection
Then I click the bulk bar cancel button
Then 0 entities on current page should be selected