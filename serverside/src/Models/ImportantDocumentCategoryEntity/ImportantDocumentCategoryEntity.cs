
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
	[Table("ImportantDocumentCategory")]
	public class ImportantDocumentCategoryEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public ImportantDocumentCategoryEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminImportantDocumentCategoryEntity(),
			new FarmerImportantDocumentCategoryEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.ImportantDocumentEntity"/>
		[EntityForeignKey("ImportantDocumentss", "DocumentCategory", false, typeof(ImportantDocumentEntity))]
		public ICollection<ImportantDocumentEntity> ImportantDocumentss { get; set; }

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
			var modelList = models.Cast<ImportantDocumentCategoryEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "ImportantDocumentss":
					var importantDocumentsIds = modelList.SelectMany(x => x.ImportantDocumentss.Select(m => m.Id)).ToList();
					var oldimportantDocuments = await dbContext.ImportantDocumentEntity
						.Where(m => m.DocumentCategoryId.HasValue && ids.Contains(m.DocumentCategoryId.Value))
						.Where(m => !importantDocumentsIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var importantDocuments in oldimportantDocuments)
					{
						importantDocuments.DocumentCategoryId = null;
					}

					dbContext.ImportantDocumentEntity.UpdateRange(oldimportantDocuments);
					return oldimportantDocuments.Count;
				default:
					return 0;
			}
		}
	}
}