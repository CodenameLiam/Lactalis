
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Services.Files;
using Microsoft.AspNetCore.Mvc;

namespace ServersideTests.Helpers.FileProviders
{
	/// <summary>
	/// In memory file storage provider. This store is backed by an in memory dictionary for testing purposes.
	/// </summary>
	public class InMemoryFileProvider : IUploadStorageProvider
	{
		private readonly ConcurrentDictionary<string, byte[]> _contents = new ConcurrentDictionary<string, byte[]>();

		/// <inheritdoc />
		public void Dispose()
		{
		}

		/// <inheritdoc />
		public Task<Stream> GetAsync(StorageGetOptions options, CancellationToken cancellationToken = default)
		{
			var bytes = _contents[GetFileKey(options.Container, options.FileName)];
			return Task.FromResult(new MemoryStream(bytes) as Stream);
		}

		/// <inheritdoc />
		public Task<IEnumerable<string>> ListAsync(StorageListOptions options, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(_contents.Keys
				.Where(x => x.StartsWith($"{options.Container}/")));
		}

		/// <inheritdoc />
		public Task<bool> ExistsAsync(StorageExistsOptions options, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(_contents.ContainsKey(GetFileKey(options.Container, options.FileName)));
		}

		/// <inheritdoc />
		public Task PutAsync(StoragePutOptions options, CancellationToken cancellationToken = default)
		{
			var stream = new MemoryStream();
			options.Content.CopyTo(stream);
			_contents[GetFileKey(options.Container, options.FileName)] = stream.ToArray();
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task DeleteAsync(StorageDeleteOptions options, CancellationToken cancellationToken = default)
		{
			_contents.Remove(GetFileKey(options.Container, options.FileName), out _);
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task<bool> ContainerExistsAsync(StorageContainerExistsOptions options, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(true);
		}

		/// <inheritdoc />
		public Task CreateContainerAsync(StorageCreateContainerOptions options, CancellationToken cancellationToken = default)
		{
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public async Task DeleteContainerAsync(StorageDeleteContainerOptions options, CancellationToken cancellationToken = default)
		{
			var files = await ListAsync(new StorageListOptions {Container = options.Container}, cancellationToken);
			foreach (var file in files)
			{
				_contents.Remove(file, out _);
			}
		}

		/// <inheritdoc />
		public Func<CancellationToken, Task<IActionResult>> OnFetch(StorageOnFetchOptions options)
		{
			return null;
		}

		private static string GetFileKey(string container, string fileName)
		{
			return $"{container}/{fileName}";
		}
	}
}