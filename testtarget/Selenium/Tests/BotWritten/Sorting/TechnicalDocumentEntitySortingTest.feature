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

Feature: Sort TechnicalDocumentEntity
	
	@TechnicalDocumentEntity
	Scenario: Sort TechnicalDocumentEntity
	Given I login to the site as a user
	And I navigate to the TechnicalDocumentEntity backend page
	When I sort TechnicalDocumentEntity by File
	Then I assert that File in TechnicalDocumentEntity of type String is properly sorted in descending
	When I sort TechnicalDocumentEntity by File
	Then I assert that File in TechnicalDocumentEntity of type String is properly sorted in ascending
	When I sort TechnicalDocumentEntity by Name
	Then I assert that Name in TechnicalDocumentEntity of type String is properly sorted in descending
	When I sort TechnicalDocumentEntity by Name
	Then I assert that Name in TechnicalDocumentEntity of type String is properly sorted in ascending

