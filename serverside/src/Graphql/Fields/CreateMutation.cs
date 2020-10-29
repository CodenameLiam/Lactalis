
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Models.RegistrationModels;
using Lactalis.Services;
using GraphQL;
using GraphQL.Types;

namespace Lactalis.Graphql.Fields
{
	public class CreateMutation
	{
		/// <summary>
		/// Makes a Create mutation that will save new entities to the database
		/// </summary>
		/// <param name="name">The name of the model to create</param>
		/// <typeparam name="TModel">The type of the model to create</typeparam>
		/// <returns>A function that takes a graphql context and returns a list of created models</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateCreateMutation<TModel>(string name)
			where TModel : class, IOwnerAbstractModel, new()
		{
			return async context =>
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var crudService = graphQlContext.CrudService;
				var models = context.GetArgument<List<TModel>>(name.ToCamelCase() + "s");
				List<string> mergeReferences = null;

				if (context.HasArgument("mergeReferences"))
				{
					mergeReferences = context.GetArgument<List<string>>("mergeReferences");
				}

				try
				{
					if (models == null)
					{
						throw new AggregateException(new Exception("No entities provided to save, aborting!"));
					}

					return await crudService.Create(models, new UpdateOptions
					{
						MergeReferences = mergeReferences,
						Files = graphQlContext.Files,
					});
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
		/// Makes a Create mutation new user entities to the database
		/// </summary>
		/// <param name="name">The name of the model to create</param>
		/// <typeparam name="TModel">The type of the model to create</typeparam>
		/// <typeparam name="TRegisterModel">The type of the registration model</typeparam>
		/// <typeparam name="TGraphQlRegisterModel">The graphql register model</typeparam>
		/// <returns>A function that takes a graphql context and returns a list of created models</returns>
		public static Func<ResolveFieldContext<object>, Task<object>> CreateUserCreateMutation<TModel, TRegisterModel, TGraphQlRegisterModel>(string name)
			where TModel : User, IOwnerAbstractModel, new()
			where TRegisterModel : IRegistrationModel<TModel>
			where TGraphQlRegisterModel : TRegisterModel
		{
			return async context =>
			{
				var graphQlContext = (LactalisGraphQlContext) context.UserContext;
				var crudService = graphQlContext.CrudService;
				var models = context.GetArgument<List<TGraphQlRegisterModel>>(name.ToCamelCase() + "s");
				var mergeReferences = context.GetArgument<List<string>>("mergeReferences");

				try
				{
					if (models == null)
					{
						throw new AggregateException(new Exception("No entities provided to save, aborting!"));
					}

					return await crudService.CreateUser<TModel, TGraphQlRegisterModel>(models,new UpdateOptions
					{
						MergeReferences = mergeReferences,
						Files = graphQlContext.Files,
					});
				}
				catch (AggregateException exception)
				{
					context.Errors.AddRange(
						exception.InnerExceptions.Select(error => new ExecutionError(error.Message)));
					return new List<TModel>();
				}
			};
		}
	}
}