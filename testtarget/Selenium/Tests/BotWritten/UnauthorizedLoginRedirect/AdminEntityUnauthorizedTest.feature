
@BotWritten @loginredirect
Feature: Unauthorized AdminEntity Redirect

@AdminEntity
Scenario: Unauthorized AdminEntity Redirect
Given I am logged out of the site
And I navigate to the AdminEntity backend page
Then I assert that I am redirected from AdminEntity to login page

# %  ion % [Add any additional tests here] end