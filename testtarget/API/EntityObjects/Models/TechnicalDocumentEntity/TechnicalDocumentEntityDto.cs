
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideTechnicalDocumentEntity = Lactalis.Models.TechnicalDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class TechnicalDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? TechnicalDocumentCategoryId { get; set; }

		public TechnicalDocumentEntityDto(TechnicalDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			TechnicalDocumentCategoryId = model.TechnicalDocumentCategoryId;
		}

		public TechnicalDocumentEntityDto(ServersideTechnicalDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			TechnicalDocumentCategoryId = model.TechnicalDocumentCategoryId;
		}

		public TechnicalDocumentEntity GetTesttargetTechnicalDocumentEntity()
		{
			return new TechnicalDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				TechnicalDocumentCategoryId = TechnicalDocumentCategoryId,
			};
		}

		public ServersideTechnicalDocumentEntity GetServersideTechnicalDocumentEntity()
		{
			return new ServersideTechnicalDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				TechnicalDocumentCategoryId = TechnicalDocumentCategoryId,
			};
		}

		public static ServersideTechnicalDocumentEntity Convert(TechnicalDocumentEntity model)
		{
			var dto = new TechnicalDocumentEntityDto(model);
			return dto.GetServersideTechnicalDocumentEntity();
		}

		public static TechnicalDocumentEntity Convert(ServersideTechnicalDocumentEntity model)
		{
			var dto = new TechnicalDocumentEntityDto(model);
			return dto.GetTesttargetTechnicalDocumentEntity();
		}
	}
}