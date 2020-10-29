
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lactalis.Graphql.Helpers;
using Lactalis.Graphql.Types;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services;
using GraphQL;
using GraphQL.Types;

namespace Lactalis.Graphql.Fields
{
	public class DeleteMutation
	{
		/// <summary>
		/// Creates a mutation that will delete things from the database
		/// </summary>
		/// <param name="name">The name of the model to delete</param>
		/// <typeparam name="TModel">The type of the model to delete</typeparam>
		/// <returns>A function that takes a graphql context and returns a list the deleted ids</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateDeleteMutation<TModel>(string name)
			where TModel : class, IOwnerAbstractModel, new()
		{
			return async context =>
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var crudService = graphQlContext.CrudService;
				var ids = context.GetArgument<List<Guid>>($"{name}Ids".ToCamelCase());

				try
				{
					if (ids == null)
					{
						throw new AggregateException(new Exception("No ids provided to delete, aborting!"));
					}

					var deletedIds = await crudService.Delete<TModel>(ids);
					return IdObject.FromList(deletedIds);
				}
				catch (AggregateException exception)
				{
					context.Errors.AddRange(
						exception.InnerExceptions.Select(error => new ExecutionError(error.Message)));
					return new List<TModel>();
				}
			};
		}

		/// <summary>
		/// Creates a mutation that will delete things from the database by a where condition
		/// </summary>
		/// <param name="name">The name of the model to delete</param>
		/// <typeparam name="TModel">The type of the model to delete</typeparam>
		/// <returns>A function that takes a graphql context and returns whether the delete is successful</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateConditionalDeleteMutation<TModel>(string name)
			where TModel : class, IOwnerAbstractModel, new()
		{
			return async context =>
			{
				var graphQlContext = (LactalisGraphQlContext)context.UserContext;
				var crudService = graphQlContext.CrudService;
				var user = graphQlContext.User;
				var dbSet = graphQlContext.DbContext.Set<TModel>().AsQueryable();

				var models = QueryHelpers.CreateConditionalWhere(context, dbSet);
				models = QueryHelpers.CreateIdsCondition(context, models);
				models = QueryHelpers.CreateIdCondition(context, models);

				try
				{
					return await crudService.ConditionalDelete(models);
				}
				catch (AggregateException exception)
				{
					context.Errors.AddRange(
						exception.InnerExceptions.Select(error => new ExecutionError(error.Message)));
					return false;
				}
			};
		}
	}
}