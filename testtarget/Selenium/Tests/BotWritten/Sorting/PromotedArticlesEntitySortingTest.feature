
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort PromotedArticlesEntity
	
	@PromotedArticlesEntity
	Scenario: Sort PromotedArticlesEntity
	Given I login to the site as a user
	And I navigate to the PromotedArticlesEntity backend page
	When I sort PromotedArticlesEntity by Name
	Then I assert that Name in PromotedArticlesEntity of type String is properly sorted in descending
	When I sort PromotedArticlesEntity by Name
	Then I assert that Name in PromotedArticlesEntity of type String is properly sorted in ascending

