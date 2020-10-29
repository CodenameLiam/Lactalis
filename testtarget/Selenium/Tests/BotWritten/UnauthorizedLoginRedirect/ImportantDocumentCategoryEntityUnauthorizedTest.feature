
@BotWritten @loginredirect
Feature: Unauthorized ImportantDocumentCategoryEntity Redirect

@ImportantDocumentCategoryEntity
Scenario: Unauthorized ImportantDocumentCategoryEntity Redirect
Given I am logged out of the site
And I navigate to the ImportantDocumentCategoryEntity backend page
Then I assert that I am redirected from ImportantDocumentCategoryEntity to login page

# %  ion % [Add any additional tests here] end