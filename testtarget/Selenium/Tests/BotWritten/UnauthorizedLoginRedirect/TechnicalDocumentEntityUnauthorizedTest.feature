
@BotWritten @loginredirect
Feature: Unauthorized TechnicalDocumentEntity Redirect

@TechnicalDocumentEntity
Scenario: Unauthorized TechnicalDocumentEntity Redirect
Given I am logged out of the site
And I navigate to the TechnicalDocumentEntity backend page
Then I assert that I am redirected from TechnicalDocumentEntity to login page

# %  ion % [Add any additional tests here] end