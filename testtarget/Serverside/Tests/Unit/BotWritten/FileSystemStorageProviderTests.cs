

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoFixture;
using Lactalis.Configuration;
using Lactalis.Services.Files;
using Lactalis.Services.Files.Providers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ServersideTests.Tests.Unit.BotWritten
{

	public class InvalidPathNameTheoryData : TheoryData<string>
	{
		public InvalidPathNameTheoryData()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Add("/");
				Add("/AnotherTypeOfString");
				Add("AnotherTypeOfString/");
				Add("\0");
			} else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
				foreach (var invalidFileNameChar in Path.GetInvalidFileNameChars())
				{
					Add("Test" + invalidFileNameChar);
				}
			}
		}
	}

	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class FileSystemStorageProviderTests
	{
		private readonly Fixture _fixture;
		private readonly FileSystemStorageProvider _fileSystemStorageProvider;

		public FileSystemStorageProviderTests()
		{
			_fixture = new Fixture();

			var mockFileConfiguration = new Mock<IOptions<FileSystemStorageProviderConfiguration>>();
			var mockOptions = new Mock<FileSystemStorageProviderConfiguration>();
			mockFileConfiguration.Setup(x => x.Value).Returns(mockOptions.Object);

			var mockLogger = new Mock<ILogger<FileSystemStorageProvider>>();

			_fileSystemStorageProvider = new FileSystemStorageProvider(mockFileConfiguration.Object, mockLogger.Object);
		}

		[Theory]
		[ClassData(typeof(InvalidPathNameTheoryData))]
		public void TestContainerNameInvalid(string inputPathName)
		{
			// Arrange
			var storageOptions = new StorageGetOptions { Container = inputPathName, FileName = _fixture.Create<string>() };

			// Act
			Func<Task> act = async () =>
				await _fileSystemStorageProvider.GetAsync(storageOptions);

			// Assert
			act.Should().Throw<IOException>().WithMessage("Invalid path name");
		}

		[Theory]
		[Trait("Category", "Immediate")]
		[ClassData(typeof(InvalidPathNameTheoryData))]
		public void TestFileNameInvalid(string inputPathName)
		{
			// Arrange
			var storageOptions = new StorageGetOptions { Container = _fixture.Create<string>(), FileName = inputPathName };

			// Act
			Func<Task> act = async () =>
				await _fileSystemStorageProvider.GetAsync(storageOptions);

			// Assert
			act.Should().Throw<IOException>().WithMessage("Invalid path name");
		}
	}
}