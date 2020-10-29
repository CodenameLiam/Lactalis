
@BotWritten @navigation
Feature: AdminEntity Navigation Feature

@AdminEntity
Scenario: Navigate to AdminEntity backend page
Given I login to the site as a user
And I navigate to the AdminEntity backend page
Then I assert that I am on the AdminEntity backend page