
@BotWritten @bulkoptions @ignore
Feature: MilkTestEntity Bulk Option Selection
# WARNING: These Tests have been flagged as needing web fixes and are currently ignored

@MilkTestEntity
Scenario: Select all MilkTestEntitys on current page
Given I login to the site as a user
And I navigate to the MilkTestEntity backend page
When I select all entities on current page
Then I click the bulk bar cancel button
Then 0 entities on current page should be selected

@MilkTestEntity
Scenario: I attempt to select all MilkTestEntitys on all pages
Given I login to the site as a user
And I navigate to the MilkTestEntity backend page
When I select all entities on current page
Then The bulk options bar shows up with correct information
And I select all items in the collection
Then I click the bulk bar cancel button
Then 0 entities on current page should be selected