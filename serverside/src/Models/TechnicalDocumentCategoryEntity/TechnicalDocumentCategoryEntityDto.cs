
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class TechnicalDocumentCategoryEntityDto : ModelDto<TechnicalDocumentCategoryEntity>
	{
		public String Name { get; set; }


		public TechnicalDocumentCategoryEntityDto(TechnicalDocumentCategoryEntity model)
		{
			LoadModelData(model);
		}

		public TechnicalDocumentCategoryEntityDto()
		{
		}

		public override TechnicalDocumentCategoryEntity ToModel()
		{

			return new TechnicalDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<TechnicalDocumentCategoryEntity> LoadModelData(TechnicalDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}