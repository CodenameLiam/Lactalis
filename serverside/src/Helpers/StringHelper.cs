
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lactalis.Helpers
{
	public static class StringHelper
	{
		public static string LowerCaseFirst(this string input)
		{
			var newString = input;
			if (!string.IsNullOrEmpty(newString) && char.IsUpper(newString[0]))
			{
				newString = char.ToLower(newString[0]) + newString.Substring(1);
			}
			return newString;
		}

		public static string UpperCaseFirst(this string input)
		{
			var newString = input;
			if (!string.IsNullOrEmpty(newString) && char.IsLower(newString[0]))
			{
				newString = char.ToUpper(newString[0]) + newString.Substring(1);
			}
			return newString;
		}
	}
}
