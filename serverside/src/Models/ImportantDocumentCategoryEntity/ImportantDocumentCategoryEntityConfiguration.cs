
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class ImportantDocumentCategoryEntityConfiguration : IEntityTypeConfiguration<ImportantDocumentCategoryEntity>
	{
		public void Configure(EntityTypeBuilder<ImportantDocumentCategoryEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.ImportantDocumentss)
				.WithOne(e => e.DocumentCategory)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}