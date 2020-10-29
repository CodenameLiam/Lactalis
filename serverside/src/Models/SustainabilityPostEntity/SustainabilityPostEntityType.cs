
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
	public class SustainabilityPostEntityType : EfObjectGraphType<LactalisDBContext, SustainabilityPostEntity>
	{
		public SustainabilityPostEntityType(IEfGraphQLService<LactalisDBContext> service) : base(service)
		{

			// Add model fields to type
			Field(o => o.Id, type: typeof(IdGraphType));
			Field(o => o.Created, type: typeof(DateTimeGraphType));
			Field(o => o.Modified, type: typeof(DateTimeGraphType));
			Field(o => o.Title, type: typeof(StringGraphType));
			Field(o => o.ImageId, type: typeof(IdGraphType));
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field(o => o.Content, type: typeof(StringGraphType));

			// Add entity references

		}
	}

	/// <summary>
	/// The GraphQL input type for mutation input
	/// </summary>
	public class SustainabilityPostEntityInputType : InputObjectGraphType<SustainabilityPostEntity>
	{
		public SustainabilityPostEntityInputType()
		{
			Name = "SustainabilityPostEntityInput";
			Description = "The input object for adding a new SustainabilityPostEntity";

			// Add entity fields
			Field<IdGraphType>("Id");
			Field<DateTimeGraphType>("Created");
			Field<DateTimeGraphType>("Modified");
			Field<StringGraphType>("Title");
			Field(o => o.ImageId, type: typeof(IdGraphType));
			Field(o => o.FileId, type: typeof(IdGraphType));
			Field<StringGraphType>("Content");

			// Add entity references

			// Add references to foreign models to allow nested creation

		}
	}

}