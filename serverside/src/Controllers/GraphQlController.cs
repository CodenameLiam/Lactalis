
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Services.Interfaces;
using GraphQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
 

namespace Lactalis.Controllers
{
	/// <summary>
	/// The controller that manages all GraphQL operations
	/// </summary>
	[Route("/api/graphql")]
	[ApiController]
	[Authorize(Policy = "AllowVisitorPolicy")]
	public class GraphQlController : Controller
	{
		private class PostBody
		{
			public string OperationName { get; set; }
			public string Query { get; set; }
			public JObject Variables { get; set; }
		}

		private class FormBody
		{
			public PostBody Body { get; set; }
			public IFormFileCollection Files { get; set; }
		}

		private readonly IGraphQlService _graphQlService;
		private readonly IIdentityService _identityService;
		private readonly ILogger<GraphQlController> _logger;

		public GraphQlController(
			IGraphQlService graphQlService,
			IIdentityService identityService,
			ILogger<GraphQlController> logger)
		{
			_graphQlService = graphQlService;
			_identityService = identityService;
			_logger = logger;
		}

		/// <summary>
		/// Executor for GraphQL queries
		/// </summary>
		/// <param name="cancellation">Cancellation token for the operation</param>
		/// <returns>The results for the GraphQL query</returns>
		[HttpPost]
		[RequestSizeLimit(100000000)]
		[Authorize(Policy = "AllowVisitorPolicy")]
		public async Task<ExecutionResult> Post(CancellationToken cancellation)
		{
			await _identityService.RetrieveUserAsync();

			var parsedRequest = await ParsePostBody(cancellation);

			var result = await _graphQlService.Execute(
				parsedRequest.Body.Query,
				parsedRequest.Body.OperationName,
				parsedRequest.Body.Variables.ToInputs(),
				parsedRequest.Files,
				_identityService.User,
				cancellation);

			return RenderResult(result);
		}

		/// <summary>
		/// Executor for GraphQL queries
		/// </summary>
		/// <param name="query">The graphql query body</param>
		/// <param name="variables">Variables for the graphql query as JSON</param>
		/// <param name="operationName">The name of the graphql operation to run</param>
		/// <param name="cancellation">Cancellation token for the operation</param>
		/// <returns>The results for the GraphQL query</returns>
		[HttpGet]
		[Authorize(Policy = "AllowVisitorPolicy")]
		public async Task<ExecutionResult> Get(
			[FromQuery]string query,
			[FromQuery]string variables,
			[FromQuery]string operationName,
			CancellationToken cancellation)
		{
			await _identityService.RetrieveUserAsync();

			var jObject = ParseVariables(variables);
			var result = await _graphQlService.Execute(
				query,
				operationName,
				jObject.ToInputs(),
				new FormFileCollection(),
				_identityService.User,
				cancellation);

			return RenderResult(result);
		}

		/// <summary>
		/// Correctly renders the Graphql result for returning to the user
		/// </summary>
		/// <param name="result">The graphql execution result</param>
		/// <returns>The graphql execution result with better formatting</returns>
		private ExecutionResult RenderResult(ExecutionResult result)
		{
			if (result.Errors?.Count > 0)
			{
				var newEx = new ExecutionErrors();
				foreach (var error in result.Errors)
				{
					var ex = error.InnerException;
					if (ex is PostgresException pgException)
					{
						if (string.IsNullOrWhiteSpace(pgException.MessageText))
						{
							newEx.Add(error);
						}
						else
						{
							newEx.Add(new ExecutionError(pgException.MessageText));
						}
					}
					else
					{
						newEx.Add(error);
					}

					_logger.LogError(
						"Graphql error message: {Error}\nException: {Exception}",
						error.Message,
						ex?.ToString());
				}
				result.Errors = newEx;
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			return result;
		}

		/// <summary>
		/// Parses the post body, handling weather it is a multipart form or a json request
		/// </summary>
		/// <param name="cancellation">Cancellation token</param>
		/// <returns>A parsed result for the form</returns>
		private async Task<FormBody> ParsePostBody(CancellationToken cancellation)
		{
			// We are using JSON content type
			if (!Request.HasFormContentType)
			{
				using var sr = new StreamReader(Request.Body);
				using var jsonTextReader = new JsonTextReader(sr);
				var jBody = await JObject.LoadAsync(jsonTextReader, cancellation);
				var body = jBody.ToObject<PostBody>();

				return new FormBody
				{
					Body = body,
					Files = new FormFileCollection(),
				};
			}

			// Otherwise we are a multipart form
			var form = await Request.ReadFormAsync(cancellation);

			form.TryGetValue("operationName", out var operationName);
			form.TryGetValue("variables", out var variables);
			form.TryGetValue("query", out var query);

			return new FormBody
			{
				Body = new PostBody
				{
					Query = query.First(),
					Variables = ParseVariables(variables.First()),
					OperationName = operationName.First(),
				},
				Files = form.Files,
			};
		}

		/// <summary>
		/// Parses the variables object for the graphql query
		/// </summary>
		/// <param name="variables">The variables JSON string</param>
		/// <returns>The graphql Inputs object</returns>
		/// <exception cref="Exception">On failing to parse the string</exception>
		private static JObject ParseVariables(string variables)
		{
			if (variables == null)
			{
				return null;
			}

			try
			{
				return JObject.Parse(variables);
			}
			catch (Exception exception)
			{
				throw new Exception("Could not parse variables.", exception);
			}
		}

	}
}