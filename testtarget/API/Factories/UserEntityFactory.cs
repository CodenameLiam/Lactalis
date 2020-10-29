
using System;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using Xunit.Abstractions;

namespace APITests.Factories
{
	public class UserEntityFactory : IXunitSerializable
	{
		private string _type;
		private readonly string _fixedStrValues;

		public UserEntityFactory(string type, string fixedStrValues = null)
		{
			_type = type;
			_fixedStrValues = fixedStrValues;
		}

		public UserEntityFactory()
		{

		}

		public UserBaseEntity Construct(bool isValid = true)
		{
			switch (_type)
			{
				case "AdminEntity":
					return AdminEntity.GetEntity(isValid, _fixedStrValues);
				case "FarmerEntity":
					return FarmerEntity.GetEntity(isValid, _fixedStrValues);
				default:
					throw new Exception($"Cannot find entity type {_type}");
			}
		}

		public List<UserBaseEntity> Construct(int numEntities)
		{
			var entityList = new List<UserBaseEntity>(numEntities);
			for (var i = 0; i < numEntities; i++)
			{
				entityList.Add(Construct());
			}
			return entityList;
		}

		public List<UserBaseEntity> ConstructAndSave(ITestOutputHelper output, int numEntities)
		{
			var entityList = new List<UserBaseEntity>();
			var options = _fixedStrValues == null ? BaseEntity.ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES : BaseEntity.ConfigureOptions.CREATE_REFERENCES_ONLY;

			for (var i = 0; i < numEntities; i++)
			{
				var entity = Construct();
				entity.Configure(options);
				entity.Save();
				output.WriteLine($"Database Saved Entity:\n{entity.EntityName}:\n{entity.ToJson()}\n");
				entityList.Add(entity);
			}
			return entityList;
		}

		public UserBaseEntity ConstructAndSave(ITestOutputHelper output) => ConstructAndSave(output, 1)[0];

		public void Deserialize(IXunitSerializationInfo info) => _type = info.GetValue<string>("type");

		public void Serialize(IXunitSerializationInfo info) => info.AddValue("type", _type, typeof(string));

		public override string ToString() => $"Type = {_type}";

		public string GetFixedString() => _fixedStrValues;

		public string GetEnumValue(UserBaseEntity entity, string enumColumnName)
		{
			switch (_type)
			{
				default:
					return null;
			}
		}
	}
}