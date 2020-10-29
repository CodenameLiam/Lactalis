
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideImportantDocumentCategoryEntity = Lactalis.Models.ImportantDocumentCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class ImportantDocumentCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<ImportantDocumentEntity> ImportantDocumentss { get; set; }

		public ImportantDocumentCategoryEntityDto(ImportantDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			ImportantDocumentss = model.ImportantDocumentss;
		}

		public ImportantDocumentCategoryEntityDto(ServersideImportantDocumentCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			ImportantDocumentss = model.ImportantDocumentss.Select(ImportantDocumentEntityDto.Convert).ToList();
		}

		public ImportantDocumentCategoryEntity GetTesttargetImportantDocumentCategoryEntity()
		{
			return new ImportantDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				ImportantDocumentss = ImportantDocumentss,
			};
		}

		public ServersideImportantDocumentCategoryEntity GetServersideImportantDocumentCategoryEntity()
		{
			return new ServersideImportantDocumentCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				ImportantDocumentss = ImportantDocumentss?.Select(ImportantDocumentEntityDto.Convert).ToList(),
			};
		}

		public static ServersideImportantDocumentCategoryEntity Convert(ImportantDocumentCategoryEntity model)
		{
			var dto = new ImportantDocumentCategoryEntityDto(model);
			return dto.GetServersideImportantDocumentCategoryEntity();
		}

		public static ImportantDocumentCategoryEntity Convert(ServersideImportantDocumentCategoryEntity model)
		{
			var dto = new ImportantDocumentCategoryEntityDto(model);
			return dto.GetTesttargetImportantDocumentCategoryEntity();
		}
	}
}