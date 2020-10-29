
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class TechnicalDocumentEntityDto : ModelDto<TechnicalDocumentEntity>
	{
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? TechnicalDocumentCategoryId { get; set; }


		public TechnicalDocumentEntityDto(TechnicalDocumentEntity model)
		{
			LoadModelData(model);
		}

		public TechnicalDocumentEntityDto()
		{
		}

		public override TechnicalDocumentEntity ToModel()
		{

			return new TechnicalDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				TechnicalDocumentCategoryId  = TechnicalDocumentCategoryId,
			};
		}

		public override ModelDto<TechnicalDocumentEntity> LoadModelData(TechnicalDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			TechnicalDocumentCategoryId  = model.TechnicalDocumentCategoryId;


			return this;
		}
	}
}