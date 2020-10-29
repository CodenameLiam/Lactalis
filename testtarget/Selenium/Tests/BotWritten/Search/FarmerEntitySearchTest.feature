
@BotWritten @search
Feature: FarmerEntity Search

@FarmerEntity
Scenario: FarmerEntity Search
Given I login to the site as a user
And I navigate to the FarmerEntity backend page
When I insert a valid FarmerEntity, search for it and delete it