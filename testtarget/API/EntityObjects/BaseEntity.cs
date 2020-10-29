
using System;
using System.Collections.Generic;
using System.Linq;
using APITests.Setup;
using APITests.Utils;
using EntityObject.Enums;
using Microsoft.EntityFrameworkCore;
using LactalisDBContext = Lactalis.Models.LactalisDBContext;
using RestSharp;

namespace APITests.EntityObjects.Models
{
	public class Reference
	{
		public string Name { get; set; }
		public string OppositeName { get; set; }
		public string EntityName { get; set; }
		public bool Optional { get; set; }
		public ReferenceType Type { get; set; }
		public ReferenceType OppositeType { get; set; }
	}

	public class Attribute
	{
		public string Name { get; set; }
		public bool IsRequired { get; set;}
	}

	public abstract class BaseEntity
	{
		public abstract void Configure(ConfigureOptions option);
		public abstract Guid Save();
		public abstract Dictionary<string, string> ToDictionary();
		public abstract List<Guid> GetManyToManyReferences (string reference);
		public abstract void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences);
		public abstract string GetInvalidAttribute(string attribute, string validator);
		public abstract RestSharp.JsonObject ToJson();
		public abstract (int min, int max) GetLengthValidatorMinMax(string attribute);
		public abstract ICollection<(List<string> expectedErrors, RestSharp.JsonObject jsonObject)> GetInvalidMutatedJsons();
		public ICollection<Reference> References = new List<Reference>();
		public ICollection<Attribute> Attributes = new List<Attribute>();
		public ICollection<BaseEntity> ParentEntities = new List<BaseEntity>();
		public Guid Id = Guid.NewGuid();
		public DateTime Created = DateTime.Now;
		public DateTime Modified = DateTime.Now;
		public Dictionary<string, Guid?> ReferenceIdDictionary { get; set;} = new Dictionary<string, Guid?>();
		public string EntityName { get; set; }
		public virtual bool HasFile { get; set; } = false;
		private readonly StartupTestFixture _configure = new StartupTestFixture();

		internal Guid SaveThroughGraphQl(BaseEntity model)
		{
			var api = new WebApi(_configure);
			var query = QueryBuilder.CreateEntityQueryBuilder(new List<BaseEntity>{model});
			api.ConfigureAuthenticationHeaders();
			
			if (model is IFileContainingEntity fileContainingEntity)
			{
				var headers = new Dictionary<string, string>{{"Content-Type", "multipart/form-data"}};
				var files = fileContainingEntity.GetFiles().Where(file => file != null);;
				var param = new Dictionary<string, object>
				{
					{"operationName", query["operationName"]},
					{"variables", query["variables"]},
					{"query", query["query"]}
				};
				
				api.Post($"/api/graphql", param, headers, DataFormat.None, files);
				return Id;
			}
			api.Post($"/api/graphql", query);
			return Id;
		}

		internal Guid SaveToDB<T>(T model) where T : class, Lactalis.Models.IOwnerAbstractModel
		{
			var configure = new StartupTestFixture();
			var context = new LactalisDBContext(configure.DbContextOptions, null, null);
			model.Owner = configure.SuperOwnerId;
			var dbSet = context.GetDbSet<T>(typeof(T).Name);
			dbSet.Update(model);
			var addedEntry = context
				.ChangeTracker
				.Entries()
				.First(entry => model.Equals(entry.Entity));

			addedEntry.State = EntityState.Added;
			context.SaveChanges();
			context.Dispose();
			return model.Id;
		}

		public enum ConfigureOptions
		{
			CREATE_ATTRIBUTES_AND_REFERENCES,
			CREATE_ATTRIBUTES_ONLY,
			CREATE_REFERENCES_ONLY,
			CREATE_INVALID_ATTRIBUTES,
			CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES
		}
	}
}