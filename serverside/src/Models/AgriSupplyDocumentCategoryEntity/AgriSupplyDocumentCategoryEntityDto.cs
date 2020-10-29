
using System;
using System.Linq;
using System.Collections.Generic;
 

namespace Lactalis.Models
{
	public class AgriSupplyDocumentCategoryEntityDto : ModelDto<AgriSupplyDocumentCategoryEntity>
	{
		public String Name { get; set; }


		public AgriSupplyDocumentCategoryEntityDto(AgriSupplyDocumentCategoryEntity model)
		{
			LoadModelData(model);
		}

		public AgriSupplyDocumentCategoryEntityDto()
		{
		}

		public override AgriSupplyDocumentCategoryEntity ToModel()
		{

			return new AgriSupplyDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<AgriSupplyDocumentCategoryEntity> LoadModelData(AgriSupplyDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}