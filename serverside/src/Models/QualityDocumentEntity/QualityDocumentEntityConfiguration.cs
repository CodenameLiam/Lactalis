
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class QualityDocumentEntityConfiguration : IEntityTypeConfiguration<QualityDocumentEntity>
	{
		public void Configure(EntityTypeBuilder<QualityDocumentEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.QualityDocumentCategory)
				.WithMany(e => e.QualityDocumentss)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.File)
				.WithOne(e => e.QualityDocumentFile)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}