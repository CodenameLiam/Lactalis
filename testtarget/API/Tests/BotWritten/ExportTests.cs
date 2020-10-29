

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using APITests.EntityObjects;
using APITests.Setup;
using APITests.TheoryData.BotWritten;
using APITests.Utils;
using APITests.EntityObjects.Models;
using APITests.Factories;
using CsvHelper;
using FluentAssertions;
using RestSharp;
using Xunit;
using Xunit.Abstractions;



namespace APITests.Tests.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Integration")]
	public class ExportTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public ExportTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[ClassData(typeof(EntityFactorySingleTheoryData))]
		[ClassData(typeof(EntityFactoryMultipleTheoryData))]
		public void ExportEntity(EntityFactory entityFactory, int numEntities)
		{
			var entityList = entityFactory.ConstructAndSave(_output, numEntities);
			var entityName = entityList[0].EntityName;

			var api = new WebApi(_configure, _output);


			var query = QueryBuilder.CreateExportQuery(entityList);
			var queryList = new JsonArray { new JsonArray { query } };
			api.ConfigureAuthenticationHeaders();
			var response = api.Post($"/api/entity/{entityName}/export", queryList);

			var responseDictionary = CsvToDictionary(response.Content)
				.ToDictionary(pair => pair.Key.ToLowerInvariant(), pair => pair.Value);

			foreach (var entity in entityList)
			{
				var entityDict = entity.ToDictionary()
					.ToDictionary(pair => pair.Key.ToLowerInvariant(), pair => pair.Value);

				if (entity is UserBaseEntity)
				{
					// export will not contain password
					entityDict.Remove("password");
				}

				if (entity is IFileContainingEntity)
				{
					// file ids are generated server-side
					foreach (var fileAttributeKey in GetFileEntityFilterKeys(entity))
					{
						entityDict.Remove(fileAttributeKey);
					}
				}

				foreach (var attributeKey in entityDict.Keys.Select(x => x.ToLowerInvariant()))
				{
					responseDictionary.Should().ContainKey(attributeKey);
					responseDictionary[attributeKey]
						.Should()
						.Contain(entityDict[attributeKey])
						.And
						.HaveCount(numEntities);
				}
			}
		}

		private static IEnumerable<string> GetFileEntityFilterKeys(BaseEntity entity)
		{
			var filterKeys = new List<string>();
			switch (entity)
			{
				case TradingPostListingEntity _:
					filterKeys.Add("productimageid");
					break;
				case QualityDocumentEntity _:
					filterKeys.Add("fileid");
					break;
				case TechnicalDocumentEntity _:
					filterKeys.Add("fileid");
					break;
				case ImportantDocumentEntity _:
					filterKeys.Add("fileid");
					break;
				case NewsArticleEntity _:
					filterKeys.Add("featureimageid");
					break;
				case SustainabilityPostEntity _:
					filterKeys.Add("imageid");
					filterKeys.Add("fileid");
					break;
				case AgriSupplyDocumentEntity _:
					filterKeys.Add("fileid");
					break;
			}
			return filterKeys;
		}

		private static Dictionary<string, List<string>> CsvToDictionary(string csv)
		{
			var entityDictionary = new Dictionary<string, List<string>>();

			using var stringReader = new StringReader(csv);
			using var reader = new CsvReader(stringReader, CultureInfo.InvariantCulture);

			foreach (var record in reader.GetRecords<dynamic>())
			{
				if (record is IDictionary<string, object> recordDictionary)
				{
					foreach (var key in recordDictionary.Keys)
					{
						if (!entityDictionary.ContainsKey(key))
						{
							entityDictionary[key] = new List<string>();
						}
						entityDictionary[key].Add((string)recordDictionary[key]);
					}
				}
			}
			return entityDictionary;
		}

	}
}