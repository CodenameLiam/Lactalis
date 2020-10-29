
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

		}
	}

}