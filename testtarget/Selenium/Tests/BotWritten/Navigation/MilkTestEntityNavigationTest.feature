
@BotWritten @navigation
Feature: MilkTestEntity Navigation Feature

@MilkTestEntity
Scenario: Navigate to MilkTestEntity backend page
Given I login to the site as a user
And I navigate to the MilkTestEntity backend page
Then I assert that I am on the MilkTestEntity backend page