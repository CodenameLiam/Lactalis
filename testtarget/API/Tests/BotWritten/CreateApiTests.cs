

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APITests.Classes;
using APITests.EntityObjects;
using APITests.EntityObjects.Models;
using APITests.Factories;
using APITests.Setup;
using APITests.Utils;
using APITests.TheoryData.BotWritten;
using EntityObject.Enums;
using RestSharp;
using Xunit;
using Xunit.Abstractions;

namespace APITests.Tests.BotWritten
{

	public class CreateApiTests : IClassFixture<StartupTestFixture>
	{
		private readonly StartupTestFixture _configure;
		private readonly ITestOutputHelper _output;

		public CreateApiTests(StartupTestFixture configure, ITestOutputHelper output)
		{
			_configure = configure;
			_output = output;
		}

		[Theory]
		[Trait("Category", "BotWritten")]
		[Trait("Category", "Integration")]
		[ClassData(typeof(EntityFactorySingleTheoryData))]
		[ClassData(typeof(EntityFactoryMultipleTheoryData))]
		public List<BaseEntity> CreateEntities(EntityFactory entityFactory, int numEntities)
		{
			// get the list of entities we will be creating
			var entityList = CreateReferencedEntities(entityFactory, numEntities);

			var api = new WebApi(_configure, _output);

			/*
			 * get the first entity out of the list, we will be iterating over this one and creating the references needed
			 * for it. This means that all the created entities share the same references
			 */
			var query = QueryBuilder.CreateEntityQueryBuilder(entityList);
			api.ConfigureAuthenticationHeaders();

			var response = PostCreateRequest(api, query, entityList);
			

			//valid ids returned and a valid response
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

			//return the object at the end, this can be reused.
			return entityList;
		}

		private IRestResponse PostCreateRequest(WebApi api, RestSharp.JsonObject query, List<BaseEntity> entityList)
		{
			// if entity does not contain files, perform a normal post
			if (!(entityList[0] is IFileContainingEntity))
			{
				return api.Post($"/api/graphql", query);
			}
			// otherwise, perform a multipart/form-data post
			var headers = new Dictionary<string, string>{{"Content-Type", "multipart/form-data"}};
			var files = new List<FileData>();
			foreach (var entity in entityList)
			{
				if (entity is IFileContainingEntity fileContainingEntity)
				{
					files.AddRange(fileContainingEntity.GetFiles().Where(file => file != null));
				}
			}
			var param = new Dictionary<string, object>
			{
				{"operationName", query["operationName"]},
				{"variables", query["variables"]},
				{"query", query["query"]}
			};
				
			return api.Post($"/api/graphql", param, headers, DataFormat.None, files);
		}

		private List<BaseEntity> CreateReferencedEntities(EntityFactory entityFactory, int numEntities)
		{
			// get the list of entities we will be creating
			var entityList = entityFactory.Construct(numEntities);

			//iterate overall of our references and recursively create them.
			foreach (var entity in entityList)
			{
				foreach (var reference in entity.References)
				{
					var referenceIdName = reference.OppositeName + "Id";

					// check if the reference has already been filled out
					if (!entity.ReferenceIdDictionary.ContainsKey(referenceIdName))
					{
						//ignore self references
						if (!reference.Optional)
						{
							var createdEntity =
								CreateEntities(new EntityFactory(reference.EntityName, entityFactory.GetFixedString()),
									1);

							var referenceDictionary = new Dictionary<string, ICollection<Guid>>()
							{
								{
									referenceIdName, new List<Guid> {createdEntity[0].Id}
								}
							};
							//add the created entity to our dictionary for the child entity for all entities in the list
							if (reference.Type == ReferenceType.ONE && reference.OppositeType == ReferenceType.ONE)
							{
								// if one-to-one each reference must be unique
								entityList.FirstOrDefault(x => x == entity).SetReferences(referenceDictionary);
								entityList.FirstOrDefault(x => x == entity).ReferenceIdDictionary[referenceIdName] =
									createdEntity[0].Id;
								entityList.FirstOrDefault(x => x == entity).ParentEntities.Add(createdEntity[0]);
							}
							else
							{
								// if one-to-many or many-to-many entities can share a reference.
								entityList.ForEach(x => x.SetReferences(referenceDictionary));
								entityList.ForEach(x => x.ReferenceIdDictionary[referenceIdName] = createdEntity[0].Id);
								entityList.ForEach(x => x.ParentEntities.Add(createdEntity[0]));
							}
						}
					}
				}
			}
			return entityList;
		}
	}
}