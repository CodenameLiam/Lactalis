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
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

namespace Lactalis.Models {
	// % protected region % [Configure entity attributes here] off begin
	[Table("TradingPostListing")]
	// % protected region % [Configure entity attributes here] end
	public class TradingPostListingEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise Title here] off begin
		[EntityAttribute]
		public String Title { get; set; }
		// % protected region % [Customise Title here] end

		// % protected region % [Customise Email here] off begin
		[EntityAttribute]
		public String Email { get; set; }
		// % protected region % [Customise Email here] end

		// % protected region % [Customise Phone here] off begin
		[EntityAttribute]
		public String Phone { get; set; }
		// % protected region % [Customise Phone here] end

		// % protected region % [Customise AdditionalInfo here] off begin
		[EntityAttribute]
		public String AdditionalInfo { get; set; }
		// % protected region % [Customise AdditionalInfo here] end

		// % protected region % [Customise AddressLine1 here] off begin
		[EntityAttribute]
		public String AddressLine1 { get; set; }
		// % protected region % [Customise AddressLine1 here] end

		// % protected region % [Customise AddressLine2 here] off begin
		[EntityAttribute]
		public String AddressLine2 { get; set; }
		// % protected region % [Customise AddressLine2 here] end

		// % protected region % [Customise PostalCode here] off begin
		[EntityAttribute]
		public String PostalCode { get; set; }
		// % protected region % [Customise PostalCode here] end

		// % protected region % [Customise ProductImage here] off begin
		[FileReference]
		public Guid? ProductImageId { get; set; }
		[EntityForeignKey("ProductImage", "TradingPostListingProductImage", false, typeof(UploadFile))]
		public UploadFile ProductImage { get; set; }
		// % protected region % [Customise ProductImage here] end

		// % protected region % [Customise Price here] off begin
		[EntityAttribute]
		public int? Price { get; set; }
		// % protected region % [Customise Price here] end

		// % protected region % [Customise PriceType here] off begin
		[EntityAttribute]
		public PriceType PriceType { get; set; }
		// % protected region % [Customise PriceType here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public TradingPostListingEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new AdminTradingPostListingEntity(),
			new FarmerTradingPostListingEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
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

			// % protected region % [Add any before save logic here] off begin
			// % protected region % [Add any before save logic here] end
		}

		public async Task AfterSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
			// % protected region % [Add any after save logic here] off begin
			// % protected region % [Add any after save logic here] end
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
				// % protected region % [Add any extra clean reference logic here] off begin
				// % protected region % [Add any extra clean reference logic here] end
				default:
					return 0;
			}
		}
		// % protected region % [Add any further references here] off begin
		// % protected region % [Add any further references here] end
	}
}