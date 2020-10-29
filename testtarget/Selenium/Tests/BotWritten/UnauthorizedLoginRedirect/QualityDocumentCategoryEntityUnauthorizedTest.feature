
@BotWritten @loginredirect
Feature: Unauthorized QualityDocumentCategoryEntity Redirect

@QualityDocumentCategoryEntity
Scenario: Unauthorized QualityDocumentCategoryEntity Redirect
Given I am logged out of the site
And I navigate to the QualityDocumentCategoryEntity backend page
Then I assert that I am redirected from QualityDocumentCategoryEntity to login page

# %  ion % [Add any additional tests here] end