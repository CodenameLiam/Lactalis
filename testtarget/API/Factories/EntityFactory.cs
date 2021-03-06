
using System;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using Xunit.Abstractions;

namespace APITests.Factories
{
	public class EntityFactory : IXunitSerializable
	{
		private string _type;
		private readonly string _fixedStrValues;

		public EntityFactory(string type, string fixedStrValues = null)
		{
			_type = type;
			_fixedStrValues = fixedStrValues;
		}

		public EntityFactory()
		{

		}

		public BaseEntity Construct(bool isValid = true)
		{
			return _type switch
			{
				"TradingPostListingEntity" => TradingPostListingEntity.GetEntity(isValid, _fixedStrValues),
				"TradingPostCategoryEntity" => TradingPostCategoryEntity.GetEntity(isValid, _fixedStrValues),
				"AdminEntity" => AdminEntity.GetEntity(isValid, _fixedStrValues),
				"FarmEntity" => FarmEntity.GetEntity(isValid, _fixedStrValues),
				"MilkTestEntity" => MilkTestEntity.GetEntity(isValid, _fixedStrValues),
				"FarmerEntity" => FarmerEntity.GetEntity(isValid, _fixedStrValues),
				"ImportantDocumentCategoryEntity" => ImportantDocumentCategoryEntity.GetEntity(isValid, _fixedStrValues),
				"TechnicalDocumentCategoryEntity" => TechnicalDocumentCategoryEntity.GetEntity(isValid, _fixedStrValues),
				"QualityDocumentCategoryEntity" => QualityDocumentCategoryEntity.GetEntity(isValid, _fixedStrValues),
				"QualityDocumentEntity" => QualityDocumentEntity.GetEntity(isValid, _fixedStrValues),
				"TechnicalDocumentEntity" => TechnicalDocumentEntity.GetEntity(isValid, _fixedStrValues),
				"ImportantDocumentEntity" => ImportantDocumentEntity.GetEntity(isValid, _fixedStrValues),
				"NewsArticleEntity" => NewsArticleEntity.GetEntity(isValid, _fixedStrValues),
				"AgriSupplyDocumentCategoryEntity" => AgriSupplyDocumentCategoryEntity.GetEntity(isValid, _fixedStrValues),
				"SustainabilityPostEntity" => SustainabilityPostEntity.GetEntity(isValid, _fixedStrValues),
				"AgriSupplyDocumentEntity" => AgriSupplyDocumentEntity.GetEntity(isValid, _fixedStrValues),
				"PromotedArticlesEntity" => PromotedArticlesEntity.GetEntity(isValid, _fixedStrValues),
				_ => throw new Exception($"Cannot find entity type {_type}"),
			};
		}

		public List<BaseEntity> Construct(int numEntities)
		{
			var entityList = new List<BaseEntity>(numEntities);
			for (var i = 0; i < numEntities; i++)
			{
				entityList.Add(Construct());
			}
			return entityList;
		}

		public List<BaseEntity> ConstructAndSave(ITestOutputHelper output, int numEntities)
		{
			var entityList = new List<BaseEntity>();
			var options = _fixedStrValues == null ? BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES : BaseEntity.ConfigureOptions.CREATE_REFERENCES_ONLY;

			for(var i = 0; i < numEntities; i++)
			{
				var entity = Construct();
				entity.Configure(options);
				entity.Save();
				output.WriteLine($"Database Saved Entity:\n{entity.EntityName}:\n{entity.ToJson()}\n");
				entityList.Add(entity);
			}
			return entityList;
		}

		public BaseEntity ConstructAndSave(ITestOutputHelper output) => ConstructAndSave(output,1)[0];

		public void Deserialize(IXunitSerializationInfo info) => _type = info.GetValue<string>("type");

		public void Serialize(IXunitSerializationInfo info) => info.AddValue("type", _type, typeof(string));

		public override string ToString() => $"Type = {_type}";

		public string GetFixedString() => _fixedStrValues;

		public string GetEnumValue(BaseEntity entity, string enumColumnName)
		{
			switch (_type)
			{
				case "TradingPostListingEntity":
					switch (enumColumnName)
					{
						case "Price Type":
							return ((TradingPostListingEntity)entity).PriceType.ToString();
						default:
							return null;
					}
				case "FarmEntity":
					switch (enumColumnName)
					{
						case "State":
							return ((FarmEntity)entity).State.ToString();
						default:
							return null;
					}
				default:
					return null;
			}
		}
	}
}