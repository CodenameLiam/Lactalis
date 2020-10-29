
using System;
using System.Linq;
using System.Net;
using APITests.Setup;
using Newtonsoft.Json.Linq;
using RestSharp;



namespace APITests.Utils
{
	internal static class ClientXsrf
	{
		public static (RestClient client, string xsrfToken) GetValidClientAndxsrfTokenPair(StartupTestFixture _configure)
		{
			//make a new client
			var client = new RestClient { BaseUrl = new Uri(_configure.BaseUrl + "/api/authorization/login") };

			// setup a cookie container to store cookiers for later
			client.CookieContainer = new System.Net.CookieContainer();

			//setup the request
			var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Json };

			// add header to say what type the content is
			request.AddHeader("Content-Type", "application/json");

			// add valid username and password to the request body
			request.AddJsonBody(new { username = _configure.SuperUsername, password = _configure.SuperPassword });

			// execute the request
			var response = client.Execute(request);

			// check that the response is ok
			if (response.StatusCode != HttpStatusCode.OK)
			{
				var invalidResponse = JObject.Parse(response.Content);
				throw new Exception(invalidResponse["error_description"].ToString());
			}

			// get the returned xsrf token
			var xsrfToken = response.Cookies.FirstOrDefault(cookie => cookie.Name == "XSRF-TOKEN")?.Value;

			// return the client containing cookies and token
			return (client, xsrfToken);
		}
	}
}