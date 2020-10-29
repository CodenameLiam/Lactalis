
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class QualityDocumentEntityDto : ModelDto<QualityDocumentEntity>
	{
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? QualityDocumentCategoryId { get; set; }


		public QualityDocumentEntityDto(QualityDocumentEntity model)
		{
			LoadModelData(model);
		}

		public QualityDocumentEntityDto()
		{
		}

		public override QualityDocumentEntity ToModel()
		{

			return new QualityDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				QualityDocumentCategoryId  = QualityDocumentCategoryId,
			};
		}

		public override ModelDto<QualityDocumentEntity> LoadModelData(QualityDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			QualityDocumentCategoryId  = model.QualityDocumentCategoryId;


			return this;
		}
	}
}