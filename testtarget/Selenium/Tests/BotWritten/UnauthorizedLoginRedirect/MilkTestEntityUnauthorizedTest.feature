
@BotWritten @loginredirect
Feature: Unauthorized MilkTestEntity Redirect

@MilkTestEntity
Scenario: Unauthorized MilkTestEntity Redirect
Given I am logged out of the site
And I navigate to the MilkTestEntity backend page
Then I assert that I am redirected from MilkTestEntity to login page

# %  ion % [Add any additional tests here] end