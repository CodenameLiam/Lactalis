
@BotWritten @loginredirect
Feature: Unauthorized TechnicalDocumentCategoryEntity Redirect

@TechnicalDocumentCategoryEntity
Scenario: Unauthorized TechnicalDocumentCategoryEntity Redirect
Given I am logged out of the site
And I navigate to the TechnicalDocumentCategoryEntity backend page
Then I assert that I am redirected from TechnicalDocumentCategoryEntity to login page

# %  ion % [Add any additional tests here] end