
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lactalis.Utility
{
	public static class StringUtilities
	{
		/// <summary>
		/// Method for converting field value to camel Case
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public static string ConvertToCamelCase(this string field) => char.ToLowerInvariant(field[0]) + field.Substring(1);

		/// <summary>
		/// Method for converting field value to Pascal Case
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public static string ConvertToPascalCase(this string field) => char.ToUpperInvariant(field[0]) + field.Substring(1);

	}
}
