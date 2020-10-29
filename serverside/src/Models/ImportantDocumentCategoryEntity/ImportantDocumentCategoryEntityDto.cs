
using System;
using System.Linq;
using System.Collections.Generic;
 

namespace Lactalis.Models
{
	public class ImportantDocumentCategoryEntityDto : ModelDto<ImportantDocumentCategoryEntity>
	{
		public String Name { get; set; }


		public ImportantDocumentCategoryEntityDto(ImportantDocumentCategoryEntity model)
		{
			LoadModelData(model);
		}

		public ImportantDocumentCategoryEntityDto()
		{
		}

		public override ImportantDocumentCategoryEntity ToModel()
		{

			return new ImportantDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<ImportantDocumentCategoryEntity> LoadModelData(ImportantDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}