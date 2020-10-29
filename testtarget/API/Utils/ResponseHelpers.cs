

using System;
using System.Net;
using RestSharp;
using Xunit;

namespace APITests.Utils
{
	internal static class ResponseHelpers
	{
		public static void CheckResponse(RestClient client, RestRequest request, bool expectValid)
		{
			// execute the request
			var response = client.Execute(request);

			//check the response is valid
			var validResponse = response.StatusCode == HttpStatusCode.OK;

			//valid ids returned and a valid response
			Assert.Equal(expectValid, validResponse);
		}
	}
}