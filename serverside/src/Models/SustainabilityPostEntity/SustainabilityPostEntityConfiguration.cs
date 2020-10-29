
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class SustainabilityPostEntityConfiguration : IEntityTypeConfiguration<SustainabilityPostEntity>
	{
		public void Configure(EntityTypeBuilder<SustainabilityPostEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.Image)
				.WithOne(e => e.SustainabilityPostImage)
				.OnDelete(DeleteBehavior.SetNull);
			builder
				.HasOne(e => e.File)
				.WithOne(e => e.SustainabilityPostFile)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}