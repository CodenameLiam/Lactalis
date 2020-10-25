/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
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
		public String Name { get; set; }
		public State State { get; set; }

		public ICollection<MilkTestEntity> Pickupss { get; set; }
		public ICollection<FarmersFarms> Farmerss { get; set; }

		public FarmEntityDto(FarmEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
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