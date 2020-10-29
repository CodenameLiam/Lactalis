
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class TradingPostListingEntityConfiguration : IEntityTypeConfiguration<TradingPostListingEntity>
	{
		public void Configure(EntityTypeBuilder<TradingPostListingEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.Farmer)
				.WithMany(e => e.TradingPostListingss)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.ProductImage)
				.WithOne(e => e.TradingPostListingProductImage)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}