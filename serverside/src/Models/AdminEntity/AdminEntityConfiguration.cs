
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class AdminEntityConfiguration : IEntityTypeConfiguration<AdminEntity>
	{
		public void Configure(EntityTypeBuilder<AdminEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

		}
	}
}