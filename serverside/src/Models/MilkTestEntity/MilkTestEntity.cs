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
	[Table("MilkTest")]
	// % protected region % [Configure entity attributes here] end
	public class MilkTestEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		// % protected region % [Customise Time here] off begin
		[EntityAttribute]
		public DateTime? Time { get; set; }
		// % protected region % [Customise Time here] end

		// % protected region % [Customise Volume here] off begin
		[EntityAttribute]
		public int? Volume { get; set; }
		// % protected region % [Customise Volume here] end

		// % protected region % [Customise Temperature here] off begin
		[EntityAttribute]
		public Double? Temperature { get; set; }
		// % protected region % [Customise Temperature here] end

		// % protected region % [Customise MilkFat here] off begin
		[EntityAttribute]
		public Double? MilkFat { get; set; }
		// % protected region % [Customise MilkFat here] end

		// % protected region % [Customise Protein here] off begin
		[EntityAttribute]
		public Double? Protein { get; set; }
		// % protected region % [Customise Protein here] end

		// % protected region % [Add any further attributes here] off begin
		// % protected region % [Add any further attributes here] end

		public MilkTestEntity()
		{
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			// % protected region % [Override ACLs here] off begin
			new AdminMilkTestEntity(),
			new FarmerMilkTestEntity(),
			// % protected region % [Override ACLs here] end
			// % protected region % [Add any further ACL entries here] off begin
			// % protected region % [Add any further ACL entries here] end
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
			var modelList = models.Cast<MilkTestEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
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