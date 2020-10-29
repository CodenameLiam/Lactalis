
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityObject.Enums;
using APITests.Classes;
using RestSharp;
using TestDataLib;
using Lactalis.Utility;

namespace APITests.EntityObjects.Models
{
	public class ImportantDocumentEntity : BaseEntity, IFileContainingEntity 
	{
		public override bool HasFile { get; set; } = true;

		// 
		public Guid? FileId { get; set; }
		public FileData File { get; set; }
		// 
		public String Name { get; set; }
		// 
		public Boolean? Qld { get; set; }
		// 
		public Boolean? Nsw { get; set; }
		// 
		public Boolean? Vic { get; set; }
		// 
		public Boolean? Tas { get; set; }
		// 
		public Boolean? Wa { get; set; }
		// 
		public Boolean? Sa { get; set; }
		// 
		public Boolean? Nt { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.DocumentCategory"/>
		public Guid? DocumentCategoryId { get; set; }


		public ImportantDocumentEntity()
		{
			EntityName = "ImportantDocumentEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public ImportantDocumentEntity(ConfigureOptions option)
		{
			Configure(option);
			InitialiseAttributes();
			InitialiseReferences();
		}

		public override void Configure(ConfigureOptions option)
		{
			switch (option)
			{
				case ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES:
					SetValidEntityAttributes();
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_ATTRIBUTES_ONLY:
					SetValidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_REFERENCES_ONLY:
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES:
					SetInvalidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES:
					SetInvalidEntityAttributes();
					SetValidEntityAssociations();
					break;
			}
		}

		private void InitialiseAttributes()
		{
			Attributes.Add(new Attribute
			{
				Name = "FileId",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Name",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Qld",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Nsw",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Vic",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Tas",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Wa",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Sa",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Nt",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "ImportantDocumentCategoryEntity",
				OppositeName = "DocumentCategory",
				Name = "ImportantDocuments",
				Optional = true,
				Type = ReferenceType.ONE,
				OppositeType = ReferenceType.MANY
			});
		}

		public override (int min, int max) GetLengthValidatorMinMax(string attribute)
		{
			switch(attribute)
			{
				default:
					throw new Exception($"{attribute} does not exist or does not have a length validator");
			}
		}

		public override string GetInvalidAttribute(string attribute, string validator)
		{
			switch (attribute)
			{
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}


		private static string GetInvalidDocumentCategoryId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Important Documents");
			}
		}

		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, RestSharp.JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return new List<(List<string> expectedError, RestSharp.JsonObject jsonObject)>
			{


			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"fileId" , FileId.ToString()},
				{"name" , Name},
				{"qld" , Qld.ToString()},
				{"nsw" , Nsw.ToString()},
				{"vic" , Vic.ToString()},
				{"tas" , Tas.ToString()},
				{"wa" , Wa.ToString()},
				{"sa" , Sa.ToString()},
				{"nt" , Nt.ToString()},
			};

			if (DocumentCategoryId != default)
			{
				entityVar["documentCategoryId"] = DocumentCategoryId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["fileId"] = FileId.ToString(),
				["name"] = Name.ToString(),
				["qld"] = Qld.ToString(),
				["nsw"] = Nsw.ToString(),
				["vic"] = Vic.ToString(),
				["tas"] = Tas.ToString(),
				["wa"] = Wa.ToString(),
				["sa"] = Sa.ToString(),
				["nt"] = Nt.ToString(),
			};


			return entityVar;
		}

		public IEnumerable<FileData> GetFiles()
		{
			return new List<FileData>
			{
				File,
			};
		}

		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "DocumentCategoryId":
						ReferenceIdDictionary.Add("DocumentCategoryId", guidCollection.FirstOrDefault());
						SetOneReference(key, guidCollection.FirstOrDefault());
						break;
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
				case "DocumentCategoryId":
					DocumentCategoryId = guid;
					break;
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
				default:
					throw new Exception($"{reference} not valid many to many reference key");
			}
		}

		private List<RestSharp.JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<RestSharp.JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new RestSharp.JsonObject {[key] = x }));
			return manyToManyList;
		}

		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static ImportantDocumentEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static ImportantDocumentEntity GetInvalidEntity()
		{
			var importantDocumentEntity = new ImportantDocumentEntity
			{
			};
			return importantDocumentEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
				File = new FileData
				{
					Id = Guid.NewGuid(),
					Data = DataUtils.GetSVGTestFile(),
					Filename = "testfile.svg"
				};
				FileId = File.Id;
			Name = DataUtils.RandString();
			Qld = DataUtils.RandBool();
			Nsw = DataUtils.RandBool();
			Vic = DataUtils.RandBool();
			Tas = DataUtils.RandBool();
			Wa = DataUtils.RandBool();
			Sa = DataUtils.RandBool();
			Nt = DataUtils.RandBool();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static ImportantDocumentEntity GetValidEntity(string fixedStrValue = null)
		{
			var importantDocumentEntity = new ImportantDocumentEntity
			{

				File = new FileData
				{
					Id = Guid.NewGuid(),
					Data = DataUtils.GetSVGTestFile(),
					Filename = "testfile.svg"
				},

				Name = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Qld = DataUtils.RandBool(),

				Nsw = DataUtils.RandBool(),

				Vic = DataUtils.RandBool(),

				Tas = DataUtils.RandBool(),

				Wa = DataUtils.RandBool(),

				Sa = DataUtils.RandBool(),

				Nt = DataUtils.RandBool(),
			};

			importantDocumentEntity.FileId = importantDocumentEntity.File.Id;


			return importantDocumentEntity;
		}

		public override Guid Save()
		{
			return SaveThroughGraphQl(this);
		}
	}
}
