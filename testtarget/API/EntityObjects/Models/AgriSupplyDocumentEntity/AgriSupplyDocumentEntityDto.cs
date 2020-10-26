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
using ServersideAgriSupplyDocumentEntity = Lactalis.Models.AgriSupplyDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class AgriSupplyDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? AgriSupplyDocumentCategoryId { get; set; }

		public AgriSupplyDocumentEntityDto(AgriSupplyDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			AgriSupplyDocumentCategoryId = model.AgriSupplyDocumentCategoryId;
		}

		public AgriSupplyDocumentEntityDto(ServersideAgriSupplyDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			AgriSupplyDocumentCategoryId = model.AgriSupplyDocumentCategoryId;
		}

		public AgriSupplyDocumentEntity GetTesttargetAgriSupplyDocumentEntity()
		{
			return new AgriSupplyDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				AgriSupplyDocumentCategoryId = AgriSupplyDocumentCategoryId,
			};
		}

		public ServersideAgriSupplyDocumentEntity GetServersideAgriSupplyDocumentEntity()
		{
			return new ServersideAgriSupplyDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				AgriSupplyDocumentCategoryId = AgriSupplyDocumentCategoryId,
			};
		}

		public static ServersideAgriSupplyDocumentEntity Convert(AgriSupplyDocumentEntity model)
		{
			var dto = new AgriSupplyDocumentEntityDto(model);
			return dto.GetServersideAgriSupplyDocumentEntity();
		}

		public static AgriSupplyDocumentEntity Convert(ServersideAgriSupplyDocumentEntity model)
		{
			var dto = new AgriSupplyDocumentEntityDto(model);
			return dto.GetTesttargetAgriSupplyDocumentEntity();
		}
	}
}