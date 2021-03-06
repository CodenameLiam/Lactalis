
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
	public class NewsArticleEntityType : EfObjectGraphType<LactalisDBContext, NewsArticleEntity>
	{
		public NewsArticleEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Headline, type: typeof(StringGraphType));
			Field(o => o.Description, type: typeof(StringGraphType));
			Field(o => o.FeatureImageId, type: typeof(IdGraphType));
			Field(o => o.Content, type: typeof(StringGraphType));
			Field(o => o.Qld, type: typeof(BooleanGraphType));
			Field(o => o.Nsw, type: typeof(BooleanGraphType));
			Field(o => o.Vic, type: typeof(BooleanGraphType));
			Field(o => o.Tas, type: typeof(BooleanGraphType));
			Field(o => o.Wa, type: typeof(BooleanGraphType));
			Field(o => o.Sa, type: typeof(BooleanGraphType));
			Field(o => o.Nt, type: typeof(BooleanGraphType));

			// Add entity references
			Field(o => o.PromotedArticlesId, type: typeof(IdGraphType));

			// GraphQL reference to entity PromotedArticlesEntity via reference PromotedArticles
			AddNavigationField("PromotedArticles", context => {
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<PromotedArticlesEntity>(
					graphQlContext.IdentityService,
					graphQlContext.UserManager,
					graphQlContext.DbContext,
					graphQlContext.ServiceProvider);
				var value = context.Source.PromotedArticles;

				if (value != null)
				{
					return new List<PromotedArticlesEntity> {value}.All(filter.Compile()) ? value : null;
				}
				return null;
			});

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class NewsArticleEntityInputType : InputObjectGraphType<NewsArticleEntity>
	{
		public NewsArticleEntityInputType()
		{
			Name = "NewsArticleEntityInput";
			Description = "The input object for adding a new NewsArticleEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Headline");
			Field<StringGraphType>("Description");
			Field(o => o.FeatureImageId, type: typeof(IdGraphType));
			Field<StringGraphType>("Content");
			Field<BooleanGraphType>("Qld");
			Field<BooleanGraphType>("Nsw");
			Field<BooleanGraphType>("Vic");
			Field<BooleanGraphType>("Tas");
			Field<BooleanGraphType>("Wa");
			Field<BooleanGraphType>("Sa");
			Field<BooleanGraphType>("Nt");

			// Add entity references
			Field<IdGraphType>("PromotedArticlesId");

			// Add references to foreign models to allow nested creation
			Field<PromotedArticlesEntityInputType>("PromotedArticles");

		}
	}

}