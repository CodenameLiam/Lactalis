
using System;
using System.Net;
using APITests.Setup;
using APITests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	public class CookieAuthTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public CookieAuthTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Fact]
		public void ValidCookiesValidXSRFTokenAuth()
		{
			// get a paired client and xsrf token
			var clientxsrf = ClientXsrf.GetValidClientAndxsrfTokenPair(_configure);

			// extract the client
			var client = clientxsrf.client;

			// extract the xsrf token
			var xsrfToken = clientxsrf.xsrfToken;

			// set the uri for the authorised api request
			client.BaseUrl = new Uri($"{_configure.BaseUrl}/api/account/me");

			//setup the request
			var request = RequestHelpers.BasicPostRequest();

			//get the authorization token and adds the token to the request
			request.AddHeader("X-XSRF-TOKEN", xsrfToken);

			// we expect out result to be valid since we have valid cookies and a valid xsrfToken as a header
			// check response
			ResponseHelpers.CheckResponse(client, request, expectValid: true);
		}

		[Fact]
		public void MissingXSRFTokenValidCookiesUnauthTest()
		{
			// get a paired client and xsrf token
			var clientxsrf = ClientXsrf.GetValidClientAndxsrfTokenPair(_configure);

			// extract the client
			var client = clientxsrf.client;

			// extract the xsrf token
			var xsrfToken = clientxsrf.xsrfToken;

			// set the uri for the authorised api request
			client.BaseUrl = new Uri($"{_configure.BaseUrl}/api/account/me");

			//setup the request
			var request = RequestHelpers.BasicPostRequest();

			// we don't expect out result to be valid since we have not attatched a valid
			// xsrf token as a header, although we do have valid cookies
			ResponseHelpers.CheckResponse(client, request, expectValid: false);
		}

		[Fact]
		public void MissingCookiesValidXSRFTokenUnauthTest()
		{
			// get a paired client and xsrf token
			var clientxsrf = ClientXsrf.GetValidClientAndxsrfTokenPair(_configure);

			// extract the client
			var client = clientxsrf.client;

			// give client empty cookie containter
			client.CookieContainer = new System.Net.CookieContainer();

			// extract the xsrf token
			var xsrfToken = clientxsrf.xsrfToken;

			// set the uri for the authorised api request
			client.BaseUrl = new Uri($"{_configure.BaseUrl}/api/account/me");

			//setup the request
			var request = RequestHelpers.BasicPostRequest();

			//get the authorization token and adds the token to the request
			request.AddHeader("X-XSRF-TOKEN", xsrfToken);

			// we don't expect out result to be valid as we have no valid cookies,
			// even though we have attatched a valid xsrf token as a header.
			// check response
			ResponseHelpers.CheckResponse(client, request, expectValid: false);
		}
	}
}
