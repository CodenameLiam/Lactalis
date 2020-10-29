

using System;

namespace APITests.Exceptions
{
	public class UserExistsException : Exception
	{
		public UserExistsException() : base("Username is already taken.")
		{
		}

		public UserExistsException(string message) : base(message)
		{
		}

		public UserExistsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}