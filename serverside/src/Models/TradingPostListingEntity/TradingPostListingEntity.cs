
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Enums;
using Lactalis.Security;
using Lactalis.Security.Acl;
using Lactalis.Validators;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;


namespace Lactalis.Models {
	[Table("TradingPostListing")]
	public class TradingPostListingEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Title { get; set; }

		[EntityAttribute]
		public String Email { get; set; }

		[EntityAttribute]
		public String Phone { get; set; }

		[EntityAttribute]
		public String AdditionalInfo { get; set; }

		[EntityAttribute]
		public String AddressLine1 { get; set; }

		[EntityAttribute]
		public String AddressLine2 { get; set; }

		[EntityAttribute]
		public String PostalCode { get; set; }

		[FileReference]
		public Guid? ProductImageId { get; set; }
		[EntityForeignKey("ProductImage", "TradingPostListingProductImage", false, typeof(UploadFile))]
		public UploadFile ProductImage { get; set; }

		[EntityAttribute]
		public int? Price { get; set; }

		[EntityAttribute]
		public PriceType PriceType { get; set; }


		public TradingPostListingEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminTradingPostListingEntity(),
			new FarmerTradingPostListingEntity(),
		};

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmerEntity"/>
		public Guid? FarmerId { get; set; }
		[EntityForeignKey("Farmer", "TradingPostListingss", false, typeof(FarmerEntity))]
		public FarmerEntity Farmer { get; set; }

		/// <summary>
		/// Incoming many to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostListingsTradingPostCategories"/>
		[EntityForeignKey("TradingPostCategoriess", "TradingPostListings", false, typeof(TradingPostListingsTradingPostCategories))]
		public ICollection<TradingPostListingsTradingPostCategories> TradingPostCategoriess { get; set; }

		public async Task BeforeSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			if (operation == EntityState.Deleted)
			{
				if (ProductImageId.HasValue)
				{
					var existingFile = dbContext.Files.FirstOrDefault(f => f.Id == ProductImageId.Value);
					if (existingFile != null)
					{
						dbContext.Files.Remove(existingFile);
						await existingFile.BeforeSave(EntityState.Deleted, dbContext, serviceProvider);
					}
				}
			}

		}

		public async Task AfterSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
		}

		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			LactalisDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel
		{
			var modelList = models.Cast<TradingPostListingEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "TradingPostCategoriess":
					var tradingPostCategoriesEntities = modelList
						.SelectMany(m => m.TradingPostCategoriess)
						.Select(m => m.Id);
					var oldTradingPostCategories = await dbContext.TradingPostListingsTradingPostCategories
						.Where(m => ids.Contains(m.TradingPostListingsId) && !tradingPostCategoriesEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.TradingPostListingsTradingPostCategories.RemoveRange(oldTradingPostCategories);

					return oldTradingPostCategories.Count;
				default:
					return 0;
			}
		}
	}
}