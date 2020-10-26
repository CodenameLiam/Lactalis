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
using ServersideQualityDocumentEntity = Lactalis.Models.QualityDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class QualityDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }

		public Guid? QualityDocumentCategoryId { get; set; }

		public QualityDocumentEntityDto(QualityDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			QualityDocumentCategoryId = model.QualityDocumentCategoryId;
		}

		public QualityDocumentEntityDto(ServersideQualityDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			QualityDocumentCategoryId = model.QualityDocumentCategoryId;
		}

		public QualityDocumentEntity GetTesttargetQualityDocumentEntity()
		{
			return new QualityDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				QualityDocumentCategoryId = QualityDocumentCategoryId,
			};
		}

		public ServersideQualityDocumentEntity GetServersideQualityDocumentEntity()
		{
			return new ServersideQualityDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				QualityDocumentCategoryId = QualityDocumentCategoryId,
			};
		}

		public static ServersideQualityDocumentEntity Convert(QualityDocumentEntity model)
		{
			var dto = new QualityDocumentEntityDto(model);
			return dto.GetServersideQualityDocumentEntity();
		}

		public static QualityDocumentEntity Convert(ServersideQualityDocumentEntity model)
		{
			var dto = new QualityDocumentEntityDto(model);
			return dto.GetTesttargetQualityDocumentEntity();
		}
	}
}