
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class MilkTestEntityConfiguration : IEntityTypeConfiguration<MilkTestEntity>
	{
		public void Configure(EntityTypeBuilder<MilkTestEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.Farm)
				.WithMany(e => e.Pickupss)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}