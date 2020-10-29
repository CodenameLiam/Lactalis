
using System;
using System.Linq;
using System.Collections.Generic;
 

namespace Lactalis.Models
{
	public class AgriSupplyDocumentEntityDto : ModelDto<AgriSupplyDocumentEntity>
	{
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? AgriSupplyDocumentCategoryId { get; set; }


		public AgriSupplyDocumentEntityDto(AgriSupplyDocumentEntity model)
		{
			LoadModelData(model);
		}

		public AgriSupplyDocumentEntityDto()
		{
		}

		public override AgriSupplyDocumentEntity ToModel()
		{

			return new AgriSupplyDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				AgriSupplyDocumentCategoryId  = AgriSupplyDocumentCategoryId,
			};
		}

		public override ModelDto<AgriSupplyDocumentEntity> LoadModelData(AgriSupplyDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			AgriSupplyDocumentCategoryId  = model.AgriSupplyDocumentCategoryId;


			return this;
		}
	}
}