
using System;
using System.Linq;
using System.Collections.Generic;
 

namespace Lactalis.Models
{
	public class AdminEntityDto : ModelDto<AdminEntity>
	{
		public string Email { get; set; }


		public AdminEntityDto(AdminEntity model)
		{
			LoadModelData(model);
		}

		public AdminEntityDto()
		{
		}

		public override AdminEntity ToModel()
		{

			return new AdminEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Email = Email,
			};
		}

		public override ModelDto<AdminEntity> LoadModelData(AdminEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Email = model.Email;


			return this;
		}
	}
}