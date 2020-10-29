

using System;

namespace APITests.Exceptions
{
	public class UnexpectedResponseCodeException : Exception
	{
		public UnexpectedResponseCodeException() : base("The Response code returned an unexpected result")
		{
		}

		public UnexpectedResponseCodeException(string message) : base(message)
		{
		}

		public UnexpectedResponseCodeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}