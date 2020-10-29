
@BotWritten @loginredirect
Feature: Unauthorized SustainabilityPostEntity Redirect

@SustainabilityPostEntity
Scenario: Unauthorized SustainabilityPostEntity Redirect
Given I am logged out of the site
And I navigate to the SustainabilityPostEntity backend page
Then I assert that I am redirected from SustainabilityPostEntity to login page

# %  ion % [Add any additional tests here] end