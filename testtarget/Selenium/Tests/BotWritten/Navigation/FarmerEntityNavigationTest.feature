
@BotWritten @navigation
Feature: FarmerEntity Navigation Feature

@FarmerEntity
Scenario: Navigate to FarmerEntity backend page
Given I login to the site as a user
And I navigate to the FarmerEntity backend page
Then I assert that I am on the FarmerEntity backend page