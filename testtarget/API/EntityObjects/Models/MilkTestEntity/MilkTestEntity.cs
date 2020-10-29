
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
	public class MilkTestEntity : BaseEntity
	{
		// 
		public DateTime? Time { get; set; }
		// 
		public int? Volume { get; set; }
		// 
		public Double? Temperature { get; set; }
		// 
		public Double? MilkFat { get; set; }
		// 
		public Double? Protein { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.Farm"/>
		public Guid? FarmId { get; set; }


		public MilkTestEntity()
		{
			EntityName = "MilkTestEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public MilkTestEntity(ConfigureOptions option)
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
				Name = "Time",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Volume",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Temperature",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "MilkFat",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Protein",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "FarmEntity",
				OppositeName = "Farm",
				Name = "Pickups",
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


		private static string GetInvalidFarmId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Pickups");
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
				{"time" ,((DateTime)Time).ToIsoString()},
				{"volume" , Volume.ToString()},
				{"temperature" , Temperature.ToString()},
				{"milkFat" , MilkFat.ToString()},
				{"protein" , Protein.ToString()},
			};

			if (FarmId != default)
			{
				entityVar["farmId"] = FarmId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["time"] = Time?.ToString("s"),
				["volume"] = Volume,
				["temperature"] = Temperature.ToString(),
				["milkFat"] = MilkFat.ToString(),
				["protein"] = Protein.ToString(),
			};


			return entityVar;
		}


		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "FarmId":
						ReferenceIdDictionary.Add("FarmId", guidCollection.FirstOrDefault());
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
				case "FarmId":
					FarmId = guid;
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
		public static MilkTestEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static MilkTestEntity GetInvalidEntity()
		{
			var milkTestEntity = new MilkTestEntity
			{
			};
			return milkTestEntity;
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
			Time = DataUtils.RandDatetime();
			Volume = DataUtils.RandInt();
			Temperature = DataUtils.RandDouble();
			MilkFat = DataUtils.RandDouble();
			Protein = DataUtils.RandDouble();
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static MilkTestEntity GetValidEntity(string fixedStrValue = null)
		{
			var milkTestEntity = new MilkTestEntity
			{

				Time = DataUtils.RandDatetime(),

				Volume = DataUtils.RandInt(),

				Temperature = DataUtils.RandDouble(),

				MilkFat = DataUtils.RandDouble(),

				Protein = DataUtils.RandDouble(),
			};


			return milkTestEntity;
		}

		public override Guid Save()
		{
			return SaveToDB<Lactalis.Models.MilkTestEntity>(MilkTestEntityDto.Convert(this));
		}
	}
}
