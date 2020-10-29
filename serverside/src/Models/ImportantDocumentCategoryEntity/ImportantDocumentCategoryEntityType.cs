
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
	public class ImportantDocumentCategoryEntityType : EfObjectGraphType<LactalisDBContext, ImportantDocumentCategoryEntity>
	{
		public ImportantDocumentCategoryEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL reference to entity ImportantDocumentEntity via reference ImportantDocuments
			IEnumerable<ImportantDocumentEntity> ImportantDocumentssResolveFunction(ResolveFieldContext<ImportantDocumentCategoryEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<ImportantDocumentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.ImportantDocumentss.Where(filter.Compile());
			}
			AddNavigationListField("ImportantDocumentss", (Func<ResolveFieldContext<ImportantDocumentCategoryEntity>, IEnumerable<ImportantDocumentEntity>>) ImportantDocumentssResolveFunction);
			AddNavigationConnectionField("ImportantDocumentssConnection", ImportantDocumentssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class ImportantDocumentCategoryEntityInputType : InputObjectGraphType<ImportantDocumentCategoryEntity>
	{
		public ImportantDocumentCategoryEntityInputType()
		{
			Name = "ImportantDocumentCategoryEntityInput";
			Description = "The input object for adding a new ImportantDocumentCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<ImportantDocumentEntityInputType>>("ImportantDocumentss");

		}
	}

}