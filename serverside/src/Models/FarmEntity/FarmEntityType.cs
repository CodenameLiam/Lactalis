
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Enums;
using Lactalis.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Models
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class FarmEntityType : EfObjectGraphType<LactalisDBContext, FarmEntity>
	{
		public FarmEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Code, type: typeof(StringGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.State, type: typeof(EnumerationGraphType<State>));

			// Add entity references

			// GraphQL reference to entity MilkTestEntity via reference Pickups
			IEnumerable<MilkTestEntity> PickupssResolveFunction(ResolveFieldContext<FarmEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<MilkTestEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Pickupss.Where(filter.Compile());
			}
			AddNavigationListField("Pickupss", (Func<ResolveFieldContext<FarmEntity>, IEnumerable<MilkTestEntity>>) PickupssResolveFunction);
			AddNavigationConnectionField("PickupssConnection", PickupssResolveFunction);

			// GraphQL many to many reference to entity  via reference Farms
			IEnumerable<FarmersFarms> FarmerssResolveFunction(ResolveFieldContext<FarmEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<FarmersFarms>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.Farmerss.Where(filter.Compile());
			}
			AddNavigationListField("Farmerss", (Func<ResolveFieldContext<FarmEntity>, IEnumerable<FarmersFarms>>) FarmerssResolveFunction);
			AddNavigationConnectionField("FarmerssConnection", FarmerssResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class FarmEntityInputType : InputObjectGraphType<FarmEntity>
	{
		public FarmEntityInputType()
		{
			Name = "FarmEntityInput";
			Description = "The input object for adding a new FarmEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Code");
			Field<StringGraphType>("Name");
			Field<EnumerationGraphType<State>>("State");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<MilkTestEntityInputType>>("Pickupss");
			Field<ListGraphType<FarmersFarmsInputType>>("Farmerss");

		}
	}

}