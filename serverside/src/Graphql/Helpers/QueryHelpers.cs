
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Graphql.Types;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services;
using GraphQL.EntityFramework;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Lactalis.Graphql.Helpers
{
	public class QueryHelpers
	{
		/// <summary>
		/// Creates a resolve function that returns a list a queryable of models.
		/// This respects the security settings and properly applies the auditing context
		/// </summary>
		/// <typeparam name="TModel">The type of the model to create the function for</typeparam>
		/// <returns>A function that takes a graphql context and returns a queryable of models</returns>
		public static Func<ResolveFieldContext<object>, IQueryable<TModel>> CreateResolveFunction<TModel>()
			where TModel : class, IOwnerAbstractModel, new()
		{
			return context =>
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var crudService = graphQlContext.CrudService;
				var auditFields = AuditReadData.FromGraphqlContext(context);
				return crudService.Get<TModel>(auditFields: auditFields).AsNoTracking();
			};
		}

		/// <summary>
		/// Creates a conditional where statement that handles both AND and OR conditions
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		/// <see cref="QueryableExtensions.AddConditionalWhereFilter{T}"/>
		public static IQueryable<T> CreateConditionalWhere<T>(
			ResolveFieldContext<object> context,
			IQueryable<T> models,
			string argName = "conditions")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var wheres = context.GetArgument<List<List<WhereExpression>>>(argName);
				return models.AddConditionalWhereFilter(wheres);
			}

			return models;
		}

		/// <summary>
		/// Creates a where statement where all the conditions joined by an AND
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateWhereCondition<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "where")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var wheres = context.GetArgument<List<WhereExpression>>(argName);
				return models.AddWhereFilter(wheres);
			}

			return models;
		}

		/// <summary>
		/// Creates a condition where the model matches the given id
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateIdCondition<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "id")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var id = context.GetArgument<Guid>(argName);
				models = models.Where(model => model.Id == id);
			}

			return models;
		}

		/// <summary>
		/// Creates a condition where the model matches a set of ids
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateIdsCondition<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "ids")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var ids = context.GetArgument<List<Guid>>(argName);
				models = models.Where(model => ids.Contains(model.Id));
			}

			return models;
		}

		/// <summary>
		/// Applies a skip condition to a query
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateSkip<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "skip")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var skip = context.GetArgument<int>(argName);
				models = models.Skip(skip);
			}

			return models;
		}

		/// <summary>
		/// Applies a take condition to a query
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateTake<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "take")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var take = context.GetArgument<int>(argName);
				models = models.Take(take);
			}

			return models;
		}

		/// <summary>
		/// Orders a set of models by a list of order by conditions. The order by conditions are applied first to last.
		/// </summary>
		/// <param name="context">The graphql context to of the query</param>
		/// <param name="models">A queryable to apply the condition over</param>
		/// <param name="argName">The name of the graphql arg to fetch from the context</param>
		/// <typeparam name="T">The type of the model to apply the conditional over</typeparam>
		/// <returns>A new queryable that has the conditions applied</returns>
		public static IQueryable<T> CreateOrderBy<T>(ResolveFieldContext<object> context, IQueryable<T> models, string argName = "orderBy")
			where T : IOwnerAbstractModel
		{
			if (context.HasArgument(argName))
			{
				var orderBys = context.GetArgument<List<OrderBy>>(argName);
				return models.AddOrderBys(orderBys);
			}

			return models;
		}
	}
}