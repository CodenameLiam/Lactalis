
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lactalis.Utility;
 

namespace Lactalis.Controllers.Entities
{
	/// <summary>
	/// Options for downloading files
	/// </summary>
	public class FileDownloadOptions
	{
		/// <summary>
		/// The name of the file
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// The content type of the file
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// The length of the file
		/// </summary>
		public long Length { get; set; }

		/// <summary>
		/// Should the file be downloaded or displayed in the browser
		/// </summary>
		public bool Download { get; set; }
	}

	[ApiController]
	[Authorize]
	public class BaseApiController : Controller
	{
		/// <summary>
		/// Sets the headers for downloading a file
		/// </summary>
		/// <param name="contentType">The content type to download</param>
		/// <param name="fileName">The name of the file to download</param>
		ed void SetDownloadFileHeaders(string contentType, string fileName)
		{
			SetFileHeaders(new FileDownloadOptions
			{
				ContentType = contentType,
				Download = true,
				FileName = fileName,
			});
		}

		/// <summary>
		/// Sets the headers for downloading files
		/// </summary>
		/// <param name="options">The file download options</param>
		ed void SetFileHeaders(FileDownloadOptions options)
		{
			if (options.ContentType != default)
			{
				Response.ContentType = options.ContentType;
			}

			if (options.Length != default)
			{
				Response.ContentLength = options.Length;
			}

			var contentDisposition = new StringBuilder();
			if (options.Download)
			{
				contentDisposition.Append("attachment; ");
			}

			if (options.FileName != default)
			{
				contentDisposition.Append($"filename=\"{options.FileName}\"");
			}

			if (contentDisposition.Length > 0)
			{
				Response.Headers["Content-Disposition"] = contentDisposition.ToString();
			}
		}

		/// <summary>
		/// Writes an IQueryable to a the output stream of the response as a csv
		/// </summary>
		/// <param name="queryable">The queryable to write</param>
		/// <param name="fileName">The file name to write out</param>
		/// <param name="cancellationToken">Cancellation token to cancel the action</param>
		/// <typeparam name="T">The type of the entity to write out</typeparam>
		/// <returns></returns>
		ed async Task WriteQueryableCsvAsync<T>(
			IQueryable<T> queryable,
			string fileName,
			CancellationToken cancellationToken = default)
		{
			SetDownloadFileHeaders("text/csv", fileName);
			var writer = new StreamWriter(Response.Body);
			var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

			csv.SetIsoDateTimeFormat();

			csv.WriteHeader<T>();
			await csv.NextRecordAsync();
			await csv.WriteQueryableAsync(queryable, cancellationToken);

			await csv.FlushAsync();
			await writer.FlushAsync();
		}

	 
	
	}
}
