
@BotWritten @search
Feature: NewsArticleEntity Search

@NewsArticleEntity
Scenario: NewsArticleEntity Search
Given I login to the site as a user
And I navigate to the NewsArticleEntity backend page
When I insert a valid NewsArticleEntity, search for it and delete it