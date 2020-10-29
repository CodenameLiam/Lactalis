

using System.Linq;
using RestSharp;
using Xunit.Abstractions;

namespace APITests.Utils
{
	/// <summary>
	/// Contains helper methods for writing to the TestOutputHelper in api tests
	/// </summary>
	public static class ApiOutputHelper
	{
		public static void WriteRequestOutput(RestRequest request, ITestOutputHelper testOutputHelper)
		{
			var requestHeaders = request.Parameters.Select(x => $"{x.Name}: {x.Value} - {x.Type}");
			testOutputHelper.WriteLine($"Request:\n{string.Join("\n", requestHeaders)}\n\n");
		}

		public static void WriteResponseOutput(IRestResponse response, ITestOutputHelper testOutputHelper)
		{
			var responseHeaders = response.Headers.Select(x => $"{x.Name}: {x.Value}");
			testOutputHelper.WriteLine($"Response Headers:\n{string.Join("\n", responseHeaders)}\n\n");
			testOutputHelper.WriteLine($"Response Content:\n{response.Content}");
		}

		public static void WriteRequestResponseOutput(
			RestRequest request,
			IRestResponse response,
			ITestOutputHelper testOutputHelper)
		{
			WriteRequestOutput(request, testOutputHelper);
			WriteResponseOutput(response, testOutputHelper);
		}
	}
}
