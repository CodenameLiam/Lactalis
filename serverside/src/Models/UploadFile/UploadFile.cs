
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Lactalis.Services.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lactalis.Models
{
	/// <summary>
	/// A class that represents a file that is saved to a storage provider like the file system or a cloud blob store.
	/// </summary>
	[Table("__Files")]
	public class UploadFile : IAbstractModel
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		/// <summary>
		/// The container or bucket that the file is to be placed in
		/// </summary>
		[Required]
		[EntityAttribute]
		public string Container { get; set; }
		
		/// <summary>
		/// The id of the file that is saved to the storage provider
		/// </summary>
		[Required]
		[EntityAttribute]
		public string FileId { get; set; }
		
		/// <summary>
		/// The name of the file as it was uploaded to the server
		/// </summary>
		[Required]
		[EntityAttribute]
		public string FileName { get; set; }
		
		/// <summary>
		/// The content type of the file
		/// </summary>
		public string ContentType { get; set; }
		
		/// <summary>
		/// The length of the file in bytes
		/// </summary>
		[EntityAttribute]
		public long Length { get; set; }

		/// <summary>
		/// Link to the TradingPostListing entity which contains a file on the ProductImage attribute
		/// </summary>
		public TradingPostListingEntity? TradingPostListingProductImage { get; set; }

		/// <summary>
		/// Link to the QualityDocument entity which contains a file on the File attribute
		/// </summary>
		public QualityDocumentEntity? QualityDocumentFile { get; set; }

		/// <summary>
		/// Link to the TechnicalDocument entity which contains a file on the File attribute
		/// </summary>
		public TechnicalDocumentEntity? TechnicalDocumentFile { get; set; }

		/// <summary>
		/// Link to the ImportantDocument entity which contains a file on the File attribute
		/// </summary>
		public ImportantDocumentEntity? ImportantDocumentFile { get; set; }

		/// <summary>
		/// Link to the NewsArticle entity which contains a file on the FeatureImage attribute
		/// </summary>
		public NewsArticleEntity? NewsArticleFeatureImage { get; set; }

		/// <summary>
		/// Link to the SustainabilityPost entity which contains a file on the Image attribute
		/// </summary>
		public SustainabilityPostEntity? SustainabilityPostImage { get; set; }

		/// <summary>
		/// Link to the SustainabilityPost entity which contains a file on the File attribute
		/// </summary>
		public SustainabilityPostEntity? SustainabilityPostFile { get; set; }

		/// <summary>
		/// Link to the AgriSupplyDocument entity which contains a file on the File attribute
		/// </summary>
		public AgriSupplyDocumentEntity? AgriSupplyDocumentFile { get; set; }

		public async Task BeforeSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
		{
			return;
		}

		public async Task AfterSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, ICollection<ChangeState> changes, CancellationToken cancellationToken = default)
		{
			if (operation == EntityState.Deleted)
			{
				try
				{
					var storageProvider = serviceProvider.GetRequiredService<IUploadStorageProvider>();
					await storageProvider.DeleteAsync(new StorageDeleteOptions
					{
						Container = Container,
						FileName = FileId,
					});
				}
				catch (Exception exception)
				{
					// Ignore errors to not destroy the operation since it is non critical for this to succeed.
					Log.Error(
						"Failed to delete file from storage provider. Error: {exception}",
						exception,
						this);
				}
			}
			return;
		}
	}
}