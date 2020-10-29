
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class TechnicalDocumentCategoryEntityConfiguration : IEntityTypeConfiguration<TechnicalDocumentCategoryEntity>
	{
		public void Configure(EntityTypeBuilder<TechnicalDocumentCategoryEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasMany(e => e.TechnicalDocumentss)
				.WithOne(e => e.TechnicalDocumentCategory)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}