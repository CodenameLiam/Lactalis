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
using ServersideQualityDocumentCategoryEntity = Lactalis.Models.QualityDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class QualityDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<QualityDocumentEntity> QualityDocumentss { get; set; }

		public QualityDocumentCategoryEntityDto(QualityDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			QualityDocumentss = model.QualityDocumentss;
		}

		public QualityDocumentCategoryEntityDto(ServersideQualityDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			QualityDocumentss = model.QualityDocumentss.Select(QualityDocumentEntityDto.Convert).ToList();
		}

		public QualityDocumentCategoryEntity GetTesttargetQualityDocumentCategoryEntity()
		{
			return new QualityDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				QualityDocumentss = QualityDocumentss,
			};
		}

		public ServersideQualityDocumentCategoryEntity GetServersideQualityDocumentCategoryEntity()
		{
			return new ServersideQualityDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				QualityDocumentss = QualityDocumentss?.Select(QualityDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideQualityDocumentCategoryEntity Convert(QualityDocumentCategoryEntity model)
		{
			var dto = new QualityDocumentCategoryEntityDto(model);
			return dto.GetServersideQualityDocumentCategoryEntity();
		}

		public static QualityDocumentCategoryEntity Convert(ServersideQualityDocumentCategoryEntity model)
		{
			var dto = new QualityDocumentCategoryEntityDto(model);
			return dto.GetTesttargetQualityDocumentCategoryEntity();
		}
	}
}