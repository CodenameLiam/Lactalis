/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Enums;
using Lactalis.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

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
			Field(o => o.Name, type: typeof(StringGraphType));
			Field(o => o.State, type: typeof(EnumerationGraphType<State>));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

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

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
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
			Field<StringGraphType>("Name");
			Field<EnumerationGraphType<State>>("State");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<MilkTestEntityInputType>>("Pickupss");
			Field<ListGraphType<FarmersFarmsInputType>>("Farmerss");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}