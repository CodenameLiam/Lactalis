
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
	public class QualityDocumentCategoryEntityType : EfObjectGraphType<LactalisDBContext, QualityDocumentCategoryEntity>
	{
		public QualityDocumentCategoryEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL reference to entity QualityDocumentEntity via reference QualityDocuments
			IEnumerable<QualityDocumentEntity> QualityDocumentssResolveFunction(ResolveFieldContext<QualityDocumentCategoryEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<QualityDocumentEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.QualityDocumentss.Where(filter.Compile());
			}
			AddNavigationListField("QualityDocumentss", (Func<ResolveFieldContext<QualityDocumentCategoryEntity>, IEnumerable<QualityDocumentEntity>>) QualityDocumentssResolveFunction);
			AddNavigationConnectionField("QualityDocumentssConnection", QualityDocumentssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class QualityDocumentCategoryEntityInputType : InputObjectGraphType<QualityDocumentCategoryEntity>
	{
		public QualityDocumentCategoryEntityInputType()
		{
			Name = "QualityDocumentCategoryEntityInput";
			Description = "The input object for adding a new QualityDocumentCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<QualityDocumentEntityInputType>>("QualityDocumentss");

		}
	}

}