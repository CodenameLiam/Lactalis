

using System;

namespace APITests.Exceptions
{
	public class AccountNotActivatedException : Exception
	{
		public AccountNotActivatedException() : base("This account is not yet activated")
		{
		}

		public AccountNotActivatedException(string message) : base(message)
		{
		}

		public AccountNotActivatedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}