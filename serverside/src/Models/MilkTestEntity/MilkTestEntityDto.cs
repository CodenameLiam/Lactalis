
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class MilkTestEntityDto : ModelDto<MilkTestEntity>
	{
		public DateTime? Time { get; set; }
		public int? Volume { get; set; }
		public Double? Temperature { get; set; }
		public Double? MilkFat { get; set; }
		public Double? Protein { get; set; }

		public Guid? FarmId { get; set; }


		public MilkTestEntityDto(MilkTestEntity model)
		{
			LoadModelData(model);
		}

		public MilkTestEntityDto()
		{
		}

		public override MilkTestEntity ToModel()
		{

			return new MilkTestEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Time = Time,
				Volume = Volume,
				Temperature = Temperature,
				MilkFat = MilkFat,
				Protein = Protein,
				FarmId  = FarmId,
			};
		}

		public override ModelDto<MilkTestEntity> LoadModelData(MilkTestEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Time = model.Time;
			Volume = model.Volume;
			Temperature = model.Temperature;
			MilkFat = model.MilkFat;
			Protein = model.Protein;
			FarmId  = model.FarmId;


			return this;
		}
	}
}