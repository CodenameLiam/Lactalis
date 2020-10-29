
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Lactalis.Models {
	public class TradingPostCategoryEntityConfiguration : IEntityTypeConfiguration<TradingPostCategoryEntity>
	{
		public void Configure(EntityTypeBuilder<TradingPostCategoryEntity> builder)
		{
			AbstractModelConfiguration.Configure(builder);

		}
	}
}