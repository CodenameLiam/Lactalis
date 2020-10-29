
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class QualityDocumentCategoryEntityDto : ModelDto<QualityDocumentCategoryEntity>
	{
		public String Name { get; set; }


		public QualityDocumentCategoryEntityDto(QualityDocumentCategoryEntity model)
		{
			LoadModelData(model);
		}

		public QualityDocumentCategoryEntityDto()
		{
		}

		public override QualityDocumentCategoryEntity ToModel()
		{

			return new QualityDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<QualityDocumentCategoryEntity> LoadModelData(QualityDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}