
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class FarmerEntityConfiguration : IEntityTypeConfiguration<FarmerEntity>
	{
		public void Configure(EntityTypeBuilder<FarmerEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.TradingPostListingss)
				.WithOne(e => e.Farmer)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}