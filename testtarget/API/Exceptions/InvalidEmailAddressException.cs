

using System;

namespace APITests.Exceptions
{
	public class InvalidEmailAddressException : Exception
	{
		public InvalidEmailAddressException() : base("The Email field is not a valid e-mail address.")
		{
		}

		public InvalidEmailAddressException(string message) : base(message)
		{
		}

		public InvalidEmailAddressException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}