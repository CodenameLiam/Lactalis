
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class ImportantDocumentEntityDto : ModelDto<ImportantDocumentEntity>
	{
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
			LoadModelData(model);
		}

		public ImportantDocumentEntityDto()
		{
		}

		public override ImportantDocumentEntity ToModel()
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
				DocumentCategoryId  = DocumentCategoryId,
			};
		}

		public override ModelDto<ImportantDocumentEntity> LoadModelData(ImportantDocumentEntity model)
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
			DocumentCategoryId  = model.DocumentCategoryId;


			return this;
		}
	}
}