
using System;

namespace Lactalis.Exceptions
{
	public class DuplicateUserException : Exception
	{
		public DuplicateUserException() : base("This user already exists")
		{
		}

		public DuplicateUserException(string message) : base(message)
		{
		}

		public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}