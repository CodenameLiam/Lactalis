
using System;

namespace Lactalis.Exceptions
{
	public class InvalidUserPasswordException : Exception
	{
		public InvalidUserPasswordException() : base("The username/password couple is invalid.")
		{
		}

		public InvalidUserPasswordException(string message) : base(message)
		{
		}

		public InvalidUserPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}