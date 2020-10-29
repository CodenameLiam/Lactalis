
@BotWritten @navigation
Feature: PromotedArticlesEntity Navigation Feature

@PromotedArticlesEntity
Scenario: Navigate to PromotedArticlesEntity backend page
Given I login to the site as a user
And I navigate to the PromotedArticlesEntity backend page
Then I assert that I am on the PromotedArticlesEntity backend page