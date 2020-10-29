
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace Lactalis.Models {
	public class EntityAttribute : Attribute
	{
	}

	public class FileReference : Attribute
	{
	}

	public class EntityForeignKey : Attribute
	{
		public string Name { get; }
		public string OppositeName { get; }
		public bool Required { get; }
		public Type OppositeEntity { get; }

		public EntityForeignKey(string name, string oppositeName, bool required, Type oppositeEntity)
		{
			Name = name;
			OppositeName = oppositeName;
			Required = required;
			OppositeEntity = oppositeEntity;
		}
	}

	public interface IAbstractModel
	{
		Guid Id { get; set; }
		DateTime Created { get; set; }
		DateTime Modified { get; set; }

		Task BeforeSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);
		Task AfterSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, ICollection<ChangeState> changes, CancellationToken cancellationToken = default);

	}

	public static class AbstractModelExtensions
	{
		public static bool ValidateObjectFields(this object abstractModel, List<ValidationResult> errors)
		{
			var context = new ValidationContext(abstractModel, serviceProvider: null, items: null);
			return Validator.TryValidateObject(abstractModel, context, errors, validateAllProperties: true);
		}
	}

	public class AbstractModelConfiguration
	{
		public static void Configure<T>(EntityTypeBuilder<T> builder)
			where T : class, IAbstractModel
		{
			// Configuration for a POSTGRES database
			builder
				.Property(e => e.Id)
				.HasDefaultValueSql("uuid_generate_v4()");
			builder
				.Property(e => e.Created)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
			builder
				.Property(e => e.Modified)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

		}
	}
}