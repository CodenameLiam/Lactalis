
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class AgriSupplyDocumentCategoryEntityConfiguration : IEntityTypeConfiguration<AgriSupplyDocumentCategoryEntity>
	{
		public void Configure(EntityTypeBuilder<AgriSupplyDocumentCategoryEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.AgriSupplyDocumentss)
				.WithOne(e => e.AgriSupplyDocumentCategory)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}