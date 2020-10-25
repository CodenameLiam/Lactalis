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

			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

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

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
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

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}
}