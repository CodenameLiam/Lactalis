
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
	public class TradingPostCategoryEntityType : EfObjectGraphType<LactalisDBContext, TradingPostCategoryEntity>
	{
		public TradingPostCategoryEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL many to many reference to entity  via reference TradingPostCategories
			IEnumerable<TradingPostListingsTradingPostCategories> TradingPostListingssResolveFunction(ResolveFieldContext<TradingPostCategoryEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TradingPostListingsTradingPostCategories>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.TradingPostListingss.Where(filter.Compile());
			}
			AddNavigationListField("TradingPostListingss", (Func<ResolveFieldContext<TradingPostCategoryEntity>, IEnumerable<TradingPostListingsTradingPostCategories>>) TradingPostListingssResolveFunction);
			AddNavigationConnectionField("TradingPostListingssConnection", TradingPostListingssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TradingPostCategoryEntityInputType : InputObjectGraphType<TradingPostCategoryEntity>
	{
		public TradingPostCategoryEntityInputType()
		{
			Name = "TradingPostCategoryEntityInput";
			Description = "The input object for adding a new TradingPostCategoryEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<TradingPostListingsTradingPostCategoriesInputType>>("TradingPostListingss");

		}
	}

}