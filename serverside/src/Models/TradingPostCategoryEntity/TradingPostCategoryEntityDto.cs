
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class TradingPostCategoryEntityDto : ModelDto<TradingPostCategoryEntity>
	{
		public String Name { get; set; }


		public TradingPostCategoryEntityDto(TradingPostCategoryEntity model)
		{
			LoadModelData(model);
		}

		public TradingPostCategoryEntityDto()
		{
		}

		public override TradingPostCategoryEntity ToModel()
		{

			return new TradingPostCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<TradingPostCategoryEntity> LoadModelData(TradingPostCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}