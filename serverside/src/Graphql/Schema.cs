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
using System.Threading.Tasks;
using Lactalis.Graphql.Fields;
using Lactalis.Graphql.Helpers;
using Lactalis.Graphql.Types;
using Lactalis.Models;
using Lactalis.Models.RegistrationModels;
using GraphQL;
using GraphQL.EntityFramework;
using GraphQL.Types;
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Lactalis.Graphql
{
	/// <summary>
	/// The GraphQL schema class for fetching and mutating data
	/// </summary>
	public class LactalisSchema : Schema
	{
		public LactalisSchema(IDependencyResolver resolver) : base(resolver)
		{
			Query = resolver.Resolve<LactalisQuery>();
			Mutation = resolver.Resolve<LactalisMutation>();
			// % protected region % [Add any extra schema constructor options here] off begin
			// % protected region % [Add any extra schema constructor options here] end
		}

		// % protected region % [Add any schema methods here] off begin
		// % protected region % [Add any schema methods here] end
	}

	/// <summary>
	/// The query class for the GraphQL schema
	/// </summary>
	public class LactalisQuery : QueryGraphType<LactalisDBContext>
	{
		private const string WhereDesc = "A list of where conditions that are joined with an AND";
		private const string ConditionalWhereDesc = "A list of lists of where conditions. The conditions inside the " +
													"innermost lists are joined with and OR and the results of those " +
													"lists are joined with an AND";

		public LactalisQuery(IEfGraphQLService<LactalisDBContext> efGraphQlService) : base(efGraphQlService)
		{
			// Add query types for each entity
			AddModelQueryField<NewsArticleEntityType, NewsArticleEntity>("NewsArticleEntity");
			AddModelQueryField<AdminEntityType, AdminEntity>("AdminEntity");
			AddModelQueryField<FarmerEntityType, FarmerEntity>("FarmerEntity");
			AddModelQueryField<FarmEntityType, FarmEntity>("FarmEntity");
			AddModelQueryField<MilkTestEntityType, MilkTestEntity>("MilkTestEntity");

			// Add query types for each many to many reference
			AddModelQueryField<FarmersFarmsType, FarmersFarms>("FarmersFarms");

			// % protected region % [Add any extra query config here] off begin
			// % protected region % [Add any extra query config here] end
		}

		/// <summary>
		/// Adds single, multiple and connection queries to query
		/// </summary>
		/// <typeparam name="TModelType">The GraphQL type for returning data</typeparam>
		/// <typeparam name="TModel">The EF model type for querying the DB</typeparam>
		/// <param name="name">The name of the entity</param>
		public void AddModelQueryField<TModelType, TModel>(string name)
			where TModelType : ObjectGraphType<TModel>
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Override single query here] off begin
			AddQueryField(
				$"{name}s",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType)).Description = $"Query for fetching multiple {name}s";
			// % protected region % [Override single query here] end

			// % protected region % [Override multiple query here] off begin
			AddSingleField(
				name: name,
				resolve: QueryHelpers.CreateResolveFunction<TModel>(),
				graphType: typeof(TModelType)).Description = $"Query for fetching a single {name}";
			// % protected region % [Override multiple query here] end

			// % protected region % [Override connection query here] off begin
			AddQueryConnectionField(
				$"{name}sConnection",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType));
			// % protected region % [Override connection query here] end

			// % protected region % [Override count query here] off begin
			FieldAsync<NumberObjectType>(
				$"count{name}s",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<WhereExpressionGraph>>
					{
						Name = "where",
						Description = WhereDesc
					}
				),
				resolve: CountQuery.CreateCountQuery<TModel>(),
				description: "Counts the number of models according to a given set of conditions"
			);
			// % protected region % [Override count query here] end

			// % protected region % [Override conditional query here] off begin
			AddQueryField(
				$"{name}sConditional",
				ConditionalQuery.CreateConditionalQuery<TModel>(),
				typeof(TModelType),
				new QueryArguments(
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				)
			);
			// % protected region % [Override conditional query here] end

			// % protected region % [Override count conditional query here] off begin
			FieldAsync<NumberObjectType>(
				$"count{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				),
				resolve: CountQuery.CreateConditionalCountQuery<TModel>(),
				description: "Counts the number of models according to a given set of conditions. This query can " +
							"perform both AND and OR conditions"
			);
			// % protected region % [Override count conditional query here] end

			// % protected region % [Add any extra per entity fields here] off begin
			// % protected region % [Add any extra per entity fields here] end
		}

		// % protected region % [Add any extra query methods here] off begin
		// % protected region % [Add any extra query methods here] end
	}

	/// <summary>
	/// The mutation class for the GraphQL schema
	/// </summary>
	public class LactalisMutation : ObjectGraphType<object>
	{
		private const string ConditionalWhereDesc = "A list of lists of where conditions. The conditions inside the " +
											"innermost lists are joined with and OR and the results of those " +
											"lists are joined with an AND";

		public LactalisMutation()
		{
			Name = "Mutation";

			// Add input types for each entity
			AddMutationField<NewsArticleEntityInputType, NewsArticleEntityInputType, NewsArticleEntityType, NewsArticleEntity>("NewsArticleEntity");
			AddMutationField<AdminEntityCreateInputType, AdminEntityInputType, AdminEntityType, AdminEntity>(
				"AdminEntity",
				CreateMutation.CreateUserCreateMutation<AdminEntity, AdminEntityRegistrationModel, AdminEntityGraphQlRegistrationModel>("AdminEntity"));
			AddMutationField<FarmerEntityCreateInputType, FarmerEntityInputType, FarmerEntityType, FarmerEntity>(
				"FarmerEntity",
				CreateMutation.CreateUserCreateMutation<FarmerEntity, FarmerEntityRegistrationModel, FarmerEntityGraphQlRegistrationModel>("FarmerEntity"));
			AddMutationField<FarmEntityInputType, FarmEntityInputType, FarmEntityType, FarmEntity>("FarmEntity");
			AddMutationField<MilkTestEntityInputType, MilkTestEntityInputType, MilkTestEntityType, MilkTestEntity>("MilkTestEntity");

			// Add input types for each many to many reference
			AddMutationField<FarmersFarmsInputType, FarmersFarmsInputType, FarmersFarmsType, FarmersFarms>("FarmersFarms");

			// % protected region % [Add any extra mutation queries here] off begin
			// % protected region % [Add any extra mutation queries here] end
		}

		/// <summary>
		/// Adds the required mutation fields to the GraphQL schema for create, update and delete
		/// </summary>
		/// <typeparam name="TModelCreateInputType">The GraphQL input type used for the create functions</typeparam>
		/// <typeparam name="TModelUpdateInputType">The GraphQL Input Type used for the update functions</typeparam>
		/// <typeparam name="TModelType">The GraphQL model type used for returning data</typeparam>
		/// <typeparam name="TModel">The EF model type for saving to the DB</typeparam>
		/// <param name="name">The name of the entity</param>
		/// <param name="createMutation">An override for the create mutation</param>
		/// <param name="updateMutation">An override for the update mutation</param>
		/// <param name="deleteMutation">An override for the delete mutation</param>
		/// <param name="conditionalUpdateMutation">An override for the conditional update mutation</param>
		/// <param name="conditionalDeleteMutation">An override for the conditional delete mutation</param>
		public void AddMutationField<TModelCreateInputType, TModelUpdateInputType, TModelType, TModel>(
			string name,
			Func<ResolveFieldContext<object>, Task<object>> createMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> updateMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> deleteMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> conditionalUpdateMutation = null,
			Func<ResolveFieldContext<object>, Task<object>> conditionalDeleteMutation = null)
			where TModelCreateInputType : InputObjectGraphType<TModel>
			where TModelUpdateInputType : InputObjectGraphType<TModel>
			where TModelType : ObjectGraphType<TModel>
			where TModel : class, IOwnerAbstractModel, new()
		{
			// % protected region % [Override create mutation here] off begin
			FieldAsync<ListGraphType<TModelType>>(
				$"create{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelCreateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: createMutation ?? CreateMutation.CreateCreateMutation<TModel>(name)
			);
			// % protected region % [Override create mutation here] end

			// % protected region % [Override update mutation here] off begin
			FieldAsync<ListGraphType<TModelType>>(
				$"update{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelUpdateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: updateMutation ?? UpdateMutation.CreateUpdateMutation<TModel>(name)
			);
			// % protected region % [Override update mutation here] end

			// % protected region % [Override delete mutation here] off begin
			FieldAsync<ListGraphType<IdObjectType>>(
				$"delete{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<IdGraphType>> { Name = $"{name}Ids" }
				),
				resolve: deleteMutation ?? DeleteMutation.CreateDeleteMutation<TModel>(name)
			);
			// % protected region % [Override delete mutation here] end

			// % protected region % [Override update conditional mutation here] off begin
			FieldAsync<BooleanObjectType>(
				$"update{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					},
					new QueryArgument<TModelUpdateInputType> { Name = "valuesToUpdate" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "fieldsToUpdate" }
				),
				resolve: conditionalUpdateMutation ?? UpdateMutation.CreateConditionalUpdateMutation<TModel>(name)
			);
			// % protected region % [Override update conditional mutation here] end

			// % protected region % [Override delete conditional mutation here] off begin
			FieldAsync<BooleanObjectType>(
				$"delete{name}sConditional",
				arguments: new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "id" },
					new QueryArgument<ListGraphType<IdGraphType>> { Name = "ids" },
					new QueryArgument<ListGraphType<ListGraphType<WhereExpressionGraph>>>
					{
						Name = "conditions",
						Description = ConditionalWhereDesc
					}
				),
				resolve: conditionalDeleteMutation ?? DeleteMutation.CreateConditionalDeleteMutation<TModel>(name)
			);
			// % protected region % [Override delete conditional mutation here] end

			// % protected region % [Add any extra per entity mutations here] off begin
			// % protected region % [Add any extra per entity mutations here] end
		}

		// % protected region % [Add any extra mutation methods here] off begin
		// % protected region % [Add any extra mutation methods here] end
	}
}