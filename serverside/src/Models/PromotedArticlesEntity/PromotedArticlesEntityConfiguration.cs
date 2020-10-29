
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class PromotedArticlesEntityConfiguration : IEntityTypeConfiguration<PromotedArticlesEntity>
	{
		public void Configure(EntityTypeBuilder<PromotedArticlesEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.NewsArticless)
				.WithOne(e => e.PromotedArticles)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}