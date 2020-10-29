
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideTechnicalDocumentCategoryEntity = Lactalis.Models.TechnicalDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class TechnicalDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<TechnicalDocumentEntity> TechnicalDocumentss { get; set; }

		public TechnicalDocumentCategoryEntityDto(TechnicalDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TechnicalDocumentss = model.TechnicalDocumentss;
		}

		public TechnicalDocumentCategoryEntityDto(ServersideTechnicalDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TechnicalDocumentss = model.TechnicalDocumentss.Select(TechnicalDocumentEntityDto.Convert).ToList();
		}

		public TechnicalDocumentCategoryEntity GetTesttargetTechnicalDocumentCategoryEntity()
		{
			return new TechnicalDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TechnicalDocumentss = TechnicalDocumentss,
			};
		}

		public ServersideTechnicalDocumentCategoryEntity GetServersideTechnicalDocumentCategoryEntity()
		{
			return new ServersideTechnicalDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TechnicalDocumentss = TechnicalDocumentss?.Select(TechnicalDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideTechnicalDocumentCategoryEntity Convert(TechnicalDocumentCategoryEntity model)
		{
			var dto = new TechnicalDocumentCategoryEntityDto(model);
			return dto.GetServersideTechnicalDocumentCategoryEntity();
		}

		public static TechnicalDocumentCategoryEntity Convert(ServersideTechnicalDocumentCategoryEntity model)
		{
			var dto = new TechnicalDocumentCategoryEntityDto(model);
			return dto.GetTesttargetTechnicalDocumentCategoryEntity();
		}
	}
}