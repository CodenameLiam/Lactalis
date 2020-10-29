
using System;
using System.Linq;
using System.Collections.Generic;
using Lactalis.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
 

namespace Lactalis.Models
{
	public class FarmEntityDto : ModelDto<FarmEntity>
	{
		public String Code { get; set; }
		public String Name { get; set; }
		[JsonProperty("state")]
		[JsonConverter(typeof(StringEnumConverter))]
		public State State { get; set; }


		public FarmEntityDto(FarmEntity model)
		{
			LoadModelData(model);
		}

		public FarmEntityDto()
		{
		}

		public override FarmEntity ToModel()
		{

			return new FarmEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Code = Code,
				Name = Name,
				State = State,
			};
		}

		public override ModelDto<FarmEntity> LoadModelData(FarmEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Code = model.Code;
			Name = model.Name;
			State = model.State;


			return this;
		}
	}
}