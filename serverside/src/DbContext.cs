
using System;
using System.Linq;
using Npgsql;
using Audit.EntityFramework;
using Microsoft.AspNetCore.Dataion.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Lactalis.Enums;
using Microsoft.Extensions.Logging;
 

namespace Lactalis.Models {
	public class LactalisDBContext : AuditIdentityDbContext<User, Group, Guid>, IDataionKeyContext
	{
		private readonly ILogger<LactalisDBContext> _logger;

		public string SessionUserId { get; }
		public string SessionUser { get; }
		public string SessionId { get; }


		public DbSet<UploadFile> Files { get; set; }
		public DbSet<TradingPostListingEntity> TradingPostListingEntity { get; set; }
		public DbSet<TradingPostCategoryEntity> TradingPostCategoryEntity { get; set; }
		public DbSet<AdminEntity> AdminEntity { get; set; }
		public DbSet<FarmEntity> FarmEntity { get; set; }
		public DbSet<MilkTestEntity> MilkTestEntity { get; set; }
		public DbSet<FarmerEntity> FarmerEntity { get; set; }
		public DbSet<ImportantDocumentCategoryEntity> ImportantDocumentCategoryEntity { get; set; }
		public DbSet<TechnicalDocumentCategoryEntity> TechnicalDocumentCategoryEntity { get; set; }
		public DbSet<QualityDocumentCategoryEntity> QualityDocumentCategoryEntity { get; set; }
		public DbSet<QualityDocumentEntity> QualityDocumentEntity { get; set; }
		public DbSet<TechnicalDocumentEntity> TechnicalDocumentEntity { get; set; }
		public DbSet<ImportantDocumentEntity> ImportantDocumentEntity { get; set; }
		public DbSet<NewsArticleEntity> NewsArticleEntity { get; set; }
		public DbSet<AgriSupplyDocumentCategoryEntity> AgriSupplyDocumentCategoryEntity { get; set; }
		public DbSet<SustainabilityPostEntity> SustainabilityPostEntity { get; set; }
		public DbSet<AgriSupplyDocumentEntity> AgriSupplyDocumentEntity { get; set; }
		public DbSet<PromotedArticlesEntity> PromotedArticlesEntity { get; set; }
		public DbSet<TradingPostListingsTradingPostCategories> TradingPostListingsTradingPostCategories { get; set; }
		public DbSet<FarmersFarms> FarmersFarms { get; set; }
		public DbSet<DataionKey> DataionKeys { get; set; }

		static LactalisDBContext()
		{
			NpgsqlConnection.GlobalTypeMapper.MapEnum<PriceType>();
			NpgsqlConnection.GlobalTypeMapper.MapEnum<State>();
		}

		public LactalisDBContext(
			DbContextOptions<LactalisDBContext> options,
			IHttpContextAccessor httpContextAccessor,
			ILogger<LactalisDBContext> logger) : base(options)
		{
			_logger = logger;

			SessionUser = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			SessionUserId = httpContextAccessor?.HttpContext?.User?.FindFirst("UserId")?.Value;
			SessionId = httpContextAccessor?.HttpContext?.TraceIdentifier;

		}

		ed override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasPostgresEnum<PriceType>();
			modelBuilder.HasPostgresEnum<State>();
			// Configure models from the entity diagram
			modelBuilder.HasPostgresExtension("uuid-ossp");
			modelBuilder.ApplyConfiguration(new TradingPostListingEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TradingPostCategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new AdminEntityConfiguration());
			modelBuilder.ApplyConfiguration(new FarmEntityConfiguration());
			modelBuilder.ApplyConfiguration(new MilkTestEntityConfiguration());
			modelBuilder.ApplyConfiguration(new FarmerEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ImportantDocumentCategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TechnicalDocumentCategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new QualityDocumentCategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new QualityDocumentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TechnicalDocumentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ImportantDocumentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new NewsArticleEntityConfiguration());
			modelBuilder.ApplyConfiguration(new AgriSupplyDocumentCategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new SustainabilityPostEntityConfiguration());
			modelBuilder.ApplyConfiguration(new AgriSupplyDocumentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PromotedArticlesEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TradingPostListingsTradingPostCategoriesConfiguration());
			modelBuilder.ApplyConfiguration(new FarmersFarmsConfiguration());

			// Configure the user and group models
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GroupConfiguration());

			// Configure the file upload models
			modelBuilder.ApplyConfiguration(new UploadFileConfiguration());

		}

		/// <summary>
		/// Gets a DbSet of a certain type from the context
		/// </summary>
		/// <param name="name">The name of the DbSet to retrieve</param>
		/// <typeparam name="T">The type to cast the DbSet to</typeparam>
		/// <returns>A DbSet of the given type</returns>
		[Obsolete("Please obtain the db set from the db context with generic type param instead.")]
		public DbSet<T> GetDbSet<T>(string name = null) where T : class, IAbstractModel
		{

			return GetType().GetProperty(name ?? typeof(T).Name).GetValue(this, null) as DbSet<T>;
		}

		/// <summary>
		/// Gets a DbSet as an IQueryable over the owner abstract model
		/// </summary>
		/// <param name="name">The name of the DbSet to retrieve</param>
		/// <returns>The DbSet as an IQueryable over the OwnerAbstractModel or null if it doesn't exist</returns>
		public IQueryable GetOwnerDbSet(string name)
		{
			return GetType().GetProperty(name).GetValue(this, null) as IQueryable;
		}

	}
}
