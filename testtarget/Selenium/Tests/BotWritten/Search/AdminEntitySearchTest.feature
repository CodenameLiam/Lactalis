
@BotWritten @search
Feature: AdminEntity Search

@AdminEntity
Scenario: AdminEntity Search
Given I login to the site as a user
And I navigate to the AdminEntity backend page
When I insert a valid AdminEntity, search for it and delete it