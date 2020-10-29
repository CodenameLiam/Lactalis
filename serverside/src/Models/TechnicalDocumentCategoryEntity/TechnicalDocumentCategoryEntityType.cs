
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
	public class TechnicalDocumentCategoryEntityType : EfObjectGraphType<LactalisDBContext, TechnicalDocumentCategoryEntity>
	{
		public TechnicalDocumentCategoryEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL reference to entity TechnicalDocumentEntity via reference TechnicalDocuments
			IEnumerable<TechnicalDocumentEntity> TechnicalDocumentssResolveFunction(ResolveFieldContext<TechnicalDocumentCategoryEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TechnicalDocumentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.TechnicalDocumentss.Where(filter.Compile());
			}
			AddNavigationListField("TechnicalDocumentss", (Func<ResolveFieldContext<TechnicalDocumentCategoryEntity>, IEnumerable<TechnicalDocumentEntity>>) TechnicalDocumentssResolveFunction);
			AddNavigationConnectionField("TechnicalDocumentssConnection", TechnicalDocumentssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TechnicalDocumentCategoryEntityInputType : InputObjectGraphType<TechnicalDocumentCategoryEntity>
	{
		public TechnicalDocumentCategoryEntityInputType()
		{
			Name = "TechnicalDocumentCategoryEntityInput";
			Description = "The input object for adding a new TechnicalDocumentCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<TechnicalDocumentEntityInputType>>("TechnicalDocumentss");

		}
	}

}