
@BotWritten @loginredirect
Feature: Unauthorized ImportantDocumentEntity Redirect

@ImportantDocumentEntity
Scenario: Unauthorized ImportantDocumentEntity Redirect
Given I am logged out of the site
And I navigate to the ImportantDocumentEntity backend page
Then I assert that I am redirected from ImportantDocumentEntity to login page

# %  ion % [Add any additional tests here] end