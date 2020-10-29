
@BotWritten @loginredirect
Feature: Unauthorized FarmEntity Redirect

@FarmEntity
Scenario: Unauthorized FarmEntity Redirect
Given I am logged out of the site
And I navigate to the FarmEntity backend page
Then I assert that I am redirected from FarmEntity to login page

# %  ion % [Add any additional tests here] end