
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Lactalis.Utility
{
	public class LactalisActionResult : IActionResult
	{
		public Task ExecuteResultAsync(ActionContext context)
		{
			var messages = context.ModelState
				.Where(e => e.Value.ValidationState == ModelValidationState.Invalid)
				.Select(e => new {e.Key, Value = e.Value.Errors})
				.Select(e => new {e.Key, ErrorMessages = e.Value.Select(r => r.ErrorMessage)})
				.ToDictionary(errors => errors.Key, errors => string.Join(", ", errors.ErrorMessages));
			context.HttpContext.Response.ContentType = "application/json";
			context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			return context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
			{
				errors = messages.Values.Select(error => new
				{
					message = error
				})
			}));
		}
	}
}