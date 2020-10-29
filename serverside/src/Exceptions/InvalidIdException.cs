
using System;

namespace Lactalis.Exceptions
{
	public class InvalidIdException : Exception
	{
		public InvalidIdException() : base("An invalid id was provided")
		{
		}

		public InvalidIdException(string message) : base(message)
		{
		}

		public InvalidIdException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}