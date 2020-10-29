
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideMilkTestEntity = Lactalis.Models.MilkTestEntity;

namespace APITests.EntityObjects.Models
{
	public class MilkTestEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public DateTime? Time { get; set; }
		public int? Volume { get; set; }
		public Double? Temperature { get; set; }
		public Double? MilkFat { get; set; }
		public Double? Protein { get; set; }

		public Guid? FarmId { get; set; }

		public MilkTestEntityDto(MilkTestEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Time = model.Time;
			Volume = model.Volume;
			Temperature = model.Temperature;
			MilkFat = model.MilkFat;
			Protein = model.Protein;
			FarmId = model.FarmId;
		}

		public MilkTestEntityDto(ServersideMilkTestEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Time = model.Time;
			Volume = model.Volume;
			Temperature = model.Temperature;
			MilkFat = model.MilkFat;
			Protein = model.Protein;
			FarmId = model.FarmId;
		}

		public MilkTestEntity GetTesttargetMilkTestEntity()
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
				FarmId = FarmId,
			};
		}

		public ServersideMilkTestEntity GetServersideMilkTestEntity()
		{
			return new ServersideMilkTestEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Time = Time,
				Volume = Volume,
				Temperature = Temperature,
				MilkFat = MilkFat,
				Protein = Protein,
				FarmId = FarmId,
			};
		}

		public static ServersideMilkTestEntity Convert(MilkTestEntity model)
		{
			var dto = new MilkTestEntityDto(model);
			return dto.GetServersideMilkTestEntity();
		}

		public static MilkTestEntity Convert(ServersideMilkTestEntity model)
		{
			var dto = new MilkTestEntityDto(model);
			return dto.GetTesttargetMilkTestEntity();
		}
	}
}