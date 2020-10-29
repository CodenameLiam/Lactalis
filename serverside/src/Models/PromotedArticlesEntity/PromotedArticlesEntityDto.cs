
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class PromotedArticlesEntityDto : ModelDto<PromotedArticlesEntity>
	{
		public String Name { get; set; }


		public PromotedArticlesEntityDto(PromotedArticlesEntity model)
		{
			LoadModelData(model);
		}

		public PromotedArticlesEntityDto()
		{
		}

		public override PromotedArticlesEntity ToModel()
		{

			return new PromotedArticlesEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
			};
		}

		public override ModelDto<PromotedArticlesEntity> LoadModelData(PromotedArticlesEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;


			return this;
		}
	}
}