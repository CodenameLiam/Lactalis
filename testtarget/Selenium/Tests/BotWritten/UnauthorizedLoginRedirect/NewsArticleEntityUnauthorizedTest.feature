
@BotWritten @loginredirect
Feature: Unauthorized NewsArticleEntity Redirect

@NewsArticleEntity
Scenario: Unauthorized NewsArticleEntity Redirect
Given I am logged out of the site
And I navigate to the NewsArticleEntity backend page
Then I assert that I am redirected from NewsArticleEntity to login page

# %  ion % [Add any additional tests here] end