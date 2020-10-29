
using System;

namespace Lactalis.Exceptions
{
	public class InvalidGrantTypeException : Exception
	{
		public InvalidGrantTypeException() : base("The specified grant type is not supported.")
		{
		}

		public InvalidGrantTypeException(string message) : base(message)
		{
		}

		public InvalidGrantTypeException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}