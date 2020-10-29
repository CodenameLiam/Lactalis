
using System;
using System.Net;
using RestSharp;

namespace APITests.Utils
{
	public static class PingServer
	{
		/// <summary>
		/// Pings the server to determine if it can get a connection
		/// </summary>
		public static void TestConnection(string url)
		{
			//setup the rest client
			var client = new RestClient
			{
				BaseUrl = new Uri($"{url}")
			};

			//setup the request
			var request = new RestRequest
			{
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			// execute the request
			var response = client.Execute(request);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception($"Could not reach {url}, Response Code: {response.StatusCode}");
			}
		}
	}
}