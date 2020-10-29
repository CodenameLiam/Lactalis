
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
	public class FarmerEntityType : EfObjectGraphType<LactalisDBContext, FarmerEntity>
	{
		public FarmerEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Email, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL reference to entity TradingPostListingEntity via reference TradingPostListings
			IEnumerable<TradingPostListingEntity> TradingPostListingssResolveFunction(ResolveFieldContext<FarmerEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TradingPostListingEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.TradingPostListingss.Where(filter.Compile());
			}
			AddNavigationListField("TradingPostListingss", (Func<ResolveFieldContext<FarmerEntity>, IEnumerable<TradingPostListingEntity>>) TradingPostListingssResolveFunction);
			AddNavigationConnectionField("TradingPostListingssConnection", TradingPostListingssResolveFunction);

			// GraphQL many to many reference to entity FarmEntity via reference Farms
			IEnumerable<FarmersFarms> FarmssResolveFunction(ResolveFieldContext<FarmerEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<FarmersFarms>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Farmss.Where(filter.Compile());
			}
			AddNavigationListField("Farmss", (Func<ResolveFieldContext<FarmerEntity>, IEnumerable<FarmersFarms>>) FarmssResolveFunction);
			AddNavigationConnectionField("FarmssConnection", FarmssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class FarmerEntityInputType : InputObjectGraphType<FarmerEntity>
	{
		public FarmerEntityInputType()
		{
			Name = "FarmerEntityInput";
			Description = "The input object for adding a new FarmerEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<TradingPostListingEntityInputType>>("TradingPostListingss");
			Field<ListGraphType<FarmersFarmsInputType>>("Farmss");

		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class FarmerEntityCreateInputType : InputObjectGraphType<FarmerEntity>
	{
		public FarmerEntityCreateInputType()
		{
			Name = "FarmerEntityCreateInput";
			Description = "The input object for creating a new FarmerEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");


			// Add entity references


			// Add references to foreign models to allow nested creation
			Field<ListGraphType<TradingPostListingEntityInputType>>("TradingPostListingss");
			Field<ListGraphType<FarmersFarmsInputType>>("Farmss");

		}
	}
}