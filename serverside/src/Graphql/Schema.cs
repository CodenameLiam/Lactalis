
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
		}

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
			AddModelQueryField<TradingPostListingEntityType, TradingPostListingEntity>("TradingPostListingEntity");
			AddModelQueryField<TradingPostCategoryEntityType, TradingPostCategoryEntity>("TradingPostCategoryEntity");
			AddModelQueryField<AdminEntityType, AdminEntity>("AdminEntity");
			AddModelQueryField<FarmEntityType, FarmEntity>("FarmEntity");
			AddModelQueryField<MilkTestEntityType, MilkTestEntity>("MilkTestEntity");
			AddModelQueryField<FarmerEntityType, FarmerEntity>("FarmerEntity");
			AddModelQueryField<ImportantDocumentCategoryEntityType, ImportantDocumentCategoryEntity>("ImportantDocumentCategoryEntity");
			AddModelQueryField<TechnicalDocumentCategoryEntityType, TechnicalDocumentCategoryEntity>("TechnicalDocumentCategoryEntity");
			AddModelQueryField<QualityDocumentCategoryEntityType, QualityDocumentCategoryEntity>("QualityDocumentCategoryEntity");
			AddModelQueryField<QualityDocumentEntityType, QualityDocumentEntity>("QualityDocumentEntity");
			AddModelQueryField<TechnicalDocumentEntityType, TechnicalDocumentEntity>("TechnicalDocumentEntity");
			AddModelQueryField<ImportantDocumentEntityType, ImportantDocumentEntity>("ImportantDocumentEntity");
			AddModelQueryField<NewsArticleEntityType, NewsArticleEntity>("NewsArticleEntity");
			AddModelQueryField<AgriSupplyDocumentCategoryEntityType, AgriSupplyDocumentCategoryEntity>("AgriSupplyDocumentCategoryEntity");
			AddModelQueryField<SustainabilityPostEntityType, SustainabilityPostEntity>("SustainabilityPostEntity");
			AddModelQueryField<AgriSupplyDocumentEntityType, AgriSupplyDocumentEntity>("AgriSupplyDocumentEntity");
			AddModelQueryField<PromotedArticlesEntityType, PromotedArticlesEntity>("PromotedArticlesEntity");

			// Add query types for each many to many reference
			AddModelQueryField<TradingPostListingsTradingPostCategoriesType, TradingPostListingsTradingPostCategories>("TradingPostListingsTradingPostCategories");
			AddModelQueryField<FarmersFarmsType, FarmersFarms>("FarmersFarms");

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
			AddQueryField(
				$"{name}s",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType)).Description = $"Query for fetching multiple {name}s";

			AddSingleField(
				name: name,
				resolve: QueryHelpers.CreateResolveFunction<TModel>(),
				graphType: typeof(TModelType)).Description = $"Query for fetching a single {name}";

			AddQueryConnectionField(
				$"{name}sConnection",
				QueryHelpers.CreateResolveFunction<TModel>(),
				typeof(TModelType));

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

		}

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
			AddMutationField<TradingPostListingEntityInputType, TradingPostListingEntityInputType, TradingPostListingEntityType, TradingPostListingEntity>("TradingPostListingEntity");
			AddMutationField<TradingPostCategoryEntityInputType, TradingPostCategoryEntityInputType, TradingPostCategoryEntityType, TradingPostCategoryEntity>("TradingPostCategoryEntity");
			AddMutationField<AdminEntityCreateInputType, AdminEntityInputType, AdminEntityType, AdminEntity>(
				"AdminEntity",
				CreateMutation.CreateUserCreateMutation<AdminEntity, AdminEntityRegistrationModel, AdminEntityGraphQlRegistrationModel>("AdminEntity"));
			AddMutationField<FarmEntityInputType, FarmEntityInputType, FarmEntityType, FarmEntity>("FarmEntity");
			AddMutationField<MilkTestEntityInputType, MilkTestEntityInputType, MilkTestEntityType, MilkTestEntity>("MilkTestEntity");
			AddMutationField<FarmerEntityCreateInputType, FarmerEntityInputType, FarmerEntityType, FarmerEntity>(
				"FarmerEntity",
				CreateMutation.CreateUserCreateMutation<FarmerEntity, FarmerEntityRegistrationModel, FarmerEntityGraphQlRegistrationModel>("FarmerEntity"));
			AddMutationField<ImportantDocumentCategoryEntityInputType, ImportantDocumentCategoryEntityInputType, ImportantDocumentCategoryEntityType, ImportantDocumentCategoryEntity>("ImportantDocumentCategoryEntity");
			AddMutationField<TechnicalDocumentCategoryEntityInputType, TechnicalDocumentCategoryEntityInputType, TechnicalDocumentCategoryEntityType, TechnicalDocumentCategoryEntity>("TechnicalDocumentCategoryEntity");
			AddMutationField<QualityDocumentCategoryEntityInputType, QualityDocumentCategoryEntityInputType, QualityDocumentCategoryEntityType, QualityDocumentCategoryEntity>("QualityDocumentCategoryEntity");
			AddMutationField<QualityDocumentEntityInputType, QualityDocumentEntityInputType, QualityDocumentEntityType, QualityDocumentEntity>("QualityDocumentEntity");
			AddMutationField<TechnicalDocumentEntityInputType, TechnicalDocumentEntityInputType, TechnicalDocumentEntityType, TechnicalDocumentEntity>("TechnicalDocumentEntity");
			AddMutationField<ImportantDocumentEntityInputType, ImportantDocumentEntityInputType, ImportantDocumentEntityType, ImportantDocumentEntity>("ImportantDocumentEntity");
			AddMutationField<NewsArticleEntityInputType, NewsArticleEntityInputType, NewsArticleEntityType, NewsArticleEntity>("NewsArticleEntity");
			AddMutationField<AgriSupplyDocumentCategoryEntityInputType, AgriSupplyDocumentCategoryEntityInputType, AgriSupplyDocumentCategoryEntityType, AgriSupplyDocumentCategoryEntity>("AgriSupplyDocumentCategoryEntity");
			AddMutationField<SustainabilityPostEntityInputType, SustainabilityPostEntityInputType, SustainabilityPostEntityType, SustainabilityPostEntity>("SustainabilityPostEntity");
			AddMutationField<AgriSupplyDocumentEntityInputType, AgriSupplyDocumentEntityInputType, AgriSupplyDocumentEntityType, AgriSupplyDocumentEntity>("AgriSupplyDocumentEntity");
			AddMutationField<PromotedArticlesEntityInputType, PromotedArticlesEntityInputType, PromotedArticlesEntityType, PromotedArticlesEntity>("PromotedArticlesEntity");

			// Add input types for each many to many reference
			AddMutationField<TradingPostListingsTradingPostCategoriesInputType, TradingPostListingsTradingPostCategoriesInputType, TradingPostListingsTradingPostCategoriesType, TradingPostListingsTradingPostCategories>("TradingPostListingsTradingPostCategories");
			AddMutationField<FarmersFarmsInputType, FarmersFarmsInputType, FarmersFarmsType, FarmersFarms>("FarmersFarms");

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
			FieldAsync<ListGraphType<TModelType>>(
				$"create{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelCreateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: createMutation ?? CreateMutation.CreateCreateMutation<TModel>(name)
			);

			FieldAsync<ListGraphType<TModelType>>(
				$"update{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<TModelUpdateInputType>> { Name = name + "s" },
					new QueryArgument<ListGraphType<StringGraphType>> { Name = "MergeReferences" }
				),
				resolve: updateMutation ?? UpdateMutation.CreateUpdateMutation<TModel>(name)
			);

			FieldAsync<ListGraphType<IdObjectType>>(
				$"delete{name}",
				arguments: new QueryArguments(
					new QueryArgument<ListGraphType<IdGraphType>> { Name = $"{name}Ids" }
				),
				resolve: deleteMutation ?? DeleteMutation.CreateDeleteMutation<TModel>(name)
			);

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

		}

	}
}