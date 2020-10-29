
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
	[Table("TechnicalDocumentCategory")]
	public class TechnicalDocumentCategoryEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public TechnicalDocumentCategoryEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminTechnicalDocumentCategoryEntity(),
			new FarmerTechnicalDocumentCategoryEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TechnicalDocumentEntity"/>
		[EntityForeignKey("TechnicalDocumentss", "TechnicalDocumentCategory", false, typeof(TechnicalDocumentEntity))]
		public ICollection<TechnicalDocumentEntity> TechnicalDocumentss { get; set; }

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
			var modelList = models.Cast<TechnicalDocumentCategoryEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "TechnicalDocumentss":
					var technicalDocumentsIds = modelList.SelectMany(x => x.TechnicalDocumentss.Select(m => m.Id)).ToList();
					var oldtechnicalDocuments = await dbContext.TechnicalDocumentEntity
						.Where(m => m.TechnicalDocumentCategoryId.HasValue && ids.Contains(m.TechnicalDocumentCategoryId.Value))
						.Where(m => !technicalDocumentsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var technicalDocuments in oldtechnicalDocuments)
					{
						technicalDocuments.TechnicalDocumentCategoryId = null;
					}

					dbContext.TechnicalDocumentEntity.UpdateRange(oldtechnicalDocuments);
					return oldtechnicalDocuments.Count;
				default:
					return 0;
			}
		}
	}
}