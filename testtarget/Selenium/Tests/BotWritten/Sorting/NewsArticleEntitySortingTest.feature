
@sorting @BotWritten @ignore
# WARNING: These Tests have been flagged as unstable and have been ignored until they are updated.

Feature: Sort NewsArticleEntity
	
	@NewsArticleEntity
	Scenario: Sort NewsArticleEntity
	Given I login to the site as a user
	And I navigate to the NewsArticleEntity backend page
	When I sort NewsArticleEntity by Headline
	Then I assert that Headline in NewsArticleEntity of type String is properly sorted in descending
	When I sort NewsArticleEntity by Headline
	Then I assert that Headline in NewsArticleEntity of type String is properly sorted in ascending
	When I sort NewsArticleEntity by Description
	Then I assert that Description in NewsArticleEntity of type String is properly sorted in descending
	When I sort NewsArticleEntity by Description
	Then I assert that Description in NewsArticleEntity of type String is properly sorted in ascending
	When I sort NewsArticleEntity by Feature Image
	Then I assert that Feature Image in NewsArticleEntity of type String is properly sorted in descending
	When I sort NewsArticleEntity by Feature Image
	Then I assert that Feature Image in NewsArticleEntity of type String is properly sorted in ascending
	When I sort NewsArticleEntity by Content
	Then I assert that Content in NewsArticleEntity of type String is properly sorted in descending
	When I sort NewsArticleEntity by Content
	Then I assert that Content in NewsArticleEntity of type String is properly sorted in ascending
	When I sort NewsArticleEntity by QLD
	Then I assert that QLD in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by QLD
	Then I assert that QLD in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by NSW
	Then I assert that NSW in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by NSW
	Then I assert that NSW in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by VIC
	Then I assert that VIC in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by VIC
	Then I assert that VIC in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by TAS
	Then I assert that TAS in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by TAS
	Then I assert that TAS in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by WA
	Then I assert that WA in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by WA
	Then I assert that WA in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by SA
	Then I assert that SA in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by SA
	Then I assert that SA in NewsArticleEntity of type bool is properly sorted in ascending
	When I sort NewsArticleEntity by NT
	Then I assert that NT in NewsArticleEntity of type bool is properly sorted in descending
	When I sort NewsArticleEntity by NT
	Then I assert that NT in NewsArticleEntity of type bool is properly sorted in ascending

