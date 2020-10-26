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
using ServersideAgriSupplyDocumentCategoryEntity = Lactalis.Models.AgriSupplyDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class AgriSupplyDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<AgriSupplyDocumentEntity> AgriSupplyDocumentss { get; set; }

		public AgriSupplyDocumentCategoryEntityDto(AgriSupplyDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			AgriSupplyDocumentss = model.AgriSupplyDocumentss;
		}

		public AgriSupplyDocumentCategoryEntityDto(ServersideAgriSupplyDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			AgriSupplyDocumentss = model.AgriSupplyDocumentss.Select(AgriSupplyDocumentEntityDto.Convert).ToList();
		}

		public AgriSupplyDocumentCategoryEntity GetTesttargetAgriSupplyDocumentCategoryEntity()
		{
			return new AgriSupplyDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				AgriSupplyDocumentss = AgriSupplyDocumentss,
			};
		}

		public ServersideAgriSupplyDocumentCategoryEntity GetServersideAgriSupplyDocumentCategoryEntity()
		{
			return new ServersideAgriSupplyDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				AgriSupplyDocumentss = AgriSupplyDocumentss?.Select(AgriSupplyDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideAgriSupplyDocumentCategoryEntity Convert(AgriSupplyDocumentCategoryEntity model)
		{
			var dto = new AgriSupplyDocumentCategoryEntityDto(model);
			return dto.GetServersideAgriSupplyDocumentCategoryEntity();
		}

		public static AgriSupplyDocumentCategoryEntity Convert(ServersideAgriSupplyDocumentCategoryEntity model)
		{
			var dto = new AgriSupplyDocumentCategoryEntityDto(model);
			return dto.GetTesttargetAgriSupplyDocumentCategoryEntity();
		}
	}
}