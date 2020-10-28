###
# @bot-written
# 
# WARNING AND NOTICE
# Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
# Full Software Licence as accepted by you before being granted access to this source code and other materials,
# the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
# commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
# licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
# including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
# access, download, storage, and/or use of this source code.
# 
# BOT WARNING
# This file is bot-written.
# Any changes out side of "protected regions" will be lost next time the bot makes any changes.
###
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

