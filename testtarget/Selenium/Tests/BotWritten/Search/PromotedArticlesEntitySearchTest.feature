
@BotWritten @search
Feature: PromotedArticlesEntity Search

@PromotedArticlesEntity
Scenario: PromotedArticlesEntity Search
Given I login to the site as a user
And I navigate to the PromotedArticlesEntity backend page
When I insert a valid PromotedArticlesEntity, search for it and delete it