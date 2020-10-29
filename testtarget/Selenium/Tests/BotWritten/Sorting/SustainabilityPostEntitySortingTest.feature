
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort SustainabilityPostEntity
	
	@SustainabilityPostEntity
	Scenario: Sort SustainabilityPostEntity
	Given I login to the site as a user
	And I navigate to the SustainabilityPostEntity backend page
	When I sort SustainabilityPostEntity by Title
	Then I assert that Title in SustainabilityPostEntity of type String is properly sorted in descending
	When I sort SustainabilityPostEntity by Title
	Then I assert that Title in SustainabilityPostEntity of type String is properly sorted in ascending
	When I sort SustainabilityPostEntity by Image
	Then I assert that Image in SustainabilityPostEntity of type String is properly sorted in descending
	When I sort SustainabilityPostEntity by Image
	Then I assert that Image in SustainabilityPostEntity of type String is properly sorted in ascending
	When I sort SustainabilityPostEntity by File
	Then I assert that File in SustainabilityPostEntity of type String is properly sorted in descending
	When I sort SustainabilityPostEntity by File
	Then I assert that File in SustainabilityPostEntity of type String is properly sorted in ascending
	When I sort SustainabilityPostEntity by Content
	Then I assert that Content in SustainabilityPostEntity of type String is properly sorted in descending
	When I sort SustainabilityPostEntity by Content
	Then I assert that Content in SustainabilityPostEntity of type String is properly sorted in ascending

