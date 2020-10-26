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
	public class SustainabilityPostEntityDto : ModelDto<SustainabilityPostEntity>
	{
		public String Title { get; set; }
		public Guid? ImageId { get; set; }
		public Guid? FileId { get; set; }
		public String Content { get; set; }

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public SustainabilityPostEntityDto(SustainabilityPostEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public SustainabilityPostEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override SustainabilityPostEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

			return new SustainabilityPostEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				ImageId = ImageId,
				FileId = FileId,
				Content = Content,
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
			};
		}

		public override ModelDto<SustainabilityPostEntity> LoadModelData(SustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}