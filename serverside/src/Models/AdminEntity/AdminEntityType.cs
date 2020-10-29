
using System;
using System.Collections.Generic;
using System.Linq;
using Lactalis.Services;
using GraphQL.Types;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Models
{
	/// <summary>
	/// The GraphQL type for returning data in GraphQL queries
	/// </summary>
	public class AdminEntityType : EfObjectGraphType<LactalisDBContext, AdminEntity>
	{
		public AdminEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Email, type: typeof(StringGraphType));

			// Add entity references

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class AdminEntityInputType : InputObjectGraphType<AdminEntity>
	{
		public AdminEntityInputType()
		{
			Name = "AdminEntityInput";
			Description = "The input object for adding a new AdminEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add entity references

			// Add references to foreign models to allow nested creation

		}
	}

	/// <summary>
	/// The GraphQL input type for creating a user entity
	/// </summary>
	public class AdminEntityCreateInputType : InputObjectGraphType<AdminEntity>
	{
		public AdminEntityCreateInputType()
		{
			Name = "AdminEntityCreateInput";
			Description = "The input object for creating a new AdminEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");

			// Add fields specific to a user entity
			Field<StringGraphType>("Email");
			Field<StringGraphType>("Password");


			// Add entity references


			// Add references to foreign models to allow nested creation

		}
	}
}