
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class AgriSupplyDocumentEntityConfiguration : IEntityTypeConfiguration<AgriSupplyDocumentEntity>
	{
		public void Configure(EntityTypeBuilder<AgriSupplyDocumentEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.AgriSupplyDocumentCategory)
				.WithMany(e => e.AgriSupplyDocumentss)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.File)
				.WithOne(e => e.AgriSupplyDocumentFile)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}