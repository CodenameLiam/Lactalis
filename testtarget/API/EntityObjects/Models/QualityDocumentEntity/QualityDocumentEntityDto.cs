
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideQualityDocumentEntity = Lactalis.Models.QualityDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class QualityDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? QualityDocumentCategoryId { get; set; }

		public QualityDocumentEntityDto(QualityDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			QualityDocumentCategoryId = model.QualityDocumentCategoryId;
		}

		public QualityDocumentEntityDto(ServersideQualityDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			QualityDocumentCategoryId = model.QualityDocumentCategoryId;
		}

		public QualityDocumentEntity GetTesttargetQualityDocumentEntity()
		{
			return new QualityDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				QualityDocumentCategoryId = QualityDocumentCategoryId,
			};
		}

		public ServersideQualityDocumentEntity GetServersideQualityDocumentEntity()
		{
			return new ServersideQualityDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				QualityDocumentCategoryId = QualityDocumentCategoryId,
			};
		}

		public static ServersideQualityDocumentEntity Convert(QualityDocumentEntity model)
		{
			var dto = new QualityDocumentEntityDto(model);
			return dto.GetServersideQualityDocumentEntity();
		}

		public static QualityDocumentEntity Convert(ServersideQualityDocumentEntity model)
		{
			var dto = new QualityDocumentEntityDto(model);
			return dto.GetTesttargetQualityDocumentEntity();
		}
	}
}