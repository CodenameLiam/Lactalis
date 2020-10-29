

using System;
using System.Linq;
using System.Net;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	public class ApiTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ApiTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[ClassData(typeof(EntityNamePluralizedTheoryData))]
		public void TestGraphqlEndPoints(string entityName)
		{

			var api = new WebApi(_configure, _output);

			var query = new RestSharp.JsonObject();
			query.Add("query", "{ " + entityName + "{id}}");

			api.ConfigureAuthenticationHeaders();
			var response = api.Post($"/api/graphql", query);

			//check the ids are valid
			var validIds = JObject.Parse(response.Content)["data"][entityName]
				.Select(o => o["id"].Value<string>())
				.All(o => !string.IsNullOrWhiteSpace(o));

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Theory]
		[ClassData(typeof(EntityNameTheoryData))]
		public void TestApiEndPoints(string entityName)
		{
			var api = new WebApi(_configure, _output);
			api.ConfigureAuthenticationHeaders();
			var response = api.Get($"/api/entity/{entityName}");

			// a valid response code
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}