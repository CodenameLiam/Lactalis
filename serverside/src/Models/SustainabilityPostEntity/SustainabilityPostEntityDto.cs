
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class SustainabilityPostEntityDto : ModelDto<SustainabilityPostEntity>
	{
		public String Title { get; set; }
		public Guid? ImageId { get; set; }
		public Guid? FileId { get; set; }
		public String Content { get; set; }


		public SustainabilityPostEntityDto(SustainabilityPostEntity model)
		{
			LoadModelData(model);
		}

		public SustainabilityPostEntityDto()
		{
		}

		public override SustainabilityPostEntity ToModel()
		{

			return new SustainabilityPostEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				ImageId = ImageId,
				FileId = FileId,
				Content = Content,
			};
		}

		public override ModelDto<SustainabilityPostEntity> LoadModelData(SustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;


			return this;
		}
	}
}