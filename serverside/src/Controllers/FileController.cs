
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Controllers.Entities;
using Lactalis.Models;
using Lactalis.Services.Interfaces;
using Lactalis.Services.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 

namespace Lactalis.Controllers
{
	public class MetadataResponse
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
		public double Length { get; set; }
	}

	[ApiController]
	[Route("/api/files")]
	public class FileController : BaseApiController
	{
		private readonly IUploadStorageProvider _storageProvider;
		private readonly ICrudService _crudService;
		private readonly ILogger<FileController> _logger;

		public FileController(
			IUploadStorageProvider storageProvider,
			ICrudService crudService,
			ILogger<FileController> logger
			)
		{
			_storageProvider = storageProvider;
			_crudService = crudService;
			_logger = logger;
		}

		/// <summary>
		/// Gets a file
		/// </summary>
		/// <param name="id">The id of the file to get</param>
		/// <param name="cancellationToken">The cancellation token for this operation</param>
		/// <param name="download">Should the file be downloaded</param>
		/// <returns>The requested file</returns>
		[HttpGet]
		[Route("{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(
			Guid id,
			CancellationToken cancellationToken,
			[FromQuery]bool download = false
			)
		{
			UploadFile file;
			try
			{
				file = await _crudService.GetFile(id);
				_logger.LogInformation("Fetched file with id: {Id}", file.Id, file);
			}
			catch (FileNotFoundException e)
			{
				return BadRequest(e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				return Unauthorized(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			// Check if there is a OnFetch method given by the provider
			var onFetch = _storageProvider.OnFetch(new StorageOnFetchOptions
			{
				Download = download,
				File = file,
				HttpContext = HttpContext,
			});
			if (onFetch != null)
			{
				return await onFetch(cancellationToken);
			}

			// No OnFetch provided, execute the download ourselves
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = file.Container,
				FileName = file.FileId,
			}, cancellationToken);
			SetFileHeaders(new FileDownloadOptions
			{
				ContentType = file.ContentType,
				FileName = file.FileName,
				Length = fileStream.Length,
				Download = download,
			});

			return new FileStreamResult(fileStream, file.ContentType ?? "application/octet-stream");
		}

		/// <summary>
		/// Gets the metadata for a file
		/// </summary>
		/// <param name="id">The id of the file</param>
		/// <param name="cancellationToken">Cancellation token for the operation</param>
		/// <returns>The file metadata</returns>
		[HttpGet]
		[Route("metadata/{id}")]
		[Produces(typeof(MetadataResponse))]
		[AllowAnonymous]
		public async Task<IActionResult> Metadata(
			Guid id, 
			CancellationToken cancellationToken
			)
		{
			UploadFile file;
			try
			{
				file = await _crudService.GetFile(id);
				_logger.LogInformation("Fetched file with id: {Id}", file.Id, file);
			}
			catch (FileNotFoundException e)
			{
				return BadRequest(e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				return Unauthorized(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(new MetadataResponse
			{
				Id = file.Id,
				Created = file.Created,
				Modified = file.Modified,
				FileName = file.FileName,
				ContentType = file.ContentType,
				Length = file.Length,
			});
		}

	}
}