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
	[Table("Farm")]
	// % protected region % [Configure entity attributes here] end
	public class FarmEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise Code here] off begin
		[EntityAttribute]
		public String Code { get; set; }
		// % protected region % [Customise Code here] end

		// % protected region % [Customise Name here] off begin
		[EntityAttribute]
		public String Name { get; set; }
		// % protected region % [Customise Name here] end

		// % protected region % [Customise State here] off begin
		[EntityAttribute]
		public State State { get; set; }
		// % protected region % [Customise State here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public FarmEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new AdminFarmEntity(),
			new FarmerFarmEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
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