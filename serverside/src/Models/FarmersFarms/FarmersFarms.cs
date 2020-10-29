
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Enums;
using Lactalis.Security;
using Lactalis.Security.Acl;
using Microsoft.EntityFrameworkCore;


namespace Lactalis.Models {
	public class FarmersFarms : IOwnerAbstractModel
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid Owner { get; set; }

		public FarmersFarms() {}

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmerEntity"/>
		public Guid FarmersId { get; set; }
		[EntityForeignKey("Farmers", "Farmss", true, typeof(FarmerEntity))]
		public FarmerEntity Farmers { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.FarmEntity"/>
		public Guid FarmsId { get; set; }
		[EntityForeignKey("Farms", "Farmerss", true, typeof(FarmEntity))]
		public FarmEntity Farms { get; set; }

		public async Task BeforeSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
		{
		}

		public async Task AfterSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, ICollection<ChangeState> changes, CancellationToken cancellationToken = default)
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminFarmsEntity(),
			new FarmerFarmsEntity(),
		};


		public async Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			LactalisDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel => 0;
	}
}