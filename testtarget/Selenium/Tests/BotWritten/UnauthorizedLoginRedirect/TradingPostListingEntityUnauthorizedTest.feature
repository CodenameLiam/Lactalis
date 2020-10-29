
@BotWritten @loginredirect
Feature: Unauthorized TradingPostListingEntity Redirect

@TradingPostListingEntity
Scenario: Unauthorized TradingPostListingEntity Redirect
Given I am logged out of the site
And I navigate to the TradingPostListingEntity backend page
Then I assert that I am redirected from TradingPostListingEntity to login page

# %  ion % [Add any additional tests here] end