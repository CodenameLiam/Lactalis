/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Lactalis.Models
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class ImportantDocumentEntityType : EfObjectGraphType<LactalisDBContext, ImportantDocumentEntity>
	{
		public ImportantDocumentEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.Qld, type: typeof(BooleanGraphType));
			Field(o => o.Nsw, type: typeof(BooleanGraphType));
			Field(o => o.Vic, type: typeof(BooleanGraphType));
			Field(o => o.Tas, type: typeof(BooleanGraphType));
			Field(o => o.Wa, type: typeof(BooleanGraphType));
			Field(o => o.Sa, type: typeof(BooleanGraphType));
			Field(o => o.Nt, type: typeof(BooleanGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.DocumentCategoryId, type: typeof(IdGraphType));

			// GraphQL reference to entity ImportantDocumentCategoryEntity via reference DocumentCategory
			AddNavigationField("DocumentCategory", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<ImportantDocumentCategoryEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.DocumentCategory;

				if (value != null)
				{
					return new List<ImportantDocumentCategoryEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class ImportantDocumentEntityInputType : InputObjectGraphType<ImportantDocumentEntity>
	{
		public ImportantDocumentEntityInputType()
		{
			Name = "ImportantDocumentEntityInput";
			Description = "The input object for adding a new ImportantDocumentEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field<StringGraphType>("Name");
			Field<BooleanGraphType>("Qld");
			Field<BooleanGraphType>("Nsw");
			Field<BooleanGraphType>("Vic");
			Field<BooleanGraphType>("Tas");
			Field<BooleanGraphType>("Wa");
			Field<BooleanGraphType>("Sa");
			Field<BooleanGraphType>("Nt");

			// Add entity references
			Field<IdGraphType>("DocumentCategoryId");

			// Add references to foreign models to allow nested creation
			Field<ImportantDocumentCategoryEntityInputType>("DocumentCategory");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}