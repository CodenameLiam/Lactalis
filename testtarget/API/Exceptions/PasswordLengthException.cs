

using System;

namespace APITests.Exceptions
{
	public class PasswordLengthException : Exception
	{
		public PasswordLengthException() : base("Passwords must be at least 6 characters.")
		{
		}

		public PasswordLengthException(string message) : base(message)
		{
		}

		public PasswordLengthException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}