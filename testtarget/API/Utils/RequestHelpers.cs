

using System;
using RestSharp;
using System.Net;
using Xunit;
using Xunit.Abstractions;



namespace APITests.Utils
{
	internal static class RequestHelpers
	{


		public static RestRequest BasicGetRequest()
		{
			//setup the request
			var request = new RestRequest { Method = Method.GET, RequestFormat = DataFormat.Json };

			//get the authorization token and adds the token to the request
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text/html, */*");

			return request;
		}

		public static RestRequest BasicPostRequest()
		{
			//setup the request
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };

			//get the authorization token and adds the token to the request
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "application/json, text/html, */*");

			return request;
		}

		public static void ValidateResponse(RestClient client, Method method, RestRequest request, HttpStatusCode expectedResponse)
		{
			request.Method = method;
			var response = client.Execute(request);
			Assert.Equal(expectedResponse, response.StatusCode);
		}

		public static void SendPostRequest(string uri, RestSharp.JsonObject query, ITestOutputHelper output)
		{
			var client = new RestClient {BaseUrl = new Uri(uri)};
			var request = new RestRequest {Method = Method.POST, RequestFormat = DataFormat.Json};
			request.AddParameter("application/json", query, ParameterType.RequestBody);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Accept", "*\\*");
			var response = client.Execute(request);

			ApiOutputHelper.WriteRequestResponseOutput(request, response, output);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

	}
}