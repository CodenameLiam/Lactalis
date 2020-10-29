
@BotWritten @loginredirect
Feature: Unauthorized PromotedArticlesEntity Redirect

@PromotedArticlesEntity
Scenario: Unauthorized PromotedArticlesEntity Redirect
Given I am logged out of the site
And I navigate to the PromotedArticlesEntity backend page
Then I assert that I am redirected from PromotedArticlesEntity to login page

# %  ion % [Add any additional tests here] end