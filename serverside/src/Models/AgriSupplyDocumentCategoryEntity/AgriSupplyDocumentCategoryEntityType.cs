
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;


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

		}
	}

}