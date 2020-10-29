
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class ImportantDocumentEntityConfiguration : IEntityTypeConfiguration<ImportantDocumentEntity>
	{
		public void Configure(EntityTypeBuilder<ImportantDocumentEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.DocumentCategory)
				.WithMany(e => e.ImportantDocumentss)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.File)
				.WithOne(e => e.ImportantDocumentFile)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}