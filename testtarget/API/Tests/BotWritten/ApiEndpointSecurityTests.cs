

using System;
using System.Net;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	public class ApiSecurityTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ApiSecurityTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		/// <summary>
		/// Tests that unauthorized users can access the graphql endpoints.
		/// Should receive a  a HTPP Status Code OK.
		/// </summary>
		/// <param name="entityName"></param>
		[Theory]
		[ClassData(typeof(EntityNamePluralizedTheoryData))]
		public void TestGraphqlEndPointsUnauthorized(string entityName)
		{
			var api = new WebApi(_configure, _output);

			var query = new RestSharp.JsonObject();
			query.Add("query", "{ " + entityName + "{id}}");
			var response = api.Post($"/api/graphql", query);

			// we should get a valid response back
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Theory]
		[ClassData(typeof(EntityNameTheoryData))]
		public void TestApiEndPointsUnauthorized(string entityName)
		{
			var api = new WebApi(_configure, _output);
			var response = api.Get($"/api/entity/{entityName}");

			// valid response code
			Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
		}
	}
}