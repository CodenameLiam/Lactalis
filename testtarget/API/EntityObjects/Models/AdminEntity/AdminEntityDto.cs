
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideAdminEntity = Lactalis.Models.AdminEntity;

namespace APITests.EntityObjects.Models
{
	public class AdminEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }


		public AdminEntityDto(AdminEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public AdminEntityDto(ServersideAdminEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
		}

		public AdminEntity GetTesttargetAdminEntity()
		{
			return new AdminEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public ServersideAdminEntity GetServersideAdminEntity()
		{
			return new ServersideAdminEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
			};
		}

		public static ServersideAdminEntity Convert(AdminEntity model)
		{
			var dto = new AdminEntityDto(model);
			return dto.GetServersideAdminEntity();
		}

		public static AdminEntity Convert(ServersideAdminEntity model)
		{
			var dto = new AdminEntityDto(model);
			return dto.GetTesttargetAdminEntity();
		}
	}
}