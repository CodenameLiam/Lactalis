
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
	public class QualityDocumentEntityType : EfObjectGraphType<LactalisDBContext, QualityDocumentEntity>
	{
		public QualityDocumentEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references
			Field(o => o.QualityDocumentCategoryId, type: typeof(IdGraphType));

			// GraphQL reference to entity QualityDocumentCategoryEntity via reference QualityDocumentCategory
			AddNavigationField("QualityDocumentCategory", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<QualityDocumentCategoryEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.QualityDocumentCategory;

				if (value != null)
				{
					return new List<QualityDocumentCategoryEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class QualityDocumentEntityInputType : InputObjectGraphType<QualityDocumentEntity>
	{
		public QualityDocumentEntityInputType()
		{
			Name = "QualityDocumentEntityInput";
			Description = "The input object for adding a new QualityDocumentEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field<StringGraphType>("Name");

			// Add entity references
			Field<IdGraphType>("QualityDocumentCategoryId");

			// Add references to foreign models to allow nested creation
			Field<QualityDocumentCategoryEntityInputType>("QualityDocumentCategory");

		}
	}

}