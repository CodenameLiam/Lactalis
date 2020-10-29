

using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SeleniumTests.Utils
{
	public static class StringUtil
	{
		public static string Capitalize(this string field) => char.ToUpperInvariant(field[0]) + field.Substring(1);
		public static string Capitalize(this string[] sentence)=> sentence.Select(e => e.ToLower().Capitalize()).Aggregate("", (c, n) => $"{c} {n}").Trim();
		public static string RemoveWordsSpacing(this string field) => field.Replace(" ", "");

		public static IList<string> GetTableData(this Table table)
		{
			// return table values
			return table.Rows.Select(row => row.Select(cell => cell.Value).FirstOrDefault()).ToList();
		}
	}
}