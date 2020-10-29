
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class FarmEntityConfiguration : IEntityTypeConfiguration<FarmEntity>
	{
		public void Configure(EntityTypeBuilder<FarmEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.Pickupss)
				.WithOne(e => e.Farm)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}