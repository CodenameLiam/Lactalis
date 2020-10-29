
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class QualityDocumentCategoryEntityConfiguration : IEntityTypeConfiguration<QualityDocumentCategoryEntity>
	{
		public void Configure(EntityTypeBuilder<QualityDocumentCategoryEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.QualityDocumentss)
				.WithOne(e => e.QualityDocumentCategory)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}