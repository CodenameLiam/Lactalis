
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class FarmersFarmsConfiguration : IEntityTypeConfiguration<FarmersFarms>
	{
		public void Configure(EntityTypeBuilder<FarmersFarms> builder)
		{
			AbstractModelConfiguration.Configure(builder);
			
			builder
				.HasOne(e => e.Farmers)
				.WithMany(e => e.Farmss)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(e => e.Farms)
				.WithMany(e => e.Farmerss)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}