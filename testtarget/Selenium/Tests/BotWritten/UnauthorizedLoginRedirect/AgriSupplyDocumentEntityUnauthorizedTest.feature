
@BotWritten @loginredirect
Feature: Unauthorized AgriSupplyDocumentEntity Redirect

@AgriSupplyDocumentEntity
Scenario: Unauthorized AgriSupplyDocumentEntity Redirect
Given I am logged out of the site
And I navigate to the AgriSupplyDocumentEntity backend page
Then I assert that I am redirected from AgriSupplyDocumentEntity to login page

# %  ion % [Add any additional tests here] end