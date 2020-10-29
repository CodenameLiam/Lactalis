
using System.Collections.Generic;
using System.Linq;
using Lactalis.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lactalis.Utility
{
	public class ApiErrorResponse
	{
		public ICollection<ApiError> errors { get; set; } = new List<ApiError>();

		public ApiErrorResponse() { }

		public ApiErrorResponse(IEnumerable<string> errors)
		{
			this.errors.AddRange(errors.Select(error => new ApiError(error)));
		}

		public ApiErrorResponse(string error)
		{
			errors.Add(new ApiError(error));
		}
	}

	public class ApiError
	{
		public string message { get; }

		public ApiError(string message)
		{
			this.message = message;
		}
	}

	public static class ModelStateExtensions
	{
		public static ApiErrorResponse GetNormalisedErrors(this ModelStateDictionary modelStateDictionary)
		{
			return new ApiErrorResponse
			{
				errors = modelStateDictionary
					.Values
					.SelectMany(error => error.Errors)
					.Select(message => new ApiError(message.ErrorMessage))
					.ToList()
			};
		}
	}
}