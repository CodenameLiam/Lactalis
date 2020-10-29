
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
	public class FarmersFarmsType : EfObjectGraphType<LactalisDBContext, FarmersFarms>
	{
		public FarmersFarmsType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));

			Field(o => o.FarmersId, type: typeof(IdGraphType));
			Field(o => o.FarmsId, type: typeof(IdGraphType));


			// GraphQL reference to entity FarmerEntity via reference FarmerEntity
			AddNavigationField("Farmers", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<FarmerEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Farmers;
				return new List<FarmerEntity> {value}.All(filter.Compile()) ? value : null;
			});

			// GraphQL reference to entity FarmEntity via reference FarmEntity
			AddNavigationField("Farms", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<FarmEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Farms;
				return new List<FarmEntity> {value}.All(filter.Compile()) ? value : null;
			});

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class FarmersFarmsInputType : InputObjectGraphType<FarmersFarms>
	{
		public FarmersFarmsInputType()
		{
			Name = "FarmersFarmsInput";
			Description = "The input object for adding a new FarmersFarms";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<IdGraphType>("FarmersId");
			Field<IdGraphType>("FarmsId");

			// Add references to foreign objects
			Field<FarmerEntityInputType>("Farmers");
			Field<FarmEntityInputType>("Farms");

		}
	}
}