
using System.Collections;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Lactalis.Utility
{
	public class CsvOutputFormatter : OutputFormatter
	{
		public CsvOutputFormatter()
		{
			SupportedMediaTypes.Clear();
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
		}

		public override bool CanWriteResult(OutputFormatterCanWriteContext context)
		{
			return base.CanWriteResult(context) && typeof(IEnumerable).IsAssignableFrom(context.ObjectType);
		}

		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
		{
			if (context.Object is IEnumerable data)
			{
				await using var writer = new StreamWriter(context.HttpContext.Response.Body);
				await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

				csv.SetIsoDateTimeFormat();

				await csv.WriteRecordsAsync(data);
			}
		}
	}
}