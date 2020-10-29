
@BotWritten @navigation
Feature: SustainabilityPostEntity Navigation Feature

@SustainabilityPostEntity
Scenario: Navigate to SustainabilityPostEntity backend page
Given I login to the site as a user
And I navigate to the SustainabilityPostEntity backend page
Then I assert that I am on the SustainabilityPostEntity backend page