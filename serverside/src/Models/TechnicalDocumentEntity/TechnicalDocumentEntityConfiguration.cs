
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class TechnicalDocumentEntityConfiguration : IEntityTypeConfiguration<TechnicalDocumentEntity>
	{
		public void Configure(EntityTypeBuilder<TechnicalDocumentEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

			builder
				.HasOne(e => e.TechnicalDocumentCategory)
				.WithMany(e => e.TechnicalDocumentss)
				.OnDelete(DeleteBehavior.Restrict);
			builder
				.HasOne(e => e.File)
				.WithOne(e => e.TechnicalDocumentFile)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}