
using System;
using System.Linq;
using System.Collections.Generic;
 

namespace Lactalis.Models
{
	public class FarmerEntityDto : ModelDto<FarmerEntity>
	{
		public string Email { get; set; }


		public FarmerEntityDto(FarmerEntity model)
		{
			LoadModelData(model);
		}

		public FarmerEntityDto()
		{
		}

		public override FarmerEntity ToModel()
		{

			return new FarmerEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Email = Email,
			};
		}

		public override ModelDto<FarmerEntity> LoadModelData(FarmerEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Email = model.Email;


			return this;
		}
	}
}