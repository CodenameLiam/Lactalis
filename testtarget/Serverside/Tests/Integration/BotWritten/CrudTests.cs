/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
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
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

// % protected region % [Add any additional imports here] off begin
// % protected region % [Add any additional imports here] end

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
		// % protected region % [Add any additional members here] off begin
		// % protected region % [Add any additional members here] end

		public CrudTests()
		{
			// % protected region % [Configure constructor here] off begin
			_host = ServerBuilder.CreateServer();
			_scope = _host.Services.CreateScope();
			_serviceProvider = _scope.ServiceProvider;
			_database = _serviceProvider.GetRequiredService<LactalisDBContext>();
			// % protected region % [Configure constructor here] end
		}
		
		public void Dispose()
		{
			// % protected region % [Configure dispose here] off begin
			_host?.Dispose();
			_database?.Dispose();
			_scope?.Dispose();
			// % protected region % [Configure dispose here] end
		}


		// % protected region % [Customise Trading Post Listing Entity crud tests here] off begin
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
		// % protected region % [Customise Trading Post Listing Entity crud tests here] end

		// % protected region % [Customise Trading Post Category Entity crud tests here] off begin
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
		// % protected region % [Customise Trading Post Category Entity crud tests here] end

		// % protected region % [Customise Admin Entity crud tests here] off begin
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
		// % protected region % [Customise Admin Entity crud tests here] end

		// % protected region % [Customise Farm Entity crud tests here] off begin
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
		// % protected region % [Customise Farm Entity crud tests here] end

		// % protected region % [Customise Milk Test Entity crud tests here] off begin
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
		// % protected region % [Customise Milk Test Entity crud tests here] end

		// % protected region % [Customise Farmer Entity crud tests here] off begin
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
		// % protected region % [Customise Farmer Entity crud tests here] end

		// % protected region % [Customise Important Document Category Entity crud tests here] off begin
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
		// % protected region % [Customise Important Document Category Entity crud tests here] end

		// % protected region % [Customise Quality Document Category Entity crud tests here] off begin
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
		// % protected region % [Customise Quality Document Category Entity crud tests here] end

		// % protected region % [Customise Technical Document Category Entity crud tests here] off begin
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
		// % protected region % [Customise Technical Document Category Entity crud tests here] end

		// % protected region % [Customise Quality Document Entity crud tests here] off begin
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
		// % protected region % [Customise Quality Document Entity crud tests here] end

		// % protected region % [Customise Technical Document Entity crud tests here] off begin
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
		// % protected region % [Customise Technical Document Entity crud tests here] end

		// % protected region % [Customise Important Document Entity crud tests here] off begin
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
		// % protected region % [Customise Important Document Entity crud tests here] end

		// % protected region % [Customise News Article Entity crud tests here] off begin
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
		// % protected region % [Customise News Article Entity crud tests here] end

		// % protected region % [Customise Promoted Articles Entity crud tests here] off begin
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
		// % protected region % [Customise Promoted Articles Entity crud tests here] end

		// % protected region % [Customise Agri Supply Document Category Entity crud tests here] off begin
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
		// % protected region % [Customise Agri Supply Document Category Entity crud tests here] end

		// % protected region % [Customise Sustainability Post Entity crud tests here] off begin
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
		// % protected region % [Customise Sustainability Post Entity crud tests here] end

		// % protected region % [Customise Agri Supply Document Entity crud tests here] off begin
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
		// % protected region % [Customise Agri Supply Document Entity crud tests here] end

	// % protected region % [Add any additional tests here] off begin
	// % protected region % [Add any additional tests here] end
	}
}