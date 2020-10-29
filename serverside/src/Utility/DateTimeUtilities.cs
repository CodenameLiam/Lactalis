
using System;

namespace Lactalis.Utility
{
	public static class DateTimeUtilities
	{
		public static string ToIsoString(this DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
		}
	}
}