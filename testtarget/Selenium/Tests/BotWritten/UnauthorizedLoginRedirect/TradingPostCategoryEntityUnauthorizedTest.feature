
@BotWritten @loginredirect
Feature: Unauthorized TradingPostCategoryEntity Redirect

@TradingPostCategoryEntity
Scenario: Unauthorized TradingPostCategoryEntity Redirect
Given I am logged out of the site
And I navigate to the TradingPostCategoryEntity backend page
Then I assert that I am redirected from TradingPostCategoryEntity to login page

# %  ion % [Add any additional tests here] end