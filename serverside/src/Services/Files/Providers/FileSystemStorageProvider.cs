
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Lactalis.Services.Files.Providers
{
	/// <summary>
	/// An implementation of the generic storage provider that saves files onto the file system that is running the
	/// application. This provider is mainly recommended for testing purposes, or if there is a file server attached
	/// to the application server that is running the application
	/// </summary>
	/// <see cref="FileSystemStorageProviderConfiguration">
	/// The appsettings configuration for the file system provider
	/// </see>
	public class FileSystemStorageProvider : IUploadStorageProvider
	{
		private readonly ILogger<FileSystemStorageProvider> _logger;
		private readonly FileSystemStorageProviderConfiguration _configuration;

		/// <summary>
		/// The root folder that the files are stored in
		/// </summary>
		private string RootFolder => _configuration.RootFolder;


		public FileSystemStorageProvider(
			IOptions<FileSystemStorageProviderConfiguration> configuration,
			ILogger<FileSystemStorageProvider> logger)
		{
			_logger = logger;
			_configuration = configuration.Value;

			_logger.LogInformation("Using file system provider. Root File {Path}", RootFolder);
		}

		/// <inheritdoc />
		public Task<Stream> GetAsync(StorageGetOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Fetching file {FileName} from container {Container}", options.FileName, options.Container);

			return Task.FromResult(File.OpenRead(GetFileLocation(options.Container, options.FileName)) as Stream);
		}

		/// <inheritdoc />
		public Task<IEnumerable<string>> ListAsync(StorageListOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			_logger.LogDebug("Listing contents of container {Container}", options.Container);

			return Task.FromResult(
				Directory.GetFiles(GetContainerLocation(options.Container)).AsEnumerable());
		}

		/// <inheritdoc />
		public Task<bool> ExistsAsync(StorageExistsOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Checking if file {FileName} from container {Container} exists", options.FileName, options.Container);

			return Task.FromResult(File.Exists(GetFileLocation(options.Container, options.FileName)));
		}

		/// <inheritdoc />
		public async Task PutAsync(StoragePutOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug(
				"Writing file {FileName} to container {Container}",
				options.FileName,
				options.Container,
				options.Overwrite);

			if (options.Overwrite == false && File.Exists(GetFileLocation(options.Container, options.FileName)))
			{
				throw new IOException("File already exists");
			}

			var containerLocation = GetContainerLocation(options.Container);
			if (!Directory.Exists(containerLocation))
			{
				if (options.CreateContainerIfNotExists)
				{
					Directory.CreateDirectory(containerLocation);
				}
				else
				{
					throw new IOException("This container does not exist");
				}
			}

			await using var streamWriter = new StreamWriter(GetFileLocation(options.Container, options.FileName), false);
			await options.Content.CopyToAsync(streamWriter.BaseStream, cancellationToken);
			await streamWriter.FlushAsync();
		}

		/// <inheritdoc />
		public Task DeleteAsync(StorageDeleteOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Deleting file {FileName} from container {Container}", options.FileName, options.Container);

			File.Delete(GetFileLocation(options.Container, options.FileName));

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task<bool> ContainerExistsAsync(StorageContainerExistsOptions options, CancellationToken cancellationToken = default)
		{
			_logger.LogDebug("Checking if container {Container} exists", options.Container);
			return Task.FromResult(Directory.Exists(GetContainerLocation(options.Container)));
		}

		/// <inheritdoc />
		public Task CreateContainerAsync(StorageCreateContainerOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			var path = GetContainerLocation(options.Container);
			if (!Directory.Exists(path))
			{
				_logger.LogDebug("Creating container {Container}", options.Container);
				Directory.CreateDirectory(path);
			}
			else
			{
				_logger.LogDebug("Attempting to create container {Container}, but it already exists", options.Container);
			}

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task DeleteContainerAsync(StorageDeleteContainerOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			_logger.LogDebug("Deleting container {Container}", options.Container);
			Directory.Delete(options.Container, true);

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Func<CancellationToken, Task<IActionResult>> OnFetch(StorageOnFetchOptions options)
		{
			return cancellationToken =>
			{
				var readStream = File.OpenRead(GetFileLocation(options.File.Container, options.File.FileId));

				var cd = new ContentDispositionHeaderValue(options.Download ? "attachment" : "inline")
				{
					Name = options.File.FileName,
					FileNameStar = options.File.FileName,
					Size = readStream.Length,
					FileName = options.File.FileName,
				};

				options.HttpContext.Response.Headers["Content-Disposition"] = cd.ToString();

				return Task.FromResult(new FileStreamResult(readStream, options.File.ContentType)
				{
					LastModified = options.File.Modified,
				} as IActionResult);
			};
		}

		/// <summary>
		/// Validates that a file has a valid filename
		/// </summary>
		/// <param name="fileName">The name of the file to validate</param>
		/// <exception cref="IOException">If the filename is invalid</exception>
		private void ValidateFileName(string fileName)
		{
			if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
			{
				_logger.LogInformation("The name {fileName} is an invalid file name", fileName);
				throw new IOException("Invalid path name");
			}
		}

		/// <summary>
		/// Gets the location of a options.Container on the file system
		/// </summary>
		/// <param name="container">The name of the options.Container</param>
		/// <returns>The location of the options.Container</returns>
		private string GetContainerLocation(string container)
		{
			return Path.Combine(Path.GetFullPath(RootFolder), container);
		}

		/// <summary>
		/// Get the location of a file on the file system
		/// </summary>
		/// <param name="container">The name of the options.Container the file is in</param>
		/// <param name="fileName">The name of the file</param>
		/// <returns>The location of the file</returns>
		private string GetFileLocation(string container, string fileName)
		{
			return Path.Combine(GetContainerLocation(container), fileName);
		}

		public void Dispose()
		{
		}

	}
}