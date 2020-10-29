
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
	[Table("Farmer")]
	public class FarmerEntity : User, IOwnerAbstractModel	{

		public FarmerEntity()
		{
		}

		[NotMapped]
		public override IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminFarmerEntity(),
			new FarmerFarmerEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostListingEntity"/>
		[EntityForeignKey("TradingPostListingss", "Farmer", false, typeof(TradingPostListingEntity))]
		public ICollection<TradingPostListingEntity> TradingPostListingss { get; set; }

		/// <summary>
		/// Incoming many to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmersFarms"/>
		[EntityForeignKey("Farmss", "Farmers", false, typeof(FarmersFarms))]
		public ICollection<FarmersFarms> Farmss { get; set; }

		public override async Task BeforeSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
		}

		public override async Task AfterSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			ICollection<ChangeState> changes,
			CancellationToken cancellationToken = default)
		{
		}

		public async override Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			LactalisDBContext dbContext,
			CancellationToken cancellation = default)
		{
			var modelList = models.Cast<FarmerEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "TradingPostListingss":
					var tradingPostListingsIds = modelList.SelectMany(x => x.TradingPostListingss.Select(m => m.Id)).ToList();
					var oldtradingPostListings = await dbContext.TradingPostListingEntity
						.Where(m => m.FarmerId.HasValue && ids.Contains(m.FarmerId.Value))
						.Where(m => !tradingPostListingsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var tradingPostListings in oldtradingPostListings)
					{
						tradingPostListings.FarmerId = null;
					}

					dbContext.TradingPostListingEntity.UpdateRange(oldtradingPostListings);
					return oldtradingPostListings.Count;
				case "Farmss":
					var farmsEntities = modelList
						.SelectMany(m => m.Farmss)
						.Select(m => m.Id);
					var oldFarms = await dbContext.FarmersFarms
						.Where(m => ids.Contains(m.FarmersId) && !farmsEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.FarmersFarms.RemoveRange(oldFarms);

					return oldFarms.Count;
				default:
					return 0;
			}
		}
	}
}