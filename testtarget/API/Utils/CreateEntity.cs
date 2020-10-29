

using System;
using System.Collections.Generic;
using System.Net;
using APITests.Setup;
using APITests.EntityObjects.Models;
using RestSharp;
using Xunit;

namespace APITests.Utils
{
	internal static class CreateEntityUtil
	{
		public static void CreateEntities(List<BaseEntity> entityList)
		{
			var configure = new StartupTestFixture();

			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{configure.BaseUrl}/api/graphql")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			//get the authorization token and adds the token to the request
			var loginToken = new LoginToken(configure.BaseUrl, configure.SuperUsername, configure.SuperPassword);
			var authorizationToken = $"{loginToken.TokenType} {loginToken.AccessToken}";
			request.AddHeader("Authorization", authorizationToken);

			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*\\*");

			var query = QueryBuilder.CreateEntityQueryBuilder(entityList);

			request.AddParameter("text/json", query, ParameterType.RequestBody);

			// execute the request
			var response = client.Execute(request);

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}