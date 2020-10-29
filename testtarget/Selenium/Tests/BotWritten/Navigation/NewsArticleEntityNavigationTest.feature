
@BotWritten @navigation
Feature: NewsArticleEntity Navigation Feature

@NewsArticleEntity
Scenario: Navigate to NewsArticleEntity backend page
Given I login to the site as a user
And I navigate to the NewsArticleEntity backend page
Then I assert that I am on the NewsArticleEntity backend page