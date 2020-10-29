
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
	public class TradingPostListingsTradingPostCategoriesType : EfObjectGraphType<LactalisDBContext, TradingPostListingsTradingPostCategories>
	{
		public TradingPostListingsTradingPostCategoriesType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));

			Field(o => o.TradingPostListingsId, type: typeof(IdGraphType));
			Field(o => o.TradingPostCategoriesId, type: typeof(IdGraphType));


			// GraphQL reference to entity TradingPostListingEntity via reference TradingPostListingEntity
			AddNavigationField("TradingPostListings", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TradingPostListingEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.TradingPostListings;
				return new List<TradingPostListingEntity> {value}.All(filter.Compile()) ? value : null;
			});

			// GraphQL reference to entity TradingPostCategoryEntity via reference TradingPostCategoryEntity
			AddNavigationField("TradingPostCategories", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TradingPostCategoryEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.TradingPostCategories;
				return new List<TradingPostCategoryEntity> {value}.All(filter.Compile()) ? value : null;
			});

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TradingPostListingsTradingPostCategoriesInputType : InputObjectGraphType<TradingPostListingsTradingPostCategories>
	{
		public TradingPostListingsTradingPostCategoriesInputType()
		{
			Name = "TradingPostListingsTradingPostCategoriesInput";
			Description = "The input object for adding a new TradingPostListingsTradingPostCategories";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IdGraphType>("TradingPostListingsId");
			Field<IdGraphType>("TradingPostCategoriesId");

			// Add references to foreign objects
			Field<TradingPostListingEntityInputType>("TradingPostListings");
			Field<TradingPostCategoryEntityInputType>("TradingPostCategories");

		}
	}
}