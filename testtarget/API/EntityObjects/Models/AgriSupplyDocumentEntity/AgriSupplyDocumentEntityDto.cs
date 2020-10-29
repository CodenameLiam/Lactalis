
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideAgriSupplyDocumentEntity = Lactalis.Models.AgriSupplyDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class AgriSupplyDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? AgriSupplyDocumentCategoryId { get; set; }

		public AgriSupplyDocumentEntityDto(AgriSupplyDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			AgriSupplyDocumentCategoryId = model.AgriSupplyDocumentCategoryId;
		}

		public AgriSupplyDocumentEntityDto(ServersideAgriSupplyDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			AgriSupplyDocumentCategoryId = model.AgriSupplyDocumentCategoryId;
		}

		public AgriSupplyDocumentEntity GetTesttargetAgriSupplyDocumentEntity()
		{
			return new AgriSupplyDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				AgriSupplyDocumentCategoryId = AgriSupplyDocumentCategoryId,
			};
		}

		public ServersideAgriSupplyDocumentEntity GetServersideAgriSupplyDocumentEntity()
		{
			return new ServersideAgriSupplyDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				AgriSupplyDocumentCategoryId = AgriSupplyDocumentCategoryId,
			};
		}

		public static ServersideAgriSupplyDocumentEntity Convert(AgriSupplyDocumentEntity model)
		{
			var dto = new AgriSupplyDocumentEntityDto(model);
			return dto.GetServersideAgriSupplyDocumentEntity();
		}

		public static AgriSupplyDocumentEntity Convert(ServersideAgriSupplyDocumentEntity model)
		{
			var dto = new AgriSupplyDocumentEntityDto(model);
			return dto.GetTesttargetAgriSupplyDocumentEntity();
		}
	}
}