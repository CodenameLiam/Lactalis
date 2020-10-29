
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
	[Table("AgriSupplyDocumentCategory")]
	public class AgriSupplyDocumentCategoryEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public AgriSupplyDocumentCategoryEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminAgriSupplyDocumentCategoryEntity(),
			new FarmerAgriSupplyDocumentCategoryEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.AgriSupplyDocumentEntity"/>
		[EntityForeignKey("AgriSupplyDocumentss", "AgriSupplyDocumentCategory", false, typeof(AgriSupplyDocumentEntity))]
		public ICollection<AgriSupplyDocumentEntity> AgriSupplyDocumentss { get; set; }

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
			var modelList = models.Cast<AgriSupplyDocumentCategoryEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "AgriSupplyDocumentss":
					var agriSupplyDocumentsIds = modelList.SelectMany(x => x.AgriSupplyDocumentss.Select(m => m.Id)).ToList();
					var oldagriSupplyDocuments = await dbContext.AgriSupplyDocumentEntity
						.Where(m => m.AgriSupplyDocumentCategoryId.HasValue && ids.Contains(m.AgriSupplyDocumentCategoryId.Value))
						.Where(m => !agriSupplyDocumentsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var agriSupplyDocuments in oldagriSupplyDocuments)
					{
						agriSupplyDocuments.AgriSupplyDocumentCategoryId = null;
					}

					dbContext.AgriSupplyDocumentEntity.UpdateRange(oldagriSupplyDocuments);
					return oldagriSupplyDocuments.Count;
				default:
					return 0;
			}
		}
	}
}