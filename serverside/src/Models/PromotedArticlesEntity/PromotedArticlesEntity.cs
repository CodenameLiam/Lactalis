
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
	[Table("PromotedArticles")]
	public class PromotedArticlesEntity : IOwnerAbstractModel	{
		[Key]
		public Guid Id { get; set; }
		public Guid Owner { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		[EntityAttribute]
		public String Name { get; set; }


		public PromotedArticlesEntity()
		{
		}

		[NotMapped]
		public IEnumerable<IAcl> Acls => new List<IAcl>
		{
			new AdminPromotedArticlesEntity(),
			new FarmerPromotedArticlesEntity(),
		};

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.NewsArticleEntity"/>
		[EntityForeignKey("NewsArticless", "PromotedArticles", false, typeof(NewsArticleEntity))]
		public ICollection<NewsArticleEntity> NewsArticless { get; set; }

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
			var modelList = models.Cast<PromotedArticlesEntity>().ToList();
			var ids = modelList.Select(t => t.Id).ToList();

			switch (reference)
			{
				case "NewsArticless":
					var newsArticlesIds = modelList.SelectMany(x => x.NewsArticless.Select(m => m.Id)).ToList();
					var oldnewsArticles = await dbContext.NewsArticleEntity
						.Where(m => m.PromotedArticlesId.HasValue && ids.Contains(m.PromotedArticlesId.Value))
						.Where(m => !newsArticlesIds.Contains(m.Id))
						.ToListAsync(cancellation);

					foreach (var newsArticles in oldnewsArticles)
					{
						newsArticles.PromotedArticlesId = null;
					}

					dbContext.NewsArticleEntity.UpdateRange(oldnewsArticles);
					return oldnewsArticles.Count;
				default:
					return 0;
			}
		}
	}
}