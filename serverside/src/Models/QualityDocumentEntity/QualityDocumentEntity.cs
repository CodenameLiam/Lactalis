
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
	[Table("QualityDocument")]
	public class QualityDocumentEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[FileReference]
		public Guid? FileId { get; set; }
		[EntityForeignKey("File", "QualityDocumentFile", false, typeof(UploadFile))]
		public UploadFile File { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public QualityDocumentEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminQualityDocumentEntity(),
			new FarmerQualityDocumentEntity(),
		};

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.QualityDocumentCategoryEntity"/>
		public Guid? QualityDocumentCategoryId { get; set; }
		[EntityForeignKey("QualityDocumentCategory", "QualityDocumentss", false, typeof(QualityDocumentCategoryEntity))]
		public QualityDocumentCategoryEntity QualityDocumentCategory { get; set; }

		public async Task BeforeSave(
			EntityState operation,
			LactalisDBContext dbContext,
			IServiceProvider serviceProvider,
			CancellationToken cancellationToken = default)
		{
			if (operation == EntityState.Deleted)
			{
				if (FileId.HasValue)
				{
					var existingFile = dbContext.Files.FirstOrDefault(f => f.Id == FileId.Value);
					if (existingFile != null)
					{
						dbContext.Files.Remove(existingFile);
						await existingFile.BeforeSave(EntityState.Deleted, dbContext, serviceProvider);
					}
				}
			}

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
			var modelList = models.Cast<QualityDocumentEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				default:
					return 0;
			}
		}
	}
}