
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideImportantDocumentEntity = Lactalis.Models.ImportantDocumentEntity;

namespace APITests.EntityObjects.Models
{
	public class ImportantDocumentEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public Guid? FileId { get; set; }
		public String Name { get; set; }
		public Boolean? Qld { get; set; }
		public Boolean? Nsw { get; set; }
		public Boolean? Vic { get; set; }
		public Boolean? Tas { get; set; }
		public Boolean? Wa { get; set; }
		public Boolean? Sa { get; set; }
		public Boolean? Nt { get; set; }

		public Guid? DocumentCategoryId { get; set; }

		public ImportantDocumentEntityDto(ImportantDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			Qld = model.Qld;
			Nsw = model.Nsw;
			Vic = model.Vic;
			Tas = model.Tas;
			Wa = model.Wa;
			Sa = model.Sa;
			Nt = model.Nt;
			DocumentCategoryId = model.DocumentCategoryId;
		}

		public ImportantDocumentEntityDto(ServersideImportantDocumentEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			FileId = model.FileId;
			Name = model.Name;
			Qld = model.Qld;
			Nsw = model.Nsw;
			Vic = model.Vic;
			Tas = model.Tas;
			Wa = model.Wa;
			Sa = model.Sa;
			Nt = model.Nt;
			DocumentCategoryId = model.DocumentCategoryId;
		}

		public ImportantDocumentEntity GetTesttargetImportantDocumentEntity()
		{
			return new ImportantDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				Qld = Qld,
				Nsw = Nsw,
				Vic = Vic,
				Tas = Tas,
				Wa = Wa,
				Sa = Sa,
				Nt = Nt,
				DocumentCategoryId = DocumentCategoryId,
			};
		}

		public ServersideImportantDocumentEntity GetServersideImportantDocumentEntity()
		{
			return new ServersideImportantDocumentEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				FileId = FileId,
				Name = Name,
				Qld = Qld,
				Nsw = Nsw,
				Vic = Vic,
				Tas = Tas,
				Wa = Wa,
				Sa = Sa,
				Nt = Nt,
				DocumentCategoryId = DocumentCategoryId,
			};
		}

		public static ServersideImportantDocumentEntity Convert(ImportantDocumentEntity model)
		{
			var dto = new ImportantDocumentEntityDto(model);
			return dto.GetServersideImportantDocumentEntity();
		}

		public static ImportantDocumentEntity Convert(ServersideImportantDocumentEntity model)
		{
			var dto = new ImportantDocumentEntityDto(model);
			return dto.GetTesttargetImportantDocumentEntity();
		}
	}
}