

using System;

namespace APITests.Exceptions
{
	public class PasswordAtLeastOneUpperCaseCharacterException : Exception
	{
		public PasswordAtLeastOneUpperCaseCharacterException() : base("Passwords must have at least one uppercase ('A'-'Z').")
		{
		}

		public PasswordAtLeastOneUpperCaseCharacterException(string message) : base(message)
		{
		}

		public PasswordAtLeastOneUpperCaseCharacterException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}