
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class NewsArticleEntityConfiguration : IEntityTypeConfiguration<NewsArticleEntity>
	{
		public void Configure(EntityTypeBuilder<NewsArticleEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.PromotedArticles)
				.WithMany(e => e.NewsArticless)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.FeatureImage)
				.WithOne(e => e.NewsArticleFeatureImage)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}