
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
	[Table("TradingPostCategory")]
	public class TradingPostCategoryEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public TradingPostCategoryEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminTradingPostCategoryEntity(),
			new FarmerTradingPostCategoryEntity(),
		};

		/// <summary>
		/// Outgoing many to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostListingsTradingPostCategories"/>
		[EntityForeignKey("TradingPostListingss", "TradingPostCategories", false, typeof(TradingPostListingsTradingPostCategories))]
		public ICollection<TradingPostListingsTradingPostCategories> TradingPostListingss { get; set; }

		public async Task BeforeSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
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
			var modelList = models.Cast<TradingPostCategoryEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "TradingPostListingss":
					var tradingPostListingsEntities = modelList
						.SelectMany(m => m.TradingPostListingss)
						.Select(m => m.Id);
					var oldTradingPostListings = await dbContext.TradingPostListingsTradingPostCategories
						.Where(m => ids.Contains(m.TradingPostCategoriesId) && !tradingPostListingsEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.TradingPostListingsTradingPostCategories.RemoveRange(oldTradingPostListings);

					return oldTradingPostListings.Count;
				default:
					return 0;
			}
		}
	}
}