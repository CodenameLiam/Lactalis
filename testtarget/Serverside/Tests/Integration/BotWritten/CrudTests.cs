
using System;
using System.Linq;
using FluentAssertions;
using Lactalis.Controllers.Entities;
using Lactalis.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;



namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class CrudTests : IDisposable
	{
		private readonly IWebHost _host;
		private readonly LactalisDBContext _database;
		private readonly IServiceScope _scope;
		private readonly IServiceProvider _serviceProvider;

		public CrudTests()
		{
			_host = ServerBuilder.CreateServer();
			_scope = _host.Services.CreateScope();
			_serviceProvider = _scope.ServiceProvider;
			_database = _serviceProvider.GetRequiredService<LactalisDBContext>();
		}
		
		public void Dispose()
		{
			_host?.Dispose();
			_database?.Dispose();
			_scope?.Dispose();
		}


		[Fact]
		public async void TradingPostListingEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TradingPostListingEntityController>();
			var entities = new EntityFactory<TradingPostListingEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void TradingPostCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TradingPostCategoryEntityController>();
			var entities = new EntityFactory<TradingPostCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void AdminEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AdminEntityController>();
			var entities = new EntityFactory<AdminEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void FarmEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<FarmEntityController>();
			var entities = new EntityFactory<FarmEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void MilkTestEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<MilkTestEntityController>();
			var entities = new EntityFactory<MilkTestEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void FarmerEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<FarmerEntityController>();
			var entities = new EntityFactory<FarmerEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void ImportantDocumentCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ImportantDocumentCategoryEntityController>();
			var entities = new EntityFactory<ImportantDocumentCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void TechnicalDocumentCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TechnicalDocumentCategoryEntityController>();
			var entities = new EntityFactory<TechnicalDocumentCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void QualityDocumentCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<QualityDocumentCategoryEntityController>();
			var entities = new EntityFactory<QualityDocumentCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void QualityDocumentEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<QualityDocumentEntityController>();
			var entities = new EntityFactory<QualityDocumentEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void TechnicalDocumentEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<TechnicalDocumentEntityController>();
			var entities = new EntityFactory<TechnicalDocumentEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void ImportantDocumentEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<ImportantDocumentEntityController>();
			var entities = new EntityFactory<ImportantDocumentEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void NewsArticleEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<NewsArticleEntityController>();
			var entities = new EntityFactory<NewsArticleEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void AgriSupplyDocumentCategoryEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AgriSupplyDocumentCategoryEntityController>();
			var entities = new EntityFactory<AgriSupplyDocumentCategoryEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void SustainabilityPostEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<SustainabilityPostEntityController>();
			var entities = new EntityFactory<SustainabilityPostEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void AgriSupplyDocumentEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<AgriSupplyDocumentEntityController>();
			var entities = new EntityFactory<AgriSupplyDocumentEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

		[Fact]
		public async void PromotedArticlesEntityControllerGetTest()
		{
			// Arrange
			using var controller = _serviceProvider.GetRequiredService<PromotedArticlesEntityController>();
			var entities = new EntityFactory<PromotedArticlesEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();
			_database.AddRange(entities);
			await _database.SaveChangesAsync();

			// Act
			var data = await controller.Get(null, default);

			// Assert
			data.Data.Select(d => d.Id).Should().Contain(entities.Select(d => d.Id));
		}

	}
}