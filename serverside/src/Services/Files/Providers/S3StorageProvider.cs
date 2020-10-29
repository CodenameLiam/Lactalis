
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Lactalis.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Lactalis.Services.Files.Providers
{
	/// <summary>
	/// An implementation of the generic storage provider that saves files onto Amazon S3.
	/// </summary>
	/// <see cref="S3StorageProviderConfiguration">
	/// The appsettings configuration for the S3 provider
	/// </see>
	public class S3StorageProvider : IUploadStorageProvider
	{
		/// <summary>
		/// The application logger
		/// </summary>
		private readonly ILogger<S3StorageProvider> _logger;

		/// <summary>
		/// The S3 client
		/// </summary>
		private readonly IAmazonS3 _client;

		/// <summary>
		/// Name of the S3 bucket to use
		/// </summary>
		private readonly string _bucketName;

		/// <summary>
		/// List of objects that need to be disposed at the end of the service lifetime
		/// </summary>
		private readonly List<IDisposable> _disposables = new List<IDisposable>();


		public S3StorageProvider(
			IOptions<S3StorageProviderConfiguration> configuration,
			ILogger<S3StorageProvider> logger)
		{
			if (configuration.Value.AccessKey == null && configuration.Value.SecretKey == null)
			{
				// If there is no credentials provided do not pass them through.
				// This will attempt instance configuration if deployed on AWS.
				_client = new AmazonS3Client(RegionEndpoint.GetBySystemName(configuration.Value.RegionEndpoint));
			}
			else
			{
				// Otherwise set the client to to use basic credentials
				_client = new AmazonS3Client(
					new BasicAWSCredentials(configuration.Value.AccessKey, configuration.Value.SecretKey),
					RegionEndpoint.GetBySystemName(configuration.Value.RegionEndpoint));
			}
			_bucketName = configuration.Value.BucketName;
			_logger = logger;

			_logger.LogInformation(
				"Connecting to AWS instance with access key {Key} and bucket {Bucket}", 
				configuration.Value.AccessKey,
				configuration.Value.BucketName);
		}

		/// <inheritdoc />
		public async Task<Stream> GetAsync(StorageGetOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Fetching file {FileName} from container {Container}", options.FileName, options.Container);

			var request = new GetObjectRequest
			{
				BucketName = _bucketName,
				Key = GetFileKey(options.Container, options.FileName),
			};
			var response = await _client.GetObjectAsync(request, cancellationToken);
			_disposables.Add(response);

			return response.ResponseStream;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<string>> ListAsync(StorageListOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			_logger.LogDebug("Listing contents of container {Container}", options.Container);

			var request = new ListObjectsV2Request
			{
				Prefix = $"{options.Container}/",
				BucketName = _bucketName
			};

			ListObjectsV2Response response;
			var results = new List<string>();
			do
			{
				response = await _client.ListObjectsV2Async(request, cancellationToken);
				request.ContinuationToken = response.NextContinuationToken;

				results.AddRange(response.S3Objects.Select(r => r.Key));
			} while (response.IsTruncated);

			return results;
		}

		/// <inheritdoc />
		public async Task<bool> ExistsAsync(StorageExistsOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Checking if file {FileName} from container {Container} exists", options.FileName, options.Container);

			var request = new GetObjectRequest
			{
				BucketName = _bucketName,
				Key = GetFileKey(options.Container, options.FileName),
			};
			try
			{
				var response = await _client.GetObjectAsync(request, cancellationToken);
				_disposables.Add(response);
			}
			catch (AmazonS3Exception ex)
			{
				return ex.ErrorCode switch
				{
					"NoSuchKey" => false,
					"AccessDenied" => true,
					"PermanentRedirect" => true,
					_ => throw ex,
				};
			}

			return true;
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

			if (!options.Overwrite)
			{
				var fileExists = await ExistsAsync(new StorageExistsOptions
				{
					Container = options.Container,
					FileName = options.FileName,
				}, cancellationToken);

				if (fileExists)
				{
					throw new IOException("File already exists");
				}
			}

			var transferClient = new TransferUtility(_client);
			var request = new TransferUtilityUploadRequest
			{
				ContentType = options.ContentType,
				BucketName = _bucketName,
				Key = GetFileKey(options.Container, options.FileName),
				InputStream = options.Content,

			};
			await transferClient.UploadAsync(request, cancellationToken);
		}

		/// <inheritdoc />
		public async Task DeleteAsync(StorageDeleteOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);
			ValidateFileName(options.FileName);

			_logger.LogDebug("Deleting file {FileName} from container {Container}", options.FileName, options.Container);

			var request = new DeleteObjectRequest
			{
				BucketName = _bucketName,
				Key = GetFileKey(options.Container, options.FileName),
			};
			await _client.DeleteObjectAsync(request, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<bool> ContainerExistsAsync(StorageContainerExistsOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			_logger.LogDebug("Checking if container {Container} exists", options.Container);

			var request = new ListObjectsV2Request
			{
				Prefix = $"{options.Container}/",
				BucketName = _bucketName,
				MaxKeys = 1,
			};
			var response = await _client.ListObjectsV2Async(request, cancellationToken);

			return response.S3Objects.Any();
		}

		/// <inheritdoc />
		public Task CreateContainerAsync(StorageCreateContainerOptions options, CancellationToken cancellationToken = default)
		{
			_logger.LogDebug("Call to create container {Container}, however the S3 provider can't create containers", options.Container);
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public async Task DeleteContainerAsync(StorageDeleteContainerOptions options, CancellationToken cancellationToken = default)
		{
			ValidateFileName(options.Container);

			_logger.LogDebug("Deleting container {Container}", options.Container);

			var request = new ListObjectsV2Request
			{
				Prefix = $"{options.Container}/",
				BucketName = _bucketName,
			};

			ListObjectsV2Response response;
			do
			{
				response = await _client.ListObjectsV2Async(request, cancellationToken);
				request.ContinuationToken = response.NextContinuationToken;

				var deleteObjectsRequest = new DeleteObjectsRequest
				{
					BucketName = options.Container,
					Objects = response
						.S3Objects
						.Select(v => new KeyVersion
						{
							Key = v.Key,
						})
						.ToList()
				};
				await _client.DeleteObjectsAsync(deleteObjectsRequest, cancellationToken);
			} while (response.IsTruncated);
		}

		/// <inheritdoc />
		public Func<CancellationToken, Task<IActionResult>> OnFetch(StorageOnFetchOptions options)
		{
			return cancellationToken =>
			{
				var request = new GetPreSignedUrlRequest
				{
					BucketName = _bucketName,
					Key = GetFileKey(options.File.Container, options.File.FileId),
					Expires = DateTime.Now.AddDays(1),
					Verb = HttpVerb.GET,
				};
				request.ResponseHeaderOverrides.ContentDisposition =
					new ContentDispositionHeaderValue(options.Download ? "attachment" : "inline")
					{
						Name = options.File.FileName,
						FileNameStar = options.File.FileName,
						Size = options.File.Length,
						FileName = options.File.FileName,
					}.ToString();
				var s3Url = _client.GetPreSignedURL(request);
				return Task.FromResult(new RedirectResult(s3Url) as IActionResult);
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
		/// Gets the key name in S3
		/// </summary>
		/// <param name="container">The options.Container the file is in</param>
		/// <param name="fileName">The name of the file</param>
		/// <returns></returns>
		private static string GetFileKey(string container, string fileName)
		{
			return $"{container}/{fileName}";
		}

		public void Dispose()
		{
			_client?.Dispose();
			foreach (var disposable in _disposables)
			{
				disposable.Dispose();
			}
		}

	}
}