
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lactalis.Controllers;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services;
using Lactalis.Services.Files;
using Lactalis.Services.Interfaces;
using GraphQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using ServersideTests.Helpers.FileProviders;
using Xunit;

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class GraphqlFileTests : IDisposable
	{
		private const string FileContents = @"<svg width=""200"" height=""200"" xmlns=""http://www.w3.org/2000/svg"">
	<circle cx=""10"" cy=""10"" r=""2"" fill=""red""/>
	<text x=""20"" y=""35"" class=""small"">{TEXT}</text>
</svg>";

		private readonly IWebHost _host;
		private readonly LactalisDBContext _dbContext;
		private readonly IUploadStorageProvider _storageProvider;
		private readonly ICrudService _crudService;
		private readonly IGraphQlService _graphqlService;
		private readonly IIdentityService _identityService;
		private readonly FileController _fileController;

		public GraphqlFileTests()
		{
			_host = ServerBuilder.CreateServer(new ServerBuilderOptions
			{
				ConfigureServices = (collection, options) =>
				{
					collection.AddScoped<IUploadStorageProvider, InMemoryFileProvider>();
				}
			});
			var serviceProvider = _host.Services.CreateScope().ServiceProvider;
			var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

			_dbContext = serviceProvider.GetRequiredService<LactalisDBContext>();
			_storageProvider = serviceProvider.GetRequiredService<IUploadStorageProvider>();
			_crudService = serviceProvider.GetRequiredService<ICrudService>();
			_graphqlService = serviceProvider.GetRequiredService<IGraphQlService>();
			_identityService = serviceProvider.GetRequiredService<IIdentityService>();
			_fileController = serviceProvider.GetRequiredService<FileController>();

			_fileController.ControllerContext.HttpContext = httpContextAccessor.HttpContext;
		}

		public void Dispose()
		{
			_host.Dispose();
			_dbContext.Dispose();
			_storageProvider.Dispose();
		}

		/// <summary>
		/// Test for the Product Image attribute on the Trading Post Listing entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetTradingPostListingProductImageTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<TradingPostListingEntity>(false, "TradingPostListingEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.ProductImage = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "TradingPostListingEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("TradingPostListingEntity", "productImageId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the Product Image attribute on the TradingPostListingEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateTradingPostListingEntityProductImageTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<TradingPostListingEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.ProductImageId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.ProductImageId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.TradingPostListingEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.ProductImageId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "TradingPostListingEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the File attribute on the Quality Document entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetQualityDocumentFileTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<QualityDocumentEntity>(false, "QualityDocumentEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.File = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "QualityDocumentEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("QualityDocumentEntity", "fileId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the File attribute on the QualityDocumentEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateQualityDocumentEntityFileTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<QualityDocumentEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FileId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FileId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.QualityDocumentEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FileId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "QualityDocumentEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the File attribute on the Technical Document entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetTechnicalDocumentFileTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<TechnicalDocumentEntity>(false, "TechnicalDocumentEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.File = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "TechnicalDocumentEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("TechnicalDocumentEntity", "fileId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the File attribute on the TechnicalDocumentEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateTechnicalDocumentEntityFileTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<TechnicalDocumentEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FileId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FileId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.TechnicalDocumentEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FileId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "TechnicalDocumentEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the File attribute on the Important Document entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetImportantDocumentFileTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<ImportantDocumentEntity>(false, "ImportantDocumentEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.File = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "ImportantDocumentEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("ImportantDocumentEntity", "fileId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the File attribute on the ImportantDocumentEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateImportantDocumentEntityFileTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<ImportantDocumentEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FileId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FileId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.ImportantDocumentEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FileId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "ImportantDocumentEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the Feature Image attribute on the News Article entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetNewsArticleFeatureImageTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<NewsArticleEntity>(false, "NewsArticleEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FeatureImage = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "NewsArticleEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("NewsArticleEntity", "featureImageId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the Feature Image attribute on the NewsArticleEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateNewsArticleEntityFeatureImageTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<NewsArticleEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FeatureImageId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FeatureImageId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.NewsArticleEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FeatureImageId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "NewsArticleEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the Image attribute on the Sustainability Post entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetSustainabilityPostImageTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<SustainabilityPostEntity>(false, "SustainabilityPostEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.Image = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "SustainabilityPostEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("SustainabilityPostEntity", "imageId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the Image attribute on the SustainabilityPostEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateSustainabilityPostEntityImageTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<SustainabilityPostEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.ImageId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.ImageId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.SustainabilityPostEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.ImageId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "SustainabilityPostEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the File attribute on the Sustainability Post entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetSustainabilityPostFileTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<SustainabilityPostEntity>(false, "SustainabilityPostEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.File = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "SustainabilityPostEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("SustainabilityPostEntity", "fileId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the File attribute on the SustainabilityPostEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateSustainabilityPostEntityFileTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<SustainabilityPostEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FileId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FileId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.SustainabilityPostEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FileId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "SustainabilityPostEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Test for the File attribute on the Agri Supply Document entity
		/// that will will ensure that files can be fetched using the graphql API.
		/// </summary>
		[Fact]
		public async void GetAgriSupplyDocumentFileTest()
		{
			// Arrange
			var (dbEntity, fileEntity) = InitialiseEntity<AgriSupplyDocumentEntity>(false, "AgriSupplyDocumentEntity");
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.File = fileEntity;
			_dbContext.Add(dbEntity);
			await _dbContext.SaveChangesAsync();

			await _storageProvider.PutAsync(new StoragePutOptions
			{
				Container = "AgriSupplyDocumentEntity",
				FileName = fileEntity.FileId,
				Content = new MemoryStream(fileBytes),
			});

			// Act
			var fileId = await FetchFileAsync("AgriSupplyDocumentEntity", "fileId");
			var fileResult = await _fileController.Get(fileId, default) as FileStreamResult;

			// Assert
			Assert.NotNull(fileResult?.FileStream);

			using var reader = new StreamReader(fileResult.FileStream);
			var fileContents = reader.ReadToEnd();

			Assert.Equal(fileToSave, fileContents);
		}

		/// <summary>
		/// Test for the File attribute on the AgriSupplyDocumentEntity entity
		/// that will will ensure that files can be saved using the CrudService
		/// </summary>
		[Fact]
		public async void CreateAgriSupplyDocumentEntityFileTest()
		{
			// Arrange
			var (dbEntity, _) = InitialiseEntity<AgriSupplyDocumentEntity>(true);
			var fileToSave = FileContents.Replace("{TEXT}", dbEntity.Id.ToString());
			var fileBytes = Encoding.UTF8.GetBytes(fileToSave);

			dbEntity.FileId = Guid.NewGuid();

			// Act
			await _crudService.Create(dbEntity, new UpdateOptions
			{
				Files = new FormFileCollection
				{
					new FormFile(
						new MemoryStream(fileBytes),
						0,
						fileBytes.LongLength,
						dbEntity.FileId.ToString(),
						"file.svg")
					{
						Headers = new HeaderDictionary
						{
							{HeaderNames.ContentType, "image/svg"}
						},
					}
				}
			});

			// Assert
			var entity = _dbContext.AgriSupplyDocumentEntity.First();
			var file = _dbContext.Files.First(f => f.Id == entity.FileId);
			var fileStream = await _storageProvider.GetAsync(new StorageGetOptions
			{
				Container = "AgriSupplyDocumentEntity",
				FileName = file.FileId
			});
			var reader = new StreamReader(fileStream);

			Assert.Equal(fileToSave, reader.ReadToEnd());
		}

		/// <summary>
		/// Initialises a new entity with attributes, references and a random owner and a file for this entity.
		/// </summary>
		/// <param name="disableIds">Should id generation be disabled for this entity</param>
		/// <param name="containerName">The name of the container the file should be stored in</param>
		/// <typeparam name="T">The type of the entity to create</typeparam>
		/// <returns>An entity and file pairing</returns>
		private static (T, UploadFile) InitialiseEntity<T>(bool disableIds = false, string containerName = "")
			where T : class, IAbstractModel, new()
		{
			var ownerId = Guid.NewGuid();
			var dbEntity = new EntityFactory<T>()
				.UseAttributes()
				.UseReferences()
				.UseOwner(ownerId)
				.DisableIdGeneration(disableIds)
				.Generate()
				.First();
			var fileEntity = new EntityFactory<UploadFile>()
				.UseAttributes()
				.UseOwner(ownerId)
				.FreezeAttribute<UploadFile>("Container", containerName)
				.DisableIdGeneration(disableIds)
				.Generate()
				.First();

			return (dbEntity, fileEntity);
		}

		/// <summary>
		/// Fetches the file id from graphql for the first of this entity
		/// </summary>
		/// <param name="entityName">The name of the entity in pascal case</param>
		/// <param name="attributeName">The name of the attribute in camel case</param>
		/// <returns>The file id</returns>
		private async Task<Guid> FetchFileAsync(string entityName, string attributeName)
		{
			var entityNameCamelCase = entityName.LowerCaseFirst();

			await _identityService.RetrieveUserAsync();

			var executionResult = await _graphqlService.Execute(
				$@"query {entityNameCamelCase} {{
					{entityNameCamelCase}s {{
						id
						created
						modified
						{attributeName}
						__typename
					}}
					count{entityName}s {{
						number
						__typename
					}}
				}}
				",
				entityNameCamelCase,
				new Inputs(),
				new FormFileCollection(),
				_identityService.User,
				default);

			var results = ((List<object>) ((Dictionary<string, object>) executionResult.Data)[$"{entityNameCamelCase}s"])
				.Cast<Dictionary<string, object>>()
				.Select(x => x[attributeName])
				.Cast<string>();

			return Guid.Parse(results.First());
		}
	}
}
