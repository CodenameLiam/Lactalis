
using System;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;



namespace APITests.Utils
{
	public class LoginToken
	{
		public string TokenType { get; set; }
		public string AccessToken { get; set; }
		public string ExpiresIn { get; set; }

		public LoginToken(string baseUrl, string username, string password)
		{
			var client = new RestClient {BaseUrl = new Uri(baseUrl + "/api/authorization/connect/token")};

			//setup the request
			var request = new RestRequest {Method = Method.POST, RequestFormat = DataFormat.Json};

			//add to the body the email and password for the register request
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

			//set the parameter to use the correct content type, username, password and grant type2
			request.AddParameter("application/x-www-form-urlencoded",
				$"username={username}&password={password}&grant_type=password",
				ParameterType.RequestBody);

			// execute the request
			var response = client.Execute(request);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				var invalidResponse = JObject.Parse(response.Content);
				throw new Exception(invalidResponse["error_description"].ToString());
			}

			var loginResponse = JObject.Parse(response.Content);
			TokenType = loginResponse["token_type"].ToString();
			AccessToken = loginResponse["access_token"].ToString();
			ExpiresIn = loginResponse["expires_in"].ToString();
		}
	
	}
}
