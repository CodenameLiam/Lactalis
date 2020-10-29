
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
	[Table("MilkTest")]
	public class MilkTestEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public DateTime? Time { get; set; }

		[EntityAttribute]
		public int? Volume { get; set; }

		[EntityAttribute]
		public Double? Temperature { get; set; }

		[EntityAttribute]
		public Double? MilkFat { get; set; }

		[EntityAttribute]
		public Double? Protein { get; set; }


		public MilkTestEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminMilkTestEntity(),
			new FarmerMilkTestEntity(),
		};

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmEntity"/>
		public Guid? FarmId { get; set; }
		[EntityForeignKey("Farm", "Pickupss", false, typeof(FarmEntity))]
		public FarmEntity Farm { get; set; }

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
			var modelList = models.Cast<MilkTestEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				default:
					return 0;
			}
		}
	}
}