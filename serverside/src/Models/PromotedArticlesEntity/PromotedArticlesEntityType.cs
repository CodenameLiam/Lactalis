
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
	public class PromotedArticlesEntityType : EfObjectGraphType<LactalisDBContext, PromotedArticlesEntity>
	{
		public PromotedArticlesEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Name, type: typeof(StringGraphType));

			// Add entity references

			// GraphQL reference to entity NewsArticleEntity via reference NewsArticles
			IEnumerable<NewsArticleEntity> NewsArticlessResolveFunction(ResolveFieldContext<PromotedArticlesEntity> context)
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var filter = SecurityService.CreateReadSecurityFilter<NewsArticleEntity>(graphQlContext.IdentityService, graphQlContext.UserManager, graphQlContext.DbContext, graphQlContext.ServiceProvider);
				return context.Source.NewsArticless.Where(filter.Compile());
			}
			AddNavigationListField("NewsArticless", (Func<ResolveFieldContext<PromotedArticlesEntity>, IEnumerable<NewsArticleEntity>>) NewsArticlessResolveFunction);
			AddNavigationConnectionField("NewsArticlessConnection", NewsArticlessResolveFunction);

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class PromotedArticlesEntityInputType : InputObjectGraphType<PromotedArticlesEntity>
	{
		public PromotedArticlesEntityInputType()
		{
			Name = "PromotedArticlesEntityInput";
			Description = "The input object for adding a new PromotedArticlesEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Name");

			// Add entity references

			// Add references to foreign models to allow nested creation
			Field<ListGraphType<NewsArticleEntityInputType>>("NewsArticless");

		}
	}

}