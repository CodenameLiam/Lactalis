
using System;
using Microsoft.AspNetCore.Identity;

namespace Lactalis.Exceptions
{
	public class IdentityOperationException : Exception
	{
		public IdentityResult IdentityResult { get; }

		public IdentityOperationException() : base("The identity operation was invalid")
		{
		}

		public IdentityOperationException(string message) : base(message)
		{
		}

		public IdentityOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public IdentityOperationException(IdentityResult identityResult) : base("The identity operation was invalid")
		{
			IdentityResult = identityResult;
		}

	}
}