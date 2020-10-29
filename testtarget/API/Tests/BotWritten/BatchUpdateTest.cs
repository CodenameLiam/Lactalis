

using System;
using System.Linq;
using System.Net;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using APITests.Factories;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	public class BatchUpdateTest :  IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;
		private static CreateApiTests _createApiTests;

		public BatchUpdateTest(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
			_createApiTests = new CreateApiTests(new StartupTestFixture(), _output);
		}

		#region GraphQl Batch Update
		[SkippableTheory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityFactoryMultipleTheoryData))]
		public void GraphqlBatchUpdateEntities(EntityFactory entityFactory, int numEntities)
		{
			var entity = entityFactory.Construct();
			var entityProperties = entity.GetType().GetProperties();

			if (entityProperties.Any(x => x.PropertyType.IsEnum) || entity.HasFile)
			{
				throw new SkipException("Batch update is currently not supported on entities with enum or file");
			}

			var entityList = entityFactory.ConstructAndSave(_output, numEntities);

			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{_configure.BaseUrl}/api/graphql")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			//get the authorization token and adds the token to the request
			var loginToken = new LoginToken(_configure.BaseUrl, _configure.SuperUsername, _configure.SuperPassword);
			var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
			request.AddHeader("Authorization", authorizationToken);

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*\\*");

			// form the query to update the entity
			var updateQuery = QueryBuilder.BatchUpdateEntityQueryBuilder(entityList);
			request.AddParameter("text/json", updateQuery, ParameterType.RequestBody);

			// mass update all entities in the list in a single request and check status code is ok
			RequestHelpers.ValidateResponse(client, Method.POST, request, HttpStatusCode.OK);
		}
		#endregion
	}
}