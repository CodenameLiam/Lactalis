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
using ServersideTechnicalDocumentCategoryEntity = Lactalis.Models.TechnicalDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class TechnicalDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<TechnicalDocumentEntity> TechnicalDocumentss { get; set; }

		public TechnicalDocumentCategoryEntityDto(TechnicalDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TechnicalDocumentss = model.TechnicalDocumentss;
		}

		public TechnicalDocumentCategoryEntityDto(ServersideTechnicalDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TechnicalDocumentss = model.TechnicalDocumentss.Select(TechnicalDocumentEntityDto.Convert).ToList();
		}

		public TechnicalDocumentCategoryEntity GetTesttargetTechnicalDocumentCategoryEntity()
		{
			return new TechnicalDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TechnicalDocumentss = TechnicalDocumentss,
			};
		}

		public ServersideTechnicalDocumentCategoryEntity GetServersideTechnicalDocumentCategoryEntity()
		{
			return new ServersideTechnicalDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TechnicalDocumentss = TechnicalDocumentss?.Select(TechnicalDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideTechnicalDocumentCategoryEntity Convert(TechnicalDocumentCategoryEntity model)
		{
			var dto = new TechnicalDocumentCategoryEntityDto(model);
			return dto.GetServersideTechnicalDocumentCategoryEntity();
		}

		public static TechnicalDocumentCategoryEntity Convert(ServersideTechnicalDocumentCategoryEntity model)
		{
			var dto = new TechnicalDocumentCategoryEntityDto(model);
			return dto.GetTesttargetTechnicalDocumentCategoryEntity();
		}
	}
}