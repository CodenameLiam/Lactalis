
@BotWritten @loginredirect
Feature: Unauthorized QualityDocumentEntity Redirect

@QualityDocumentEntity
Scenario: Unauthorized QualityDocumentEntity Redirect
Given I am logged out of the site
And I navigate to the QualityDocumentEntity backend page
Then I assert that I am redirected from QualityDocumentEntity to login page

# %  ion % [Add any additional tests here] end