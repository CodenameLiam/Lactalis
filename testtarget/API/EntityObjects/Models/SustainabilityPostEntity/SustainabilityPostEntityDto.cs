
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideSustainabilityPostEntity = Lactalis.Models.SustainabilityPostEntity;

namespace APITests.EntityObjects.Models
{
	public class SustainabilityPostEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Title { get; set; }
		public Guid? ImageId { get; set; }
		public Guid? FileId { get; set; }
		public String Content { get; set; }


		public SustainabilityPostEntityDto(SustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;
		}

		public SustainabilityPostEntityDto(ServersideSustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;
		}

		public SustainabilityPostEntity GetTesttargetSustainabilityPostEntity()
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

		public ServersideSustainabilityPostEntity GetServersideSustainabilityPostEntity()
		{
			return new ServersideSustainabilityPostEntity
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

		public static ServersideSustainabilityPostEntity Convert(SustainabilityPostEntity model)
		{
			var dto = new SustainabilityPostEntityDto(model);
			return dto.GetServersideSustainabilityPostEntity();
		}

		public static SustainabilityPostEntity Convert(ServersideSustainabilityPostEntity model)
		{
			var dto = new SustainabilityPostEntityDto(model);
			return dto.GetTesttargetSustainabilityPostEntity();
		}
	}
}