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
using System.Text;
using EntityObject.Enums;
using APITests.Classes;
using RestSharp;
using TestDataLib;
using Lactalis.Utility;

namespace APITests.EntityObjects.Models
{
	public class TradingPostListingEntity : BaseEntity, IFileContainingEntity 
	{
		public override bool HasFile { get; set; } = true;

		// 
		public String Title { get; set; }
		// 
		public String Email { get; set; }
		// 
		public String Phone { get; set; }
		// 
		public String AdditionalInfo { get; set; }
		// 
		public String AddressLine1 { get; set; }
		// 
		public String AddressLine2 { get; set; }
		// 
		public String PostalCode { get; set; }
		// 
		public Guid? ProductImageId { get; set; }
		public FileData ProductImage { get; set; }
		// 
		public int? Price { get; set; }
		// 
		public PriceType PriceType { get; set; }

		/// <summary>
		/// Incoming one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.Farmer"/>
		public Guid? FarmerId { get; set; }

		/// <summary>
		/// Outgoing many to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostCategories"/>
		public List<Guid> TradingPostCategoriesIds { get; set; }
		public ICollection<TradingPostListingsTradingPostCategories> TradingPostCategoriess { get; set; }


		public TradingPostListingEntity()
		{
			EntityName = "TradingPostListingEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public TradingPostListingEntity(ConfigureOptions option)
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
				Name = "Title",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Email",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Phone",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "AdditionalInfo",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "AddressLine1",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "AddressLine2",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "PostalCode",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "ProductImageId",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "Price",
				IsRequired = false
			});
			Attributes.Add(new Attribute
			{
				Name = "PriceType",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
			References.Add(new Reference
			{
				EntityName = "FarmerEntity",
				OppositeName = "Farmer",
				Name = "TradingPostListings",
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


		private static string GetInvalidFarmerId(string validator)
		{
			switch (validator)
			{
				case "Required":
					return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Trading Post Listings");
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
				{"title" , Title},
				{"email" , Email},
				{"phone" , Phone},
				{"additionalInfo" , AdditionalInfo},
				{"addressLine1" , AddressLine1},
				{"addressLine2" , AddressLine2},
				{"postalCode" , PostalCode},
				{"productImageId" , ProductImageId.ToString()},
				{"price" , Price.ToString()},
				{"priceType" , PriceType.ToString()},
			};

			if (FarmerId != default)
			{
				entityVar["farmerId"] = FarmerId.ToString();
			}

			return entityVar;
		}

		public override RestSharp.JsonObject ToJson()
		{
			var entityVar = new RestSharp.JsonObject
			{
				["id"] = Id,
				["title"] = Title.ToString(),
				["email"] = Email.ToString(),
				["phone"] = Phone.ToString(),
				["additionalInfo"] = AdditionalInfo.ToString(),
				["addressLine1"] = AddressLine1.ToString(),
				["addressLine2"] = AddressLine2.ToString(),
				["postalCode"] = PostalCode.ToString(),
				["productImageId"] = ProductImageId.ToString(),
				["price"] = Price,
				["priceType"] = PriceType.ToString(),
			};


			return entityVar;
		}

		public IEnumerable<FileData> GetFiles()
		{
			return new List<FileData>
			{
				ProductImage,
			};
		}

		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					case "FarmerId":
						ReferenceIdDictionary.Add("FarmerId", guidCollection.FirstOrDefault());
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
				case "FarmerId":
					FarmerId = guid;
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
			PriceType = PriceTypeEnum.GetRandomPriceType();
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static TradingPostListingEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static TradingPostListingEntity GetInvalidEntity()
		{
			var tradingPostListingEntity = new TradingPostListingEntity
			{
				PriceType = PriceTypeEnum.GetRandomPriceType(),
			};
			return tradingPostListingEntity;
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
			// % protected region % [Override generated entity attributes here] off begin
			Title = DataUtils.RandString();
			Email = DataUtils.RandString();
			Phone = DataUtils.RandString();
			AdditionalInfo = DataUtils.RandString();
			AddressLine1 = DataUtils.RandString();
			AddressLine2 = DataUtils.RandString();
			PostalCode = DataUtils.RandString();
				ProductImage = new FileData
				{
					Id = Guid.NewGuid(),
					Data = DataUtils.GetSVGTestFile(),
					Filename = "testfile.svg"
				};
				ProductImageId = ProductImage.Id;
			Price = DataUtils.RandInt();
			PriceType = PriceTypeEnum.GetRandomPriceType();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static TradingPostListingEntity GetValidEntity(string fixedStrValue = null)
		{
			var tradingPostListingEntity = new TradingPostListingEntity
			{

				Title = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Email = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				Phone = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				AdditionalInfo = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				AddressLine1 = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				AddressLine2 = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				PostalCode = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),

				ProductImage = new FileData
				{
					Id = Guid.NewGuid(),
					Data = DataUtils.GetSVGTestFile(),
					Filename = "testfile.svg"
				},

				Price = DataUtils.RandInt(),

				PriceType = PriceTypeEnum.GetRandomPriceType(),
			};

			tradingPostListingEntity.ProductImageId = tradingPostListingEntity.ProductImage.Id;

			// % protected region % [Customize valid entity before return here] off begin
			// % protected region % [Customize valid entity before return here] end

			return tradingPostListingEntity;
		}

		public override Guid Save()
		{
			return SaveThroughGraphQl(this);
		}
	}
}
