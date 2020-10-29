
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
	public class TechnicalDocumentEntityType : EfObjectGraphType<LactalisDBContext, TechnicalDocumentEntity>
	{
		public TechnicalDocumentEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references
			Field(o => o.TechnicalDocumentCategoryId, type: typeof(IdGraphType));

			// GraphQL reference to entity TechnicalDocumentCategoryEntity via reference TechnicalDocumentCategory
			AddNavigationField("TechnicalDocumentCategory", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TechnicalDocumentCategoryEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.TechnicalDocumentCategory;

				if (value != null)
				{
					return new List<TechnicalDocumentCategoryEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TechnicalDocumentEntityInputType : InputObjectGraphType<TechnicalDocumentEntity>
	{
		public TechnicalDocumentEntityInputType()
		{
			Name = "TechnicalDocumentEntityInput";
			Description = "The input object for adding a new TechnicalDocumentEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field<StringGraphType>("Name");

			// Add entity references
			Field<IdGraphType>("TechnicalDocumentCategoryId");

			// Add references to foreign models to allow nested creation
			Field<TechnicalDocumentCategoryEntityInputType>("TechnicalDocumentCategory");

		}
	}

}