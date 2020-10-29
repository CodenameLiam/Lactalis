

using System;

namespace APITests.Exceptions
{
	public class PasswordNonAlphanumericException : Exception
	{
		public PasswordNonAlphanumericException() : base("Passwords must have at least one non alphanumeric character.")
		{
		}

		public PasswordNonAlphanumericException(string message) : base(message)
		{
		}

		public PasswordNonAlphanumericException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}