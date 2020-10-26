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
	public class TradingPostListingEntityType : EfObjectGraphType<LactalisDBContext, TradingPostListingEntity>
	{
		public TradingPostListingEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Title, type: typeof(StringGraphType));
			Field(o => o.Email, type: typeof(StringGraphType));
			Field(o => o.Phone, type: typeof(StringGraphType));
			Field(o => o.AdditionalInfo, type: typeof(StringGraphType));
			Field(o => o.AddressLine1, type: typeof(StringGraphType));
			Field(o => o.AddressLine2, type: typeof(StringGraphType));
			Field(o => o.PostalCode, type: typeof(StringGraphType));
			Field(o => o.ProductImageId, type: typeof(IdGraphType));
			Field(o => o.Price, type: typeof(IntGraphType));
			Field(o => o.PriceType, type: typeof(EnumerationGraphType<PriceType>));
			// % protected region % [Add any extra GraphQL fields here] off begin
			// % protected region % [Add any extra GraphQL fields here] end

			// Add entity references
			Field(o => o.FarmerId, type: typeof(IdGraphType));

			// GraphQL reference to entity FarmerEntity via reference Farmer
			AddNavigationField("Farmer", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<FarmerEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.Farmer;

				if (value != null)
				{
					return new List<FarmerEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

			// GraphQL many to many reference to entity TradingPostCategoryEntity via reference TradingPostCategories
			IEnumerable<TradingPostListingsTradingPostCategories> TradingPostCategoriessResolveFunction(ResolveFieldContext<TradingPostListingEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<TradingPostListingsTradingPostCategories>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.TradingPostCategoriess.Where(filter.Compile());
			}
			AddNavigationListField("TradingPostCategoriess", (Func<ResolveFieldContext<TradingPostListingEntity>, IEnumerable<TradingPostListingsTradingPostCategories>>) TradingPostCategoriessResolveFunction);
			AddNavigationConnectionField("TradingPostCategoriessConnection", TradingPostCategoriessResolveFunction);

			// % protected region % [Add any extra GraphQL references here] off begin
			// % protected region % [Add any extra GraphQL references here] end
		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class TradingPostListingEntityInputType : InputObjectGraphType<TradingPostListingEntity>
	{
		public TradingPostListingEntityInputType()
		{
			Name = "TradingPostListingEntityInput";
			Description = "The input object for adding a new TradingPostListingEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Title");
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Phone");
			Field<StringGraphType>("AdditionalInfo");
			Field<StringGraphType>("AddressLine1");
			Field<StringGraphType>("AddressLine2");
			Field<StringGraphType>("PostalCode");
			Field(o => o.ProductImageId, type: typeof(IdGraphType));
			Field<IntGraphType>("Price");
			Field<EnumerationGraphType<PriceType>>("PriceType");

			// Add entity references
			Field<IdGraphType>("FarmerId");

			// Add references to foreign models to allow nested creation
			Field<FarmerEntityInputType>("Farmer");
			Field<ListGraphType<TradingPostListingsTradingPostCategoriesInputType>>("TradingPostCategoriess");

			// % protected region % [Add any extra GraphQL input fields here] off begin
			// % protected region % [Add any extra GraphQL input fields here] end
		}
	}

}