
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Models;
using GraphQL;
using Microsoft.AspNetCore.Http;

namespace Lactalis.Services.Interfaces
{
	public interface IGraphQlService
	{
		/// <summary>
		/// Executes a graphql query
		/// </summary>
		/// <param name="query">The query to execute</param>
		/// <param name="operationName">The name of the graphql operation to execute</param>
		/// <param name="variables">Variables to pass into the query</param>
		/// <param name="attachments">The files that are attached to this request</param>
		/// <param name="user">The user to perform the operation</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The result of the query in a task</returns>
		Task<ExecutionResult> Execute(
			string query,
			string operationName,
			Inputs variables,
			IFormFileCollection attachments,
			User user,
			CancellationToken cancellation);
	}
}