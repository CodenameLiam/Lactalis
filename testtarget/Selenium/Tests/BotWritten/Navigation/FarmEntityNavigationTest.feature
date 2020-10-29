
@BotWritten @navigation
Feature: FarmEntity Navigation Feature

@FarmEntity
Scenario: Navigate to FarmEntity backend page
Given I login to the site as a user
And I navigate to the FarmEntity backend page
Then I assert that I am on the FarmEntity backend page