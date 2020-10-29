

using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Lactalis.Models;
using APITests.Setup;
using APITests.Utils;
using RestSharp;
using TestDataLib;
using Xunit;

namespace APITests.EntityObjects.Models
{
	public abstract class UserBaseEntity : BaseEntity
	{
		public string EmailAddress = DataUtils.RandEmail();
		public string Password = "abc123A@";
		public string EndpointName;
		private readonly StartupTestFixture _configure = new StartupTestFixture();

		public Guid CreateUser(bool isRegistered = true)
		{
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
			var query = QueryBuilder.CreateEntityQueryBuilder(new List<BaseEntity>{this});
			request.AddParameter("text/json", query, ParameterType.RequestBody);
			var response = client.Execute(request);

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			if (isRegistered)
			{
				// we will confirm their email, they are registered
				var configure = new StartupTestFixture();
				using (var context = new LactalisDBContext(configure.DbContextOptions, null, null))
				{
					context.Users.FirstOrDefault(x => x.UserName == EmailAddress).EmailConfirmed = true;
					context.SaveChanges();
				}
			}
			return Id;
		}
	}
}