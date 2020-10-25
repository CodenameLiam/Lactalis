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
using ServersideFarmerEntity = Lactalis.Models.FarmerEntity;

namespace APITests.EntityObjects.Models
{
	public class FarmerEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		public ICollection<FarmersFarms> Farmss { get; set; }

		public FarmerEntityDto(FarmerEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Farmss = model.Farmss;
		}

		public FarmerEntityDto(ServersideFarmerEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Farmss  = model.Farmss == null ? null :FarmersFarmsDto.Convert(model.Farmss);
		}

		public FarmerEntity GetTesttargetFarmerEntity()
		{
			return new FarmerEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Farmss = Farmss,
			};
		}

		public ServersideFarmerEntity GetServersideFarmerEntity()
		{
			return new ServersideFarmerEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Farmss = Farmss == null ? null :FarmersFarmsDto.Convert(Farmss),
			};
		}

		public static ServersideFarmerEntity Convert(FarmerEntity model)
		{
			var dto = new FarmerEntityDto(model);
			return dto.GetServersideFarmerEntity();
		}

		public static FarmerEntity Convert(ServersideFarmerEntity model)
		{
			var dto = new FarmerEntityDto(model);
			return dto.GetTesttargetFarmerEntity();
		}
	}
}