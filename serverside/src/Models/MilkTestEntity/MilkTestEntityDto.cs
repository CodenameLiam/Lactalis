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
using System.Linq;
using System.Collections.Generic;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

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

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public MilkTestEntityDto(MilkTestEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public MilkTestEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override MilkTestEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

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
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
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

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}