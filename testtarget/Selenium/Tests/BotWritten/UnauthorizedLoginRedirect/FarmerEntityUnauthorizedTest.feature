
@BotWritten @loginredirect
Feature: Unauthorized FarmerEntity Redirect

@FarmerEntity
Scenario: Unauthorized FarmerEntity Redirect
Given I am logged out of the site
And I navigate to the FarmerEntity backend page
Then I assert that I am redirected from FarmerEntity to login page

# %  ion % [Add any additional tests here] end