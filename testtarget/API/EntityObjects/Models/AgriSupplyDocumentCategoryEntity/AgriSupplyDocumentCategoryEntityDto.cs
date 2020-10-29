
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideAgriSupplyDocumentCategoryEntity = Lactalis.Models.AgriSupplyDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class AgriSupplyDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<AgriSupplyDocumentEntity> AgriSupplyDocumentss { get; set; }

		public AgriSupplyDocumentCategoryEntityDto(AgriSupplyDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			AgriSupplyDocumentss = model.AgriSupplyDocumentss;
		}

		public AgriSupplyDocumentCategoryEntityDto(ServersideAgriSupplyDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			AgriSupplyDocumentss = model.AgriSupplyDocumentss.Select(AgriSupplyDocumentEntityDto.Convert).ToList();
		}

		public AgriSupplyDocumentCategoryEntity GetTesttargetAgriSupplyDocumentCategoryEntity()
		{
			return new AgriSupplyDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				AgriSupplyDocumentss = AgriSupplyDocumentss,
			};
		}

		public ServersideAgriSupplyDocumentCategoryEntity GetServersideAgriSupplyDocumentCategoryEntity()
		{
			return new ServersideAgriSupplyDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				AgriSupplyDocumentss = AgriSupplyDocumentss?.Select(AgriSupplyDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideAgriSupplyDocumentCategoryEntity Convert(AgriSupplyDocumentCategoryEntity model)
		{
			var dto = new AgriSupplyDocumentCategoryEntityDto(model);
			return dto.GetServersideAgriSupplyDocumentCategoryEntity();
		}

		public static AgriSupplyDocumentCategoryEntity Convert(ServersideAgriSupplyDocumentCategoryEntity model)
		{
			var dto = new AgriSupplyDocumentCategoryEntityDto(model);
			return dto.GetTesttargetAgriSupplyDocumentCategoryEntity();
		}
	}
}