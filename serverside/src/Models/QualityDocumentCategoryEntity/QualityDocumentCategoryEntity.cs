
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
	[Table("QualityDocumentCategory")]
	public class QualityDocumentCategoryEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public QualityDocumentCategoryEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminQualityDocumentCategoryEntity(),
			new FarmerQualityDocumentCategoryEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.QualityDocumentEntity"/>
		[EntityForeignKey("QualityDocumentss", "QualityDocumentCategory", false, typeof(QualityDocumentEntity))]
		public ICollection<QualityDocumentEntity> QualityDocumentss { get; set; }

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
			var modelList = models.Cast<QualityDocumentCategoryEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "QualityDocumentss":
					var qualityDocumentsIds = modelList.SelectMany(x => x.QualityDocumentss.Select(m => m.Id)).ToList();
					var oldqualityDocuments = await dbContext.QualityDocumentEntity
						.Where(m => m.QualityDocumentCategoryId.HasValue && ids.Contains(m.QualityDocumentCategoryId.Value))
						.Where(m => !qualityDocumentsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var qualityDocuments in oldqualityDocuments)
					{
						qualityDocuments.QualityDocumentCategoryId = null;
					}

					dbContext.QualityDocumentEntity.UpdateRange(oldqualityDocuments);
					return oldqualityDocuments.Count;
				default:
					return 0;
			}
		}
	}
}