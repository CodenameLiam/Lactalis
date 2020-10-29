
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lactalis.Models
{
	public class UploadFileConfiguration : IEntityTypeConfiguration<UploadFile>
	{
		public void Configure(EntityTypeBuilder<UploadFile> builder)
		{
			AbstractModelConfiguration.Configure(builder);
		}
	}
}