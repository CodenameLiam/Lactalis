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
using ServersideSustainabilityPostEntity = Lactalis.Models.SustainabilityPostEntity;

namespace APITests.EntityObjects.Models
{
	public class SustainabilityPostEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Title { get; set; }
		public Guid? ImageId { get; set; }
		public Guid? FileId { get; set; }
		public String Content { get; set; }


		public SustainabilityPostEntityDto(SustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;
		}

		public SustainabilityPostEntityDto(ServersideSustainabilityPostEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			ImageId = model.ImageId;
			FileId = model.FileId;
			Content = model.Content;
		}

		public SustainabilityPostEntity GetTesttargetSustainabilityPostEntity()
		{
			return new SustainabilityPostEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				ImageId = ImageId,
				FileId = FileId,
				Content = Content,
			};
		}

		public ServersideSustainabilityPostEntity GetServersideSustainabilityPostEntity()
		{
			return new ServersideSustainabilityPostEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				ImageId = ImageId,
				FileId = FileId,
				Content = Content,
			};
		}

		public static ServersideSustainabilityPostEntity Convert(SustainabilityPostEntity model)
		{
			var dto = new SustainabilityPostEntityDto(model);
			return dto.GetServersideSustainabilityPostEntity();
		}

		public static SustainabilityPostEntity Convert(ServersideSustainabilityPostEntity model)
		{
			var dto = new SustainabilityPostEntityDto(model);
			return dto.GetTesttargetSustainabilityPostEntity();
		}
	}
}