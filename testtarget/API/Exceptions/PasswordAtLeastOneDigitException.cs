

using System;

namespace APITests.Exceptions
{
	public class PasswordAtLeastOneDigitException : Exception
	{
		public PasswordAtLeastOneDigitException() : base("Passwords must have at least one digit ('0'-'9').")
		{
		}

		public PasswordAtLeastOneDigitException(string message) : base(message)
		{
		}

		public PasswordAtLeastOneDigitException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}