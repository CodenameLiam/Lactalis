
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideQualityDocumentCategoryEntity = Lactalis.Models.QualityDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class QualityDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<QualityDocumentEntity> QualityDocumentss { get; set; }

		public QualityDocumentCategoryEntityDto(QualityDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			QualityDocumentss = model.QualityDocumentss;
		}

		public QualityDocumentCategoryEntityDto(ServersideQualityDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			QualityDocumentss = model.QualityDocumentss.Select(QualityDocumentEntityDto.Convert).ToList();
		}

		public QualityDocumentCategoryEntity GetTesttargetQualityDocumentCategoryEntity()
		{
			return new QualityDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				QualityDocumentss = QualityDocumentss,
			};
		}

		public ServersideQualityDocumentCategoryEntity GetServersideQualityDocumentCategoryEntity()
		{
			return new ServersideQualityDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				QualityDocumentss = QualityDocumentss?.Select(QualityDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideQualityDocumentCategoryEntity Convert(QualityDocumentCategoryEntity model)
		{
			var dto = new QualityDocumentCategoryEntityDto(model);
			return dto.GetServersideQualityDocumentCategoryEntity();
		}

		public static QualityDocumentCategoryEntity Convert(ServersideQualityDocumentCategoryEntity model)
		{
			var dto = new QualityDocumentCategoryEntityDto(model);
			return dto.GetTesttargetQualityDocumentCategoryEntity();
		}
	}
}