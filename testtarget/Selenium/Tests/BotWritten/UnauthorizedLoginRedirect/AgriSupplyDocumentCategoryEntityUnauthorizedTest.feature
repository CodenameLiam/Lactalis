
@BotWritten @loginredirect
Feature: Unauthorized AgriSupplyDocumentCategoryEntity Redirect

@AgriSupplyDocumentCategoryEntity
Scenario: Unauthorized AgriSupplyDocumentCategoryEntity Redirect
Given I am logged out of the site
And I navigate to the AgriSupplyDocumentCategoryEntity backend page
Then I assert that I am redirected from AgriSupplyDocumentCategoryEntity to login page

# %  ion % [Add any additional tests here] end