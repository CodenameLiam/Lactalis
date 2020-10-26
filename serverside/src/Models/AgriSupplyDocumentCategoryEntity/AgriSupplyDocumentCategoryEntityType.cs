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
	public class AgriSupplyDocumentCategoryEntityType : EfObjectGraphType<LactalisDBContext, AgriSupplyDocumentCategoryEntity>
	{
		public AgriSupplyDocumentCategoryEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references

			// GraphQL reference to entity AgriSupplyDocumentEntity via reference AgriSupplyDocuments
			IEnumerable<AgriSupplyDocumentEntity> AgriSupplyDocumentssResolveFunction(ResolveFieldContext<AgriSupplyDocumentCategoryEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<AgriSupplyDocumentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.AgriSupplyDocumentss.Where(filter.Compile());
			}
			AddNavigationListField("AgriSupplyDocumentss", (Func<ResolveFieldContext<AgriSupplyDocumentCategoryEntity>, IEnumerable<AgriSupplyDocumentEntity>>) AgriSupplyDocumentssResolveFunction);
			AddNavigationConnectionField("AgriSupplyDocumentssConnection", AgriSupplyDocumentssResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class AgriSupplyDocumentCategoryEntityInputType : InputObjectGraphType<AgriSupplyDocumentCategoryEntity>
	{
		public AgriSupplyDocumentCategoryEntityInputType()
		{
			Name = "AgriSupplyDocumentCategoryEntityInput";
			Description = "The input object for adding a new AgriSupplyDocumentCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<AgriSupplyDocumentEntityInputType>>("AgriSupplyDocumentss");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}