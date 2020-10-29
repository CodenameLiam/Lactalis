
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Enums;
using Lactalis.Security;
using Lactalis.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using Audit.EntityFramework;

namespace Lactalis.Models {
	public class User : IdentityUser<Guid>, IOwnerAbstractModel
	{
		public override Guid Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime Modified { get; set; }
		public virtual Guid Owner { get; set; }


		public virtual async Task BeforeSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
		{
		}

		public virtual async Task AfterSave(EntityState operation, LactalisDBContext dbContext, IServiceProvider serviceProvider, ICollection<ChangeState> changes, CancellationToken cancellationToken = default)
		{
		}

		public virtual IEnumerable<IAcl> Acls => new List<IAcl>
		{
		};

		public async virtual Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			LactalisDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel
		{
			return 0;
		}

		[Required]
		[EntityAttribute]
		public override string UserName { get; set; }

		[Email]
		[EntityAttribute]
		public override string Email { get; set; }

		public string Discriminator { get; set; }

		// Materialise the password hash in the subclass so it can be ignored by the audit logs
		[AuditIgnore]
		public override string PasswordHash { get; set; }

	}
	public class Group : IdentityRole<Guid>
	{
		/// <summary>
		/// Do the users in this group have access to the administration backend of the application
		/// </summary>
		public bool? HasBackendAccess { get; set; }

	}

	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.Property(e => e.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

			builder.HasDiscriminator(u => u.Discriminator);
			builder.HasIndex(e => e.Discriminator);

		}
	}

	public class GroupConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder
				.Property(e => e.Id)
				.HasDefaultValueSql("uuid_generate_v4()");

		}
	}

}