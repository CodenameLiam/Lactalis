
using System;
using System.Linq;
using Lactalis.Graphql.Helpers;
using Lactalis.Models;
using GraphQL.Types;

namespace Lactalis.Graphql.Fields
{
	public class ConditionalQuery
	{
		/// <summary>
		/// Creates a resolve function for a query that can both AND and OR conditions together
		/// </summary>
		/// <typeparam name="TModel">The type of model to create the query for</typeparam>
		/// <returns>A function that takes a graphql context and returns a list of models</returns>
		public static Func<ResolveFieldContext<object>, IQueryable<TModel>> CreateConditionalQuery<TModel>()
			where TModel : class, IOwnerAbstractModel, new()
		{
			return context =>
			{
				// Fetch the models that we need
				var models = QueryHelpers.CreateResolveFunction<TModel>()(context);

				// Apply the conditions to the query
				models = QueryHelpers.CreateConditionalWhere(context, models);

				return models;
			};
		}
	}
}