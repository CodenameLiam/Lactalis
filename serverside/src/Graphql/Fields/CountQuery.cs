
using System;
using System.Threading.Tasks;
using Lactalis.Graphql.Helpers;
using Lactalis.Graphql.Types;
using Lactalis.Models;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Lactalis.Graphql.Fields
{
	public class CountQuery
	{
		/// <summary>
		/// Creates a query that counts the number of models that comply to a set of conditions
		/// </summary>
		/// <typeparam name="TModel">The type of model to count</typeparam>
		/// <returns>A function that takes a graphql context and returns a count of models that satisfy the condition</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateCountQuery<TModel>()
			where TModel : class, IOwnerAbstractModel, new()
		{
			return async context =>
			{
				// Fetch the models that we need
				var models = QueryHelpers.CreateResolveFunction<TModel>()(context);

				// Apply conditions to the query
				models = QueryHelpers.CreateWhereCondition(context, models);
				models = QueryHelpers.CreateIdsCondition(context, models);
				models = QueryHelpers.CreateIdCondition(context, models);

				return new NumberObject {Number = await models.CountAsync()};
			};
		}

		/// <summary>
		/// Creates a query that counts the number of models that comply to a set of conditions.
		/// This query can perform both AND and OR conditions.
		/// </summary>
		/// <typeparam name="TModel">The type of model to count</typeparam>
		/// <returns>A function that takes a graphql context and returns a list of models</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateConditionalCountQuery<TModel>()
			where TModel : class, IOwnerAbstractModel, new()
		{
			return async context =>
			{
				// Fetch the models that we need
				var models = QueryHelpers.CreateResolveFunction<TModel>()(context);

				// Apply conditions to the query
				models = QueryHelpers.CreateConditionalWhere(context, models);
				models = QueryHelpers.CreateIdsCondition(context, models);
				models = QueryHelpers.CreateIdCondition(context, models);

				return new NumberObject {Number = await models.CountAsync()};
			};
		}
	}
}