
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Enums;
using TestEnums = EntityObject.Enums;
using ServersideFarmEntity = Lactalis.Models.FarmEntity;

namespace APITests.EntityObjects.Models
{
	public class FarmEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Code { get; set; }
		public String Name { get; set; }
		public State State { get; set; }

		public ICollection<MilkTestEntity> Pickupss { get; set; }
		public ICollection<FarmersFarms> Farmerss { get; set; }

		public FarmEntityDto(FarmEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Code = model.Code;
			Name = model.Name;
			State = (State)model.State;
			Pickupss = model.Pickupss;
			Farmerss = model.Farmerss;
		}

		public FarmEntityDto(ServersideFarmEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Code = model.Code;
			Name = model.Name;
			State = model.State;
			Pickupss = model.Pickupss.Select(MilkTestEntityDto.Convert).ToList();
			Farmerss  = model.Farmerss == null ? null :FarmersFarmsDto.Convert(model.Farmerss);
		}

		public FarmEntity GetTesttargetFarmEntity()
		{
			return new FarmEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Code = Code,
				Name = Name,
				State = (TestEnums.State)State,
				Pickupss = Pickupss,
				Farmerss = Farmerss,
			};
		}

		public ServersideFarmEntity GetServersideFarmEntity()
		{
			return new ServersideFarmEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Code = Code,
				Name = Name,
				State = State,
				Pickupss = Pickupss?.Select(MilkTestEntityDto.Convert).ToList(),
				Farmerss = Farmerss == null ? null :FarmersFarmsDto.Convert(Farmerss),
			};
		}

		public static ServersideFarmEntity Convert(FarmEntity model)
		{
			var dto = new FarmEntityDto(model);
			return dto.GetServersideFarmEntity();
		}

		public static FarmEntity Convert(ServersideFarmEntity model)
		{
			var dto = new FarmEntityDto(model);
			return dto.GetTesttargetFarmEntity();
		}
	}
}