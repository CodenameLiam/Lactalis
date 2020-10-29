
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class TradingPostListingsTradingPostCategoriesConfiguration : IEntityTypeConfiguration<TradingPostListingsTradingPostCategories>
	{
		public void Configure(EntityTypeBuilder<TradingPostListingsTradingPostCategories> builder)
		{
			AbstractModelConfiguration.Configure(builder);
			
			builder
				.HasOne(e => e.TradingPostListings)
				.WithMany(e => e.TradingPostCategoriess)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(e => e.TradingPostCategories)
				.WithMany(e => e.TradingPostListingss)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}