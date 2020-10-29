
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
	[Table("Farm")]
	public class FarmEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Code { get; set; }

		[EntityAttribute]
		public String Name { get; set; }

		[EntityAttribute]
		public State State { get; set; }


		public FarmEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminFarmEntity(),
			new FarmerFarmEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.MilkTestEntity"/>
		[EntityForeignKey("Pickupss", "Farm", false, typeof(MilkTestEntity))]
		public ICollection<MilkTestEntity> Pickupss { get; set; }

		/// <summary>
		/// Outgoing many to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmersFarms"/>
		[EntityForeignKey("Farmerss", "Farms", false, typeof(FarmersFarms))]
		public ICollection<FarmersFarms> Farmerss { get; set; }

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
			var modelList = models.Cast<FarmEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "Pickupss":
					var pickupsIds = modelList.SelectMany(x => x.Pickupss.Select(m => m.Id)).ToList();
					var oldpickups = await dbContext.MilkTestEntity
						.Where(m => m.FarmId.HasValue && ids.Contains(m.FarmId.Value))
						.Where(m => !pickupsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var pickups in oldpickups)
					{
						pickups.FarmId = null;
					}

					dbContext.MilkTestEntity.UpdateRange(oldpickups);
					return oldpickups.Count;
				case "Farmerss":
					var farmersEntities = modelList
						.SelectMany(m => m.Farmerss)
						.Select(m => m.Id);
					var oldFarmers = await dbContext.FarmersFarms
						.Where(m => ids.Contains(m.FarmsId) && !farmersEntities.Contains(m.Id))
						.ToListAsync(cancellation);
					dbContext.FarmersFarms.RemoveRange(oldFarmers);

					return oldFarmers.Count;
				default:
					return 0;
			}
		}
	}
}