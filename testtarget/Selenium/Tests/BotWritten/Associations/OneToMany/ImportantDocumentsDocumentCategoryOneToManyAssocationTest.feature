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
@BotWritten @associations
Feature: Reference from ImportantDocumentEntity using Important Documents to ImportantDocumentCategoryEntity using Document Category
	Scenario: Reference from ImportantDocumentEntity using Important Documents to ImportantDocumentCategoryEntity using Document Category
	Given I login to the site as a user
	And I navigate to the ImportantDocumentEntity backend page
	And I create 3 ImportantDocumentEntity's each associated with 1 DocumentCategory using Document Category
	Then I validate each ImportantDocumentCategoryEntity has 3 ImportantDocumentEntity associations using Important Documents
	Then I validate each ImportantDocumentEntity has 1 ImportantDocumentCategoryEntity associations using Document Category